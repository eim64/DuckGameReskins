using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReskinMaker
{
    public partial class ImageSelectControl : UserControl
    {
        public PictureBox ImageDisplay = new ScaledPictureBox();
        public Size ImageSize;

        public ImageSelectControl()
        {
            ((System.ComponentModel.ISupportInitialize)(this.ImageDisplay)).BeginInit();
            this.ImageDisplay.Location = new System.Drawing.Point(3, 3);
            this.ImageDisplay.Name = "ImageDisplay";
            this.ImageDisplay.Size = new System.Drawing.Size(249, 190);
            this.ImageDisplay.TabIndex = 0;
            this.ImageDisplay.TabStop = false;
            this.ImageDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            InitializeComponent();
            this.Controls.Add(this.ImageDisplay);
            ((System.ComponentModel.ISupportInitialize)(this.ImageDisplay)).EndInit();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            fileDialogue.ShowDialog();
        }

        private void fileDialogue_FileOk(object sender, CancelEventArgs e)
        {
            ImageDisplay.ImageLocation = fileDialogue.FileName;
        }

        class ScaledPictureBox : PictureBox
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                base.OnPaint(e);
            }

            protected override void OnLoadCompleted(AsyncCompletedEventArgs e)
            {
                base.OnLoadCompleted(e);
                if (Image == null)
                    return;

                (Parent as ImageSelectControl).ImageSize = Image.Size;
                MainForm.UpdateValidity();
            }
        }
    }
}
