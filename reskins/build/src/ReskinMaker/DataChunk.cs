using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ReskinMaker
{
    class DataChunk
    {
        public string Key = String.Empty;

        public int Hash
        {
            get { return GetType().Name.GetHashCode(); }
        }

        public virtual byte[] GetCustomData()
        {
            return null;
        }

        public byte[] GetHeader()
        {
            byte[] customData = GetCustomData();
            byte[] hashCode = BitConverter.GetBytes(Hash);
            byte[] str = Encoding.UTF8.GetBytes(Key);
            byte[] strSize = BitConverter.GetBytes(str.Length);
            byte[] dataSize = BitConverter.GetBytes(customData.Length);

           
            return byteStuff.combine(hashCode,strSize,str,dataSize);
        }

        public static void ParseHeader(byte[] bytes,out string key,out int typeHash)
        {
            typeHash = BitConverter.ToInt32(bytes,0);

            int length = BitConverter.ToInt32(bytes,4);
            string str = Encoding.UTF8.GetString(bytes,8,length);
            key = str;
        }

        public byte[] GetData()
        {
            var header = GetHeader();
            byte[] customData = GetCustomData();

            var cData = new byte[customData.Length+header.Length];
            return byteStuff.combine(header,customData);
        }

        public void Parse(byte[] bytes)
        {
            int length = BitConverter.ToInt32(bytes,4);
            Key = Encoding.UTF8.GetString(bytes,8,length);
            int DataSize = BitConverter.ToInt32(bytes,8+length);
            ParseData(byteStuff.splice(bytes,12+length,DataSize));
        }

        protected virtual void ParseData(byte[] bytes)
        {

        }

        ~DataChunk()
        {
            Key = null;
        }

    }
}
