namespace ReskinMaker
{
    partial class CreateSkinPackForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.ListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonAddReskins = new System.Windows.Forms.Button();
            this.ButtonRemoveReskins = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.TextDescription = new System.Windows.Forms.TextBox();
            this.TextAuthor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextVersion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ThumbnailLabel = new System.Windows.Forms.Label();
            this.IconLabel = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.SelectImage = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pack Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(12, 25);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(220, 20);
            this.TextName.TabIndex = 1;
            this.TextName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location = new System.Drawing.Point(12, 204);
            this.ListBox.Name = "ListBox";
            this.ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ListBox.Size = new System.Drawing.Size(120, 186);
            this.ListBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Reskins";
            // 
            // ButtonAddReskins
            // 
            this.ButtonAddReskins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ButtonAddReskins.Location = new System.Drawing.Point(39, 396);
            this.ButtonAddReskins.Name = "ButtonAddReskins";
            this.ButtonAddReskins.Size = new System.Drawing.Size(23, 23);
            this.ButtonAddReskins.TabIndex = 4;
            this.ButtonAddReskins.Text = "+";
            this.ButtonAddReskins.UseVisualStyleBackColor = true;
            this.ButtonAddReskins.Click += new System.EventHandler(this.ButtonAddReskins_Click);
            // 
            // ButtonRemoveReskins
            // 
            this.ButtonRemoveReskins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ButtonRemoveReskins.Location = new System.Drawing.Point(78, 396);
            this.ButtonRemoveReskins.Name = "ButtonRemoveReskins";
            this.ButtonRemoveReskins.Size = new System.Drawing.Size(23, 23);
            this.ButtonRemoveReskins.TabIndex = 5;
            this.ButtonRemoveReskins.Text = "-";
            this.ButtonRemoveReskins.UseVisualStyleBackColor = true;
            this.ButtonRemoveReskins.Click += new System.EventHandler(this.ButtonRemoveReskins_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(134, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // OpenFile
            // 
            this.OpenFile.Filter = "Reskin File or Image|*.rsk;*.png";
            this.OpenFile.Multiselect = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pack Description";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextDescription
            // 
            this.TextDescription.Location = new System.Drawing.Point(13, 62);
            this.TextDescription.Name = "TextDescription";
            this.TextDescription.Size = new System.Drawing.Size(218, 20);
            this.TextDescription.TabIndex = 9;
            this.TextDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextAuthor
            // 
            this.TextAuthor.Location = new System.Drawing.Point(13, 104);
            this.TextAuthor.Name = "TextAuthor";
            this.TextAuthor.Size = new System.Drawing.Size(220, 20);
            this.TextAuthor.TabIndex = 11;
            this.TextAuthor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Author";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextVersion
            // 
            this.TextVersion.Location = new System.Drawing.Point(13, 146);
            this.TextVersion.Name = "TextVersion";
            this.TextVersion.Size = new System.Drawing.Size(220, 20);
            this.TextVersion.TabIndex = 13;
            this.TextVersion.Text = "1.0.0.0";
            this.TextVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Version";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(134, 253);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Select Thumbnail";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(138, 327);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Select Icon";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ThumbnailLabel
            // 
            this.ThumbnailLabel.AutoSize = true;
            this.ThumbnailLabel.Location = new System.Drawing.Point(136, 224);
            this.ThumbnailLabel.Name = "ThumbnailLabel";
            this.ThumbnailLabel.Size = new System.Drawing.Size(96, 26);
            this.ThumbnailLabel.TabIndex = 16;
            this.ThumbnailLabel.Text = "Current Thumbnail:\r\nNone\r\n";
            this.ThumbnailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IconLabel
            // 
            this.IconLabel.AutoSize = true;
            this.IconLabel.Location = new System.Drawing.Point(154, 298);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Size = new System.Drawing.Size(68, 26);
            this.IconLabel.TabIndex = 17;
            this.IconLabel.Text = "Current Icon:\r\nNone\r\n";
            this.IconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(210, 356);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(21, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // SelectImage
            // 
            this.SelectImage.DefaultExt = "png";
            this.SelectImage.Filter = "PNG Files|*.png";
            // 
            // CreateSkinPackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 473);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.IconLabel);
            this.Controls.Add(this.ThumbnailLabel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TextVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextAuthor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ButtonRemoveReskins);
            this.Controls.Add(this.ButtonAddReskins);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreateSkinPackForm";
            this.ShowIcon = false;
            this.Text = "Create Pack ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateSkinPackForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.ListBox ListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonAddReskins;
        private System.Windows.Forms.Button ButtonRemoveReskins;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextDescription;
        private System.Windows.Forms.TextBox TextAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label ThumbnailLabel;
        private System.Windows.Forms.Label IconLabel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.OpenFileDialog SelectImage;
    }
}