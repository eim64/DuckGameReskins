using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ReskinMaker
{
    public partial class SoundSelectControl : UserControl
    {
        public byte[] waveFile;
        byte[] waveData;
        public short BitCount;

        public SoundSelectControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() != DialogResult.OK) return;
            LoadWav(File.ReadAllBytes(openFile.FileName));
        }

        public void LoadWav(byte[] bytes)
        {
            try
            {
                waveFile = bytes;
                BitCount = BitConverter.ToInt16(waveFile, 34);
                if (BitCount != 16 && BitCount != 32) throw new FileLoadException();

                waveData = waveFile.Skip(44).ToArray();
                panel.Invalidate();
            }
            catch
            {
                waveFile = null;
                waveData = null;
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (waveData == null) return;

            int length = Math.Min(waveData.Length, 8192);
            float center = (panel.Height / 2);
            List<PointF> points = new List<PointF>();
            float xInc = (float)panel.Width / ((float)length/BitCount);
            float xpos = 0;
            for (int i = 0; i < length; i += BitCount)
                points.Add(new PointF(xpos+=xInc,center+(BitConverter.ToInt16(waveData,i)/1000)));

            e.Graphics.DrawLines(new Pen(Color.Red,2),points.ToArray());
        }
    }
}
