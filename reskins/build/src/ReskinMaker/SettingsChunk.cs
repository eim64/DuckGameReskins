using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    public class SettingsChunk : TextChunk
    {
        public SettingsChunk(){ }
        public SettingsChunk(string name,string text) : base(name, text) { }
    }
}
