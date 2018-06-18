namespace ReskinMaker
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DuckSpriteDropDown = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FeatherSpriteDropDown = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ArmSpriteDropDown = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RecolorDucksButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DuckSpriteDropDown);
            this.groupBox1.Location = new System.Drawing.Point(16, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Duck Sprite Size";
            // 
            // DuckSpriteDropDown
            // 
            this.DuckSpriteDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DuckSpriteDropDown.FormattingEnabled = true;
            this.DuckSpriteDropDown.Items.AddRange(new object[] {
            "32x32",
            "48x48",
            "64x64"});
            this.DuckSpriteDropDown.Location = new System.Drawing.Point(6, 17);
            this.DuckSpriteDropDown.Name = "DuckSpriteDropDown";
            this.DuckSpriteDropDown.Size = new System.Drawing.Size(187, 21);
            this.DuckSpriteDropDown.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FeatherSpriteDropDown);
            this.groupBox2.Location = new System.Drawing.Point(16, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Feather Sprite Size";
            // 
            // FeatherSpriteDropDown
            // 
            this.FeatherSpriteDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FeatherSpriteDropDown.FormattingEnabled = true;
            this.FeatherSpriteDropDown.Items.AddRange(new object[] {
            "12x4",
            "18x6",
            "24x8"});
            this.FeatherSpriteDropDown.Location = new System.Drawing.Point(7, 15);
            this.FeatherSpriteDropDown.Name = "FeatherSpriteDropDown";
            this.FeatherSpriteDropDown.Size = new System.Drawing.Size(186, 21);
            this.FeatherSpriteDropDown.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ArmSpriteDropDown);
            this.groupBox3.Location = new System.Drawing.Point(16, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Arm Sprite Size";
            // 
            // ArmSpriteDropDown
            // 
            this.ArmSpriteDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ArmSpriteDropDown.FormattingEnabled = true;
            this.ArmSpriteDropDown.Items.AddRange(new object[] {
            "16x16",
            "24x24",
            "32x32"});
            this.ArmSpriteDropDown.Location = new System.Drawing.Point(6, 19);
            this.ArmSpriteDropDown.Name = "ArmSpriteDropDown";
            this.ArmSpriteDropDown.Size = new System.Drawing.Size(186, 21);
            this.ArmSpriteDropDown.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RecolorDucksButton);
            this.groupBox4.Location = new System.Drawing.Point(16, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 46);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Recolor Textures";
            // 
            // RecolorDucksButton
            // 
            this.RecolorDucksButton.BackColor = System.Drawing.SystemColors.Control;
            this.RecolorDucksButton.ForeColor = System.Drawing.Color.Red;
            this.RecolorDucksButton.Location = new System.Drawing.Point(6, 17);
            this.RecolorDucksButton.Name = "RecolorDucksButton";
            this.RecolorDucksButton.Size = new System.Drawing.Size(186, 23);
            this.RecolorDucksButton.TabIndex = 0;
            this.RecolorDucksButton.Text = "false";
            this.RecolorDucksButton.UseVisualStyleBackColor = false;
            this.RecolorDucksButton.Click += new System.EventHandler(this.ButtonSwitchClick);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(238, 258);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox DuckSpriteDropDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox FeatherSpriteDropDown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox ArmSpriteDropDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button RecolorDucksButton;
    }
}
