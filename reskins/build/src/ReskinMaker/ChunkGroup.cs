using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ChunkGroup : DataChunk
    {
        public List<DataChunk> chunks;


        public ChunkGroup()
        {

        }

        public ChunkGroup(string Name,params DataChunk[] Chunks)
        {
            chunks = Chunks.ToList();
            Key = Name;
        }

        public override byte[] GetCustomData()
        {
            return byteStuff.combine(BitConverter.GetBytes(chunks.Count),byteStuff.combine(chunks.Select(x=> { var data = x.GetData();  return byteStuff.combine(BitConverter.GetBytes(data.Length),data); }).ToArray()));
        }

        protected override void ParseData(byte[] bytes)
        {
            chunks = new List<DataChunk>();

            int length = BitConverter.ToInt32(bytes,0);
            int index = 4;

            int hash;
            string key;
            int size;
            for (int i = 0; i < length; i++)
            {
                size = BitConverter.ToInt32(bytes,index);
                byte[] data = byteStuff.splice(bytes,index+=4,size);

                DataChunk.ParseHeader(data,out key,out hash);
                var chunk = (DataChunk)Activator.CreateInstance(ReskinFile.ChunkTypes[hash]);
                chunk.Parse(data);
                chunks.Add(chunk);

                index += size;
            }
            
        }

    }
}
