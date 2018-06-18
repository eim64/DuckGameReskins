namespace ReskinMaker
{
    partial class TextControl
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
            this.TEXT = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TEXT
            // 
            this.TEXT.Location = new System.Drawing.Point(16, 3);
            this.TEXT.Name = "TEXT";
            this.TEXT.Size = new System.Drawing.Size(260, 20);
            this.TEXT.TabIndex = 0;
            // 
            // TextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TEXT);
            this.Name = "TextControl";
            this.Size = new System.Drawing.Size(295, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TEXT;
    }
}
