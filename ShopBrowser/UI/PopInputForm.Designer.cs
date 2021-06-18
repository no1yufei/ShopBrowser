namespace ShopeeChat.UI
{
    partial class PopInputForm
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
            this.inputTbox1 = new System.Windows.Forms.TextBox();
            this.sureBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.regionCBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "注册链接：";
            // 
            // inputTbox1
            // 
            this.inputTbox1.Location = new System.Drawing.Point(84, 46);
            this.inputTbox1.Name = "inputTbox1";
            this.inputTbox1.Size = new System.Drawing.Size(427, 21);
            this.inputTbox1.TabIndex = 1;
            // 
            // sureBtn
            // 
            this.sureBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sureBtn.Location = new System.Drawing.Point(239, 89);
            this.sureBtn.Name = "sureBtn";
            this.sureBtn.Size = new System.Drawing.Size(75, 23);
            this.sureBtn.TabIndex = 2;
            this.sureBtn.Text = "确 定";
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
            this.label2.Text = "请选择站点";
            // 
            // regionCBox
            // 
            this.regionCBox.FormattingEnabled = true;
            this.regionCBox.Location = new System.Drawing.Point(85, 13);
            this.regionCBox.Name = "regionCBox";
            this.regionCBox.Size = new System.Drawing.Size(426, 20);
            this.regionCBox.TabIndex = 4;
            // 
            // PopInputForm
            // 
            this.AcceptButton = this.sureBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(523, 135);
            this.Controls.Add(this.regionCBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sureBtn);
            this.Controls.Add(this.inputTbox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PopInputForm";
            this.Text = "输入邀请码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputTbox1;
        private System.Windows.Forms.Button sureBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox regionCBox;
    }
}