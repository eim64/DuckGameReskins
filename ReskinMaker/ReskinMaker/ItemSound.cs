using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ItemSound : ItemData
    {
        SoundSelectControl ctrl = new SoundSelectControl();
        public ItemSound(string name) : base(name)
        {
            control = ctrl;
        }

        public override void parseData(DataChunk data)
        {
            ctrl.LoadWav(data.GetCustomData());
        }

        public override DataChunk getData()
        {
            return new SoundChunk(Name,ctrl.waveFile);
        }

        public override bool valid(out string Message)
        {
            Message = "";
            if (ctrl.waveFile == null) Message = "No Wave File!";
            else if (ctrl.BitCount != 16) Message = "Not 16bit wave file!";

            return String.IsNullOrEmpty(Message);
        }



    }
}
