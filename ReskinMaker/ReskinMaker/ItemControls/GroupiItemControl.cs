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
    public partial class GroupiItemControl : UserControl
    {
        internal List<ItemData> Datas = new List<ItemData>();

        public GroupiItemControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddText t = new AddText();
            if (t.ShowDialog() != DialogResult.OK || t.Text == "") return;
            listView1.Items.Add(new ListViewItem(t.TextBox.Text)).EnsureVisible();
            Datas.Add(new ItemBitmap(t.TextBox.Text));

            MainForm.UpdateValidity();
        }

        public void ApplyDatas()
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(Datas.Select(x=>new ListViewItem(x.Name)).ToArray());
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (int s in listView1.SelectedIndices)
            {
                Datas.RemoveAt(s);
                listView1.Items.RemoveAt(s);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitter1.Controls.Clear();

            string message;
            for (int i = 0; i < Datas.Count; i++)
            {
                if (!Datas[i].isValid(out message)) { listView1.Items[i].ForeColor = Color.Red; listView1.Items[i].ToolTipText = message; }
                else { listView1.Items[i].ForeColor = Color.Green; listView1.Items[i].ToolTipText = ""; };
            }

            if (listView1.SelectedIndices.Count == 0) return;

            int index = listView1.SelectedIndices[0];
            splitter1.Controls.Add(Datas[index].control);
        }
    }
}
