using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ImageChunk : FileChunk
    {
        public ImageChunk(string Name, string Path) : base(Name,Path){ }

        public ImageChunk(string Name, byte[] content):base(Name,content){ }

        public ImageChunk() { }
    }
}
