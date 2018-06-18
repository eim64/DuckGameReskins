using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ItemText : ItemData
    {
        string Value;
        TextControl ctrl;

        public ItemText(string name, string def = "") : base(name)
        {
            Value = def;
            ctrl = new TextControl();
            control = ctrl;
            ctrl.TEXT.Text = Value;
        }

        public override bool valid(out string Message)
        {
            Message = "";
            return true;
        }

        public override void parseData(DataChunk data)
        {
            if (data is TextChunk)
                ctrl.TEXT.Text = (data as TextChunk).Text;
        }

        public override DataChunk getData()
        {
            return new TextChunk(Name,ctrl.TEXT.Text);
        }

    }
}
