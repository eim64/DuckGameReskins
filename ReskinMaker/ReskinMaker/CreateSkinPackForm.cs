using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReskinMaker
{
    public partial class CreateSkinPackForm : Form
    {
        string DumpDirectory = @".\content\";
        public CreateSkinPackForm()
        {
            InitializeComponent();
            Directory.CreateDirectory(DumpDirectory);
        }

        void RefreshList()
        {
            ListBox.Items.Clear();
            foreach (var file in Directory.GetFiles(DumpDirectory, "*.rsk").Select(x => Path.GetFileNameWithoutExtension(x)))
                ListBox.Items.Add(file);
        }

        private void ButtonAddReskins_Click(object sender, EventArgs e)
        {
            OpenFile.Title = "Select Reskin Files";
            if (OpenFile.ShowDialog() != DialogResult.OK) return;
            foreach (var file in OpenFile.FileNames)
                AddFile(file);

            RefreshList();
        }

        void AddFile(string path)
        {
            string fileName = Path.GetFileName(path);
            switch (Path.GetExtension(path))
            {
                case ".rsk": 
                    File.Copy(path,DumpDirectory+fileName,true);
                    return;
                case ".png": 
                    try {
                        ReskinFile file = ReskinFile.ParseFile(new Bitmap(Image.FromFile(path)));

                        string name = Path.GetFileNameWithoutExtension(path);
                        File.WriteAllBytes(DumpDirectory + name + ".rsk", file.getHat(name));
                        return;

                    } catch { }
                    break;               
            }

            ShowWarning("Could not load " + path, "Invalid reskin");
        }

        void ShowWarning(string message,string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void CreateSkinPackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Directory.Delete(DumpDirectory,true);
        }

        private void ButtonRemoveReskins_Click(object sender, EventArgs e)
        {
            if (ListBox.SelectedIndex == -1) return;

            for (int i = ListBox.SelectedItems.Count - 1; i >= 0; i--)
            {
                string item = (string)ListBox.SelectedItems[i];
                ListBox.Items.Remove(item);

                File.Delete(DumpDirectory + item + ".rsk");
            }
        }

        //haHAA
        const string modcs = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Reflection;\nusing System.IO;\nusing System.Windows.Forms;\nusing System.Runtime.InteropServices;\nusing System.Text.RegularExpressions;\nusing System.ComponentModel;\nusing System.Diagnostics;\nusing Microsoft.Xna.Framework;\n[assembly: AssemblyTitle(\"@NAME@\")]\n[assembly: AssemblyCompany(\"@CREATOR@\")]\n[assembly: AssemblyDescription(\"@DESCRIPTION@\")]\n[assembly: AssemblyVersion(\"@VERSION@\")]\nnamespace DuckGame.MyMod{\npublic class MyMod : DisabledMod, IUpdateable{\npublic static ModConfiguration config;\nstatic PropertyInfo steamIdField = typeof(ModConfiguration).GetProperty(\"workshopID\",BindingFlags.Instance|BindingFlags.NonPublic);\nstatic PropertyInfo disabledField = typeof(ModConfiguration).GetProperty(\"disabled\", BindingFlags.Instance|BindingFlags.NonPublic);\npublic static string replaceData{get{\nreturn config.isWorkshop ? steamIdField.GetValue(config, new object[0]).ToString() : \"LOCAL\";}}\npublic static bool disabled{\nset { disabledField.SetValue(config,value,new object[0]); }\nget { return (bool)disabledField.GetValue(config, new object[0]); }}\nconst BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance;\nprotected override void OnPostInitialize(){\nconfig = configuration;\nvar form = (Form)Control.FromHandle(MonoMain.instance.Window.Handle);\nform.FormClosing += FormClosed;\n(typeof(Game).GetField(\"updateableComponents\", flags).GetValue(MonoMain.instance) as List<IUpdateable>).Add(this);}\nvoid FormClosed(object sender,EventArgs e){\nif (!Program.commandLine.Contains(\"-download\")) return;disabled = false;\ntypeof(ModLoader).GetMethod(\"DisabledModsChanged\",flags).Invoke(null,new object[0]);}\npublic bool Enabled{get{return true;}}\npublic int UpdateOrder{get{\nreturn 1;}}\npublic event EventHandler<EventArgs> EnabledChanged;\npublic event EventHandler<EventArgs> UpdateOrderChanged;bool updateLobby;\npublic void Update(GameTime gameTime){\nif (ModLoader.modsEnabled && Level.current is TeamSelect2 && Steam.lobby != null && Steam.lobby.id != 0L){\nstring str;int n;\nif (updateLobby && !string.IsNullOrEmpty((str = Steam.lobby.GetLobbyData(\"mods\"))) && (n = str.IndexOf(replaceData)) > -1){\nstr = str.Remove(n, replaceData.Length).Trim('|').Replace(\"||\", \"|\");\nSteam.lobby.SetLobbyData(\"mods\", str);\nupdateLobby = false;}}\nelse updateLobby = true;}}}";
        const string modconf = "<Mod/>";
        bool Create(string name,string creator,string description,string version)
        {
            if(name.IndexOfAny(Path.GetInvalidPathChars()) >= 0 || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                ShowWarning("Name cannot contain any of these characters: \n"+String.Join(" ",Path.GetInvalidPathChars()) +" "+ String.Join(" ",Path.GetInvalidFileNameChars()), "Invalid Name");
                return false;
            }

            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+@"\DuckGame\Mods\";
            string path = basePath + name;
            string config = modconf;
            if (Directory.Exists(path))
            {
                if (MessageBox.Show("This pack already exists\nDo you want to overwrite?", "warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return false;

                if (File.Exists(path + @"\mod.conf"))
                    config = File.ReadAllText(path + @"\mod.conf");

                Directory.Delete(path,true);
            }

            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + @"\build\src\");
            Directory.CreateDirectory(path + @"\content");
            CopyDir(DumpDirectory, path+@"\content\");

            File.WriteAllText(path + @"\mod.conf", config);
            File.WriteAllText(path+@"\build\src\Mod.cs",modcs.Replace("@NAME@",name).Replace("@CREATOR@",creator.Replace("\"","\\\"")).Replace("@DESCRIPTION@",description.Replace("\"", "\\\"")).Replace("@VERSION@",version.Replace("\"", "\\\"")));
            MessageBox.Show("Successfully created pack!\nOpen duckgame and it will appear in the mod list");
            return true;
        }

        void CopyDir(string SourcePath,string DestinationPath)
        {
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(TextAuthor.Text) || String.IsNullOrWhiteSpace(TextVersion.Text) || String.IsNullOrWhiteSpace(TextName.Text) || String.IsNullOrWhiteSpace(TextDescription.Text) || ListBox.Items.Count == 0)
            {
                ShowWarning("Some Field is empty!","Oh noes");
                return;
            }

            if (!Create(TextName.Text, TextAuthor.Text, TextDescription.Text, TextVersion.Text)) return;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The icon is whats displayed in the mod list\nIt should prefferably be a square\n\nThe Thumbnail is whats displayed on the \nsteam workshop");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectImage.Title = "Select Thumbnail";
            if (SelectImage.ShowDialog() != DialogResult.OK) return;
            File.Copy(SelectImage.FileName,DumpDirectory+@"screenshot.png",true);
            ThumbnailLabel.Text = "Current Thumbnail:\n"+Path.GetFileNameWithoutExtension(SelectImage.FileName);
            button3.Text = "Change Steam Thumbnail";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SelectImage.Title = "Select Icon";
            if (SelectImage.ShowDialog() != DialogResult.OK) return;
            File.Copy(SelectImage.FileName, DumpDirectory + @"preview.png",true);
            IconLabel.Text = "Current Icon:\n" + Path.GetFileNameWithoutExtension(SelectImage.FileName);
            button4.Text = "Change Icon";
        }

        private void TextVersion_TextChanged(object sender, EventArgs e)
        {
            TextVersion.Text = Regex.Replace(TextVersion.Text, @"[^0-9.]", "");
        }
    }
}
