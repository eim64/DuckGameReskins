using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ItemTextureGroup : ItemData
    {
        GroupiItemControl ctrl;

        public ItemTextureGroup(string name) : base(name)
        {
            ctrl = new GroupiItemControl();
            control = ctrl;
        }

        public override DataChunk getData()
        {
            string message;
            if (!valid(out message)) return null;

            return new ChunkGroup(Name,ctrl.Datas.Select(x=>x.getData()).ToArray());
        }

        public override void parseData(DataChunk data)
        {
            ChunkGroup group = data as ChunkGroup;
            if (group == null) return;
            foreach(var chunk in group.chunks.Where(x=>x is ImageChunk))
            {
                ItemBitmap item = new ItemBitmap(chunk.Key);
                item.parseData(chunk);
                ctrl.Datas.Add(item);
            }
            ctrl.ApplyDatas();
        }

        public override bool valid(out string Message)
        {
            Message = "some textures are missing";
            if (ctrl.Datas.Count == 0) return true;
            string s;
            return ctrl.Datas.TrueForAll(x=>x.valid(out s));
        }

    }
}
