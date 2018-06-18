using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class byteStuff
    {
        public static byte[] combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        public static Bitmap ParseImage(byte[] bytes)
        {
            int width = BitConverter.ToInt32(bytes, 0);
            int height = BitConverter.ToInt32(bytes, 4);

            int imgSize = width * height;
            Bitmap map = new Bitmap(width, height);

            var argbs = byteStuff.GroupBytes(byteStuff.splice(bytes, 8, bytes.Length - 8));

            for (int i = 0; i < argbs.Count(); i++)
                map.SetPixel(i % width, i / width, Color.FromArgb(argbs.ElementAt(i)));

            return map;
        }

        public static byte[] EncodeImage(Bitmap img)
        {
            List<byte> bytes = new List<byte>();
            int imgSize = img.Width * img.Height;
            bytes.AddRange(BitConverter.GetBytes(img.Width));
            bytes.AddRange(BitConverter.GetBytes(img.Height));

            for (int i = 0; i < imgSize; i++)
                bytes.AddRange(BitConverter.GetBytes(img.GetPixel(i % img.Width, i / img.Width).ToArgb()));

            return bytes.ToArray();
        }


        public static IEnumerable<int> GroupBytes(byte[] bytes)
        {
            int n = 0;
            byte[] bc = new byte[4];
            while (n < bytes.Length)
            {
                bc[n % 4] = bytes[n];                
                n++;

                if (n % 4 != 0) continue;
                yield return BitConverter.ToInt32(bc,0);
                bc = new byte[4];
            }
        }

        public static IEnumerable<int> GetRGBs(byte[] bytes)
        {
            int n = 0;
            byte[] bc = new byte[4];
            while (n < bytes.Length)
            {
                bc[n % 3] = bytes[n];
                n++;

                if (n % 3 != 0 && n != bytes.Length) continue;
                bc[3] = 0xFF;

                yield return BitConverter.ToInt32(bc, 0);
                bc = new byte[4];
            }
        }

        public static byte[] splice(byte[] bytes,int start,int length)
        {
            return bytes.Skip(start).Take(length).ToArray();
        }

    }
}
