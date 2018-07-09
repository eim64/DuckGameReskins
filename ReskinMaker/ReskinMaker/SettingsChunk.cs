using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    public class SettingsChunk : TextChunk
    {
        const char KVseparator = ';';
        const string ItemSeparator = "\n";


        public Dictionary<string,string> Settings
        {
            get { return GetDict(Text); }
        }
        public SettingsChunk(){ }
        public SettingsChunk(string name,string text) : base(name, text) { }
        public SettingsChunk(string name,Dictionary<string,string> dict) : base(name,ParseDict(dict)){ }

        public static string ParseDict(Dictionary<string, string> dict)
        {
            return String.Join(ItemSeparator,dict.Select(kvp=>kvp.Key + KVseparator + kvp.Value));
        }

        public static Dictionary<string,string> GetDict(string settings)
        {
            return settings.Split(ItemSeparator.First()).Select(x => x.Split(KVseparator)).Where(x => x.Length >= 2).ToDictionary(split => split[0], split => split[1]);
        }
    }
}
