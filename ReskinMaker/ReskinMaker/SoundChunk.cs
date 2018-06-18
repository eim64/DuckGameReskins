using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class SoundChunk : FileChunk
    {
        public SoundChunk(string Name, string Path) : base(Name,Path){ }

        public SoundChunk(string Name, byte[] content):base(Name,content){ }

        public SoundChunk() { }
    }
}
