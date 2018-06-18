using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReskinMaker
{
    class ItemBitmap : ItemData
    {
        ImageSelectControl ctrl;
        int XSize;
        int YSize;
        bool fixedSize = false;

        public ItemBitmap(string name) : base(name)
        {
            ctrl = new ImageSelectControl();
            control = ctrl;
        }

        public ItemBitmap(string name,int xSize,int ySize) : this(name)
        {
            fixedSize = true;
            XSize = xSize;
            YSize = ySize;
            ctrl.InfoLabel.Text = "Image must be of size: "+XSize+" x "+YSize;
        }

        public override bool valid(out string Message)
        {
            Message = "";
            Size? ImageSize = ctrl?.ImageSize;

            if (ctrl.ImageDisplay.Image == null) Message = "No Image!";
            else if (fixedSize && ImageSize != new Size(XSize, YSize)) Message = "Wrong Image Size! "+ImageSize.Value.Width+"x"+ImageSize.Value.Height+", Expected: "+XSize+"x"+YSize;

            return Message.Length == 0;
        }

        public override void parseData(DataChunk data)
        {
            var stream = new MemoryStream(data.GetCustomData());
            ctrl.ImageDisplay.Image = new Bitmap(stream);
        }

        public override DataChunk getData()
        {
            string message;
            if (!valid(out message)) return null;

            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                ctrl.ImageDisplay.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes = stream.ToArray();
            }

            return new ImageChunk(Name,bytes);
        }

    }
}
