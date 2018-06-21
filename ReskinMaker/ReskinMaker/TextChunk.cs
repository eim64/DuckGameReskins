using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    public class TextChunk : DataChunk
    {
        public string Text;
        public TextChunk()
        {

        }

        public TextChunk(string key,string text)
        {
            Key = key;
            Text = text;
        }

        protected override void ParseData(byte[] bytes)
        {
            int length = BitConverter.ToInt32(bytes,0);
            Text = Encoding.UTF8.GetString(bytes,4,length);
        }

        public override byte[] GetCustomData()
        {
            byte[] str = Encoding.UTF8.GetBytes(Text);
            return byteStuff.combine(BitConverter.GetBytes(str.Length),str);
        }

        ~TextChunk()
        {
            Text = null;
        }
    }
}
