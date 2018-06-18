using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ItemSettings : ItemData
    {
        SettingsControl ctrl = new SettingsControl();
        public ItemSettings(string name) : base(name)
        {
            control = ctrl;
        }

        public override void parseData(DataChunk data)
        {
            if (data is SettingsChunk) ctrl.ApplySettings((data as SettingsChunk).Text);
            base.parseData(data);
        }

        public override DataChunk getData()
        {
            return new SettingsChunk(Name, ctrl.GetSettings());
        }
    }
}
