namespace ShopeeChat.UI
{
    partial class CookieImportForm
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
            this.filePathTbox = new System.Windows.Forms.TextBox();
            this.sureBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.formatCBox = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileOpenBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件位置：";
            // 
            // filePathTbox
            // 
            this.filePathTbox.Location = new System.Drawing.Point(109, 46);
            this.filePathTbox.Name = "filePathTbox";
            this.filePathTbox.Size = new System.Drawing.Size(271, 21);
            this.filePathTbox.TabIndex = 1;
            // 
            // sureBtn
            // 
            this.sureBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sureBtn.Location = new System.Drawing.Point(239, 89);
            this.sureBtn.Name = "sureBtn";
            this.sureBtn.Size = new System.Drawing.Size(75, 23);
            this.sureBtn.TabIndex = 2;
            this.sureBtn.Text = "导 入";
            this.sureBtn.UseVisualStyleBackColor = true;
            this.sureBtn.Click += new System.EventHandler(this.sureBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件格式：";
            // 
            // formatCBox
            // 
            this.formatCBox.FormattingEnabled = true;
            this.formatCBox.Items.AddRange(new object[] {
            "Json 格式Cookie文件"});
            this.formatCBox.Location = new System.Drawing.Point(109, 13);
            this.formatCBox.Name = "formatCBox";
            this.formatCBox.Size = new System.Drawing.Size(342, 20);
            this.formatCBox.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileOpenBtn
            // 
            this.fileOpenBtn.Location = new System.Drawing.Point(386, 44);
            this.fileOpenBtn.Name = "fileOpenBtn";
            this.fileOpenBtn.Size = new System.Drawing.Size(65, 23);
            this.fileOpenBtn.TabIndex = 5;
            this.fileOpenBtn.Text = "浏 览";
            this.fileOpenBtn.UseVisualStyleBackColor = true;
            this.fileOpenBtn.Click += new System.EventHandler(this.fileOpenBtn_Click);
            // 
            // CookieImportForm
            // 
            this.AcceptButton = this.sureBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(523, 135);
            this.Controls.Add(this.fileOpenBtn);
            this.Controls.Add(this.formatCBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sureBtn);
            this.Controls.Add(this.filePathTbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CookieImportForm";
            this.Text = "导入Cookie";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePathTbox;
        private System.Windows.Forms.Button sureBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox formatCBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button fileOpenBtn;
    }
}