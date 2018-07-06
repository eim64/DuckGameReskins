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
    public partial class SettingsControl : UserControl
    {
        Dictionary<String,Control> settings = new Dictionary<String,Control>();

        public string getValue(Control ctrl)
        {
            if (ctrl is ComboBox)
                return (ctrl as ComboBox).SelectedItem.ToString();
            if (ctrl is TextBox)
                return (ctrl as TextBox).Text;
            if (ctrl is NumericUpDown)
                return (ctrl as NumericUpDown).Value.ToString();

            return ctrl.Text; 
        }

        public void setValue(Control ctrl, string value)
        {
            try {
                if (ctrl is ComboBox)
                    (ctrl as ComboBox).SelectedItem = value;
                else if (ctrl is TextBox)
                    (ctrl as TextBox).Text = value;
                else if (ctrl is NumericUpDown)
                    (ctrl as NumericUpDown).Value = Decimal.Parse(value);
                else ctrl.Text = value;
            }
            catch
            {

            }
        }



        const char KVseparator = ';';
        const char ItemSeparator = '\n';
        
        public SettingsControl()
        {
            InitializeComponent();

            DuckSpriteDropDown.SelectedIndex = 0;
            FeatherSpriteDropDown.SelectedIndex = 0;
            ArmSpriteDropDown.SelectedIndex = 0;
            settings.Add("DuckSpriteSize",DuckSpriteDropDown);
            settings.Add("FeatherSpriteSize",FeatherSpriteDropDown);
            settings.Add("ArmSpriteSize",ArmSpriteDropDown);
            settings.Add("RecolorDucks",RecolorDucksButton);
        }

        public Dictionary<string,string> GetSettings()
        {
            return settings.ToDictionary(kvp => kvp.Key,kvp => getValue(kvp.Value));
        }

        public void ApplySettings(string settings)
        {
            foreach(var setting in settings.Split(ItemSeparator))
            {
                var split = setting.Split(KVseparator);

                Control ctrl;
                if (this.settings.TryGetValue(split[0], out ctrl))
                    setValue(ctrl, split[1]);
            }
        }

        private void ButtonSwitchClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            bool flag = button.Text == "true";
            button.Text = flag ? "false" : "true";
            button.ForeColor = flag ? Color.Red : Color.Green;
        }
    }
}
