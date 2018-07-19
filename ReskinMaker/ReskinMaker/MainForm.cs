using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReskinMaker
{
    public partial class MainForm : Form
    {
        List<ItemData> Datas = new List<ItemData>();

        public MainForm()
        {
            InitializeComponent();
            InitializeItems();
            updateItemValidity();
        }

        public void InitializeItems()
        {
            Datas.Clear();

            Datas.Add(new ItemBitmap("Hat Texture",64,32) { required = true, helpMessage = "The Hat that will be displayed in the hat selector.\nmust be a png of size 64x32, capes go in its of field." });
            Datas.Add(new ItemBitmap("Duck Texture") { required = true, helpMessage = "This is what the normal duck texture will get replaced with.\nYou should use the template picture if you're not 100% certain of what you're doing\nIn the settings you can select the size of each frame,\nstandard is 32x32 and if you choose to increase the frame size the skin quality will increase" });
            Datas.Add(new ItemBitmap("Quack Texture") { helpMessage = "Same as the normal skin, except when the duck is quacking\nIf left empty the duck will just change size when quacking as it does with ArmyteCels client skins mod." });

            Datas.Add(new ItemBitmap("Controlled Texture") { helpMessage = "The texture thats used when the duck is being controlled\nIf left empty it will use the Quack Texture instead" });
            Datas.Add(new ItemBitmap("Arm Texture") { helpMessage = "The texture that will replace the ducks arm texture\nif left empty default feathers will be used" });
            Datas.Add(new ItemBitmap("Feather Texture") { helpMessage = "The texture that replaces the feathers that spwn when you're either close to being shot or shot.\nCheck template before you wreck urself"});

            Datas.Add(new ItemTextureGroup("Equipment Retextures") { required = true, helpMessage = "Changes the texture of objects when the duck picks it up.\n when you add something new you'll be asked to enter a texture name\nThe texture name is the same as the .xnb file name you can find in DuckGames content folder\nSo if you wanted to change the shotgun texture, find the texture name that the shotgun uses, (shotgun.xnb, and shotgunLoader.xnb)\nAnd add two retextures for the names (shotgun and shotgunLoader) and redraw their sprites to your liking." });

            Datas.Add(new ItemSettings("Texture Settings") { helpMessage = "The duck texture size is the size of each duck frame, including the duck quack and controlled texture.\nIf you increase the value the reskin will need a higher quality\n" });

            Datas.Add(new ItemBitmap("Cape Texture",32,32));

            Applydatas();
        }

        ReskinFile SaveImage()
        {
            string s;
            if (!Datas.TrueForAll(x => x.isValid(out s))) return null;

            ReskinFile file = new ReskinFile((Bitmap)(Datas[0].control as ImageSelectControl).ImageDisplay.Image, Datas.Skip(1).Select(x => x.getData()).Where(x=>x != null).ToArray());
            return file;
        }

        void OpenImage(string Path)
        {
            var bit = new Bitmap(Path);
            OpenImage(bit);
        }

        void OpenImage(Bitmap bitmap)
        {
            try {
                Applydatas();

                var file = ReskinFile.ParseFile(bitmap);
                var ctrl = (Datas[0].control as ImageSelectControl);
                ctrl.ImageDisplay.Image = file.Hat;
                ctrl.ImageSize = file.Hat.Size;
                foreach (var data in file.OtherData)
                    Datas.Find(x => x.Name == data.Key)?.parseData(data);
                bitmap.Dispose();
            }
            catch
            {
                MessageBox.Show("Picture doesnt contain any reskin data", "Oh noes!"); 
            }
        }


        void Applydatas()
        {
            controlSplitter.Controls.Clear();
            listView1.Items.Clear();
            foreach(var data in Datas)
                listView1.Items.Add(data.Name);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void updateItemValidity()
        {
            string message;
            for (int i = 0; i < Datas.Count; i++)
            {
                bool valid = Datas[i].isValid(out message);
                
                listView1.Items[i].ForeColor = valid ? Color.Green : Color.Red;
                listView1.Items[i].ToolTipText = (Datas[i].required ? "required! ":"")+ (valid? "" : message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            controlSplitter.Controls.Clear();

            updateItemValidity();

            if (listView1.SelectedIndices.Count == 0)  return;

            int index = listView1.SelectedIndices[0];
            controlSplitter.Controls.Add(Datas[index].control);
        }

        private void toolStripSaveButton_Click(object sender, EventArgs e)
        {
            Image img = SaveImage()?.getImage();
            if (img == null) return;

            SaveFile.Filter = "PNG File|*.png";
            SaveFile.DefaultExt = ".png";

            if (SaveFile.ShowDialog() != DialogResult.OK) return;

            img.Save(SaveFile.FileName);
        }

        private void toolStripOpenButton_Click(object sender, EventArgs e)
        {
            openFile.FileName = "myImage.png";
            openFile.Filter = "PNG File|*.png";
            if (openFile.ShowDialog() != DialogResult.OK) return;


            listView1.Items.Clear();
            Datas.Clear();
            InitializeItems();
            controlSplitter.Controls.Clear();
            OpenImage(openFile.FileName);
            updateItemValidity();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddText dialog = new AddText();
            dialog.Text = "Enter Team Name";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            SaveFile.Filter = "Reskin File|*.rsk";
            SaveFile.DefaultExt = ".rsk";

            if (SaveFile.ShowDialog() != DialogResult.OK) return;

            var bytes = SaveImage().getHat(dialog.TextBox.Text);
            File.WriteAllBytes(SaveFile.FileName,bytes);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openFile.FileName = "reskin.rsk";
            openFile.Filter = "Reskin File|*.rsk";

            if (openFile.ShowDialog() != DialogResult.OK) return;
            var bit = Deserialize(File.ReadAllBytes(openFile.FileName));
            if(bit != null)
                OpenImage(bit);
        }


        public static Bitmap Deserialize(byte[] data)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(data);
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                byte[] numArray1 = new byte[16] { 243,22,152,32,1,244,122,111,97,42,13,2,19,15,45,230};
                rijndaelManaged.Key = numArray1;
                rijndaelManaged.IV = ReadByteArray(memoryStream);
                BinaryReader binaryReader = new BinaryReader(new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV), CryptoStreamMode.Read));
                long num1 = binaryReader.ReadInt64();
                switch (num1)
                {
                    case 402965919293045L:
                    case 630430777029345L:
                    case 630449177029345L:
                    case 465665919293045L:
                        if (num1 == 630449177029345L || num1 == 465665919293045L)
                            binaryReader.ReadString();
                        string varName = binaryReader.ReadString();
                        int count = binaryReader.ReadInt32();
                        byte[] numArray2 = binaryReader.ReadBytes(count);
                        var img = Image.FromStream(new MemoryStream(numArray2));

                        binaryReader.Close();
                        return new Bitmap(img);

                    default: break;
                }
            }
            catch
            {
                MessageBox.Show("That aint no valid reskin file","oh noes!");
                return null;
            }
            return null;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] buffer1 = new byte[4];
            if (s.Read(buffer1, 0, buffer1.Length) != buffer1.Length)
                throw new SystemException("Stream did not contain properly formatted byte array");
            byte[] buffer2 = new byte[BitConverter.ToInt32(buffer1, 0)];
            if (s.Read(buffer2, 0, buffer2.Length) != buffer2.Length)
                throw new SystemException("Did not read byte array properly");
            return buffer2;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CreateSkinPackForm form = new CreateSkinPackForm();
            form.ShowDialog();
        }

        private void extraInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0) return;

            var data = Datas[listView1.SelectedIndices[0]];
            MessageBox.Show(data.helpMessage,"Extra Info");
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!e.Button.HasFlag(MouseButtons.Right) || listView1.SelectedIndices.Count == 0) return;
            contextMenu.Show(this,PointToClient( MousePosition ));
            extraInfoToolStripMenuItem.Enabled = !String.IsNullOrWhiteSpace(Datas[listView1.SelectedIndices[0]].helpMessage);
        }

        private void resetToDefault_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0 && MessageBox.Show("There is no selected propery!", "oh noes", MessageBoxButtons.OK) != DialogResult.Yes) return;
            if (MessageBox.Show("Sure you want to do that?", "Reset", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            List<ItemData> oldDatas = new List<ItemData>(Datas);
            int index = listView1.SelectedIndices[0];
            InitializeItems();
            oldDatas[index] = Datas[index];
            Datas = oldDatas;

        }
    }
}
