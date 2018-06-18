using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class FileChunk : DataChunk
    {
        byte[] fileContent;

        public FileChunk(string Name,string Path)
        {
            fileContent = System.IO.File.ReadAllBytes(Path);
            Key = Name;
        }

        public FileChunk(string Name,byte[] content)
        {
            fileContent = content;
            Key = Name;
        }

        public FileChunk() { }

        public override byte[] GetCustomData()
        {
            return fileContent;
        }

        protected override void ParseData(byte[] bytes)
        {
            fileContent = bytes;
        }

        ~FileChunk()
        {
            fileContent = null;
        }

    }
}
