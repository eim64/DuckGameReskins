using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    public class ReskinFile
    {
        public static Dictionary<int, Type> ChunkTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(DataChunk))).ToDictionary(x => x.Name.GetHashCode());

        public const int HatHeight = 32;

        public Bitmap Hat;
        public List<DataChunk> OtherData = new List<DataChunk>();


        string _uid;
        public string UID
        {
            get
            {
                if(_uid == null)
                {
                    MD5 md5 = MD5.Create();
                    byte[] hash = md5.ComputeHash(byteStuff.combine(OtherData.Select(x => x.GetData()).ToArray()));

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                        sb.Append(hash[i].ToString("X2"));

                    _uid = sb.ToString();
                }

                return _uid;   
            }

            set
            {
                _uid = value;
            }
        }

        protected byte[] getBytes(DataChunk selector)
        {
            byte[] data = selector.GetData();
            return byteStuff.combine(BitConverter.GetBytes(data.Length),data);
        }

        public DataChunk getChunk(string key)
        {
            return OtherData.Find(x => x.Key == key);
        }

        public Bitmap getImage()
        {
            int width = Hat.Width;

            byte[] allBytes = byteStuff.combine(OtherData.Select(x=>getBytes(x)).ToArray());
            allBytes = byteStuff.combine(BitConverter.GetBytes(allBytes.Length),allBytes);

            var argbs = byteStuff.GetRGBs(allBytes).ToArray();

            int height = (argbs.Count() / width) + HatHeight + 1;

            Bitmap map = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < HatHeight; y++)
                    map.SetPixel(x,y,Hat.GetPixel(x,y));

            for (int i = 0; i < argbs.Length; i++)
                map.SetPixel(i % width, (i / width) + HatHeight, Color.FromArgb(argbs[i]));

            return map;
        }

        protected void Parse(Bitmap Image,bool loadHat = true)
        {
            

            List<byte> Lbytes = new List<byte>();
            int DataSize = (Image.Height - HatHeight) * Image.Width;
            
            for (int i = 0; i < DataSize; i++)
                Lbytes.AddRange(BitConverter.GetBytes(Image.GetPixel(i%Image.Width, (i/Image.Width) + HatHeight).ToArgb()).Take(3));

            var bytes = Lbytes.ToArray();

            int Stop = BitConverter.ToInt32(bytes, 0);
            int end = 4;
            while(end < Stop)
            {
                int size = BitConverter.ToInt32(bytes,end);
                byte[] currentData = byteStuff.splice(bytes, end += 4, size);
                end += size;

                int hash;
                string key;
                DataChunk.ParseHeader(currentData,out key,out hash);

                DataChunk c = (DataChunk)Activator.CreateInstance(ChunkTypes[hash]);
                
                c.Parse(currentData);
                OtherData.Add(c);
            }

            if (!loadHat) return;

            Hat = new Bitmap(Image.Width, HatHeight);
            for (int x = 0; x < Image.Width; x++)
                for (int y = 0; y < HatHeight; y++)
                    Hat.SetPixel(x, y, Image.GetPixel(x, y));
        }

        public ReskinFile(Bitmap Hat, params DataChunk[] data)
        {
            this.Hat = Hat;
            OtherData = data.ToList();
        }

        public ReskinFile() { }

        public static ReskinFile ParseFile(Bitmap Image,bool loadHat = true)
        {
            var file = new ReskinFile();
            file.Parse(Image,loadHat);
            return file;
        }

        private static readonly byte[] encryptionKey = new byte[]{243,22,152,32,1,244,122,111,97,42,13,2,19,15,45,230};

        public byte[] getHat(string TeamName)
        {
            byte[] tex;
            using (MemoryStream stream = new MemoryStream())
            {
                getImage().Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                tex = stream.ToArray();
            }


            using (MemoryStream ms = new MemoryStream())
            using (MemoryStream unencryptedStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(unencryptedStream))
            {
                writer.Write(402965919293045L);
                writer.Write(TeamName);
                writer.Write(tex.Length);
                writer.Write(tex, 0, tex.Length);

                byte[] ivbytes = Encoding.ASCII.GetBytes("duckgamehatbyjvs");

                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = encryptionKey;
                rijndaelManaged.IV = ivbytes;

                byte[] process = unencryptedStream.ToArray();
                byte[] result = null;

                using (MemoryStream tempms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(tempms, rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV), CryptoStreamMode.Write))
                {
                    cs.Write(process, 0, process.Length);
                    cs.FlushFinalBlock();
                    result = tempms.ToArray();
                }

                if (result != null && result.Length > 1)
                {
                    byte[] IVLENGTH = BitConverter.GetBytes(ivbytes.Length);

                    ms.Write(IVLENGTH, 0, IVLENGTH.Length);
                    ms.Write(ivbytes, 0, ivbytes.Length);
                    ms.Write(result, 0, result.Length);

                    return ms.ToArray();
                }
            }

            return null;
        }

    }
}
