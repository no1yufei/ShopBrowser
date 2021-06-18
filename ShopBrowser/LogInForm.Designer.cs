namespace ShopeeChat
{
    partial class LogInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.label1 = new System.Windows.Forms.Label();
            this.userTBox = new System.Windows.Forms.TextBox();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sureBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.registerAccountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.randomCodeTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.randomPBox = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.clearCacheCBox = new System.Windows.Forms.CheckBox();
            this.addGroupPBox = new System.Windows.Forms.PictureBox();
            this.linkLabelPwdReset = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.randomPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addGroupPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(16, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "手机号";
            // 
            // userTBox
            // 
            this.userTBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userTBox.Location = new System.Drawing.Point(118, 57);
            this.userTBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userTBox.Name = "userTBox";
            this.userTBox.Size = new System.Drawing.Size(318, 35);
            this.userTBox.TabIndex = 1;
            this.userTBox.TextChanged += new System.EventHandler(this.userTBox_TextChanged);
            // 
            // passwordTBox
            // 
            this.passwordTBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordTBox.Location = new System.Drawing.Point(118, 100);
            this.passwordTBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.PasswordChar = '*';
            this.passwordTBox.Size = new System.Drawing.Size(318, 35);
            this.passwordTBox.TabIndex = 3;
            this.passwordTBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.passwordTBox_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(16, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "密   码";
            // 
            // sureBtn
            // 
            this.sureBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.sureBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sureBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sureBtn.Location = new System.Drawing.Point(168, 195);
            this.sureBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sureBtn.Name = "sureBtn";
            this.sureBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sureBtn.Size = new System.Drawing.Size(112, 46);
            this.sureBtn.TabIndex = 4;
            this.sureBtn.Text = "登录";
            this.sureBtn.UseVisualStyleBackColor = true;
            this.sureBtn.Click += new System.EventHandler(this.sureBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cancelBtn.Location = new System.Drawing.Point(288, 195);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(112, 46);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // registerAccountLinkLabel
            // 
            this.registerAccountLinkLabel.AutoSize = true;
            this.registerAccountLinkLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.registerAccountLinkLabel.Location = new System.Drawing.Point(210, 16);
            this.registerAccountLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.registerAccountLinkLabel.Name = "registerAccountLinkLabel";
            this.registerAccountLinkLabel.Size = new System.Drawing.Size(92, 27);
            this.registerAccountLinkLabel.TabIndex = 6;
            this.registerAccountLinkLabel.TabStop = true;
            this.registerAccountLinkLabel.Text = "账号注册";
            this.registerAccountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerAccountLinkLabel_LinkClicked);
            // 
            // randomCodeTBox
            // 
            this.randomCodeTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.randomCodeTBox.Location = new System.Drawing.Point(118, 146);
            this.randomCodeTBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.randomCodeTBox.Name = "randomCodeTBox";
            this.randomCodeTBox.Size = new System.Drawing.Size(162, 34);
            this.randomCodeTBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(16, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "验证码";
            // 
            // randomPBox
            // 
            this.randomPBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.randomPBox.Location = new System.Drawing.Point(288, 146);
            this.randomPBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.randomPBox.Name = "randomPBox";
            this.randomPBox.Size = new System.Drawing.Size(90, 34);
            this.randomPBox.TabIndex = 9;
            this.randomPBox.TabStop = false;
            this.randomPBox.Click += new System.EventHandler(this.randomPBox_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.linkLabel1.Location = new System.Drawing.Point(386, 150);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(52, 27);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "获取";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // clearCacheCBox
            // 
            this.clearCacheCBox.AutoSize = true;
            this.clearCacheCBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.clearCacheCBox.Location = new System.Drawing.Point(26, 202);
            this.clearCacheCBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clearCacheCBox.Name = "clearCacheCBox";
            this.clearCacheCBox.Size = new System.Drawing.Size(118, 31);
            this.clearCacheCBox.TabIndex = 11;
            this.clearCacheCBox.Text = "清空缓存";
            this.clearCacheCBox.UseVisualStyleBackColor = true;
            // 
            // addGroupPBox
            // 
            this.addGroupPBox.Image = ((System.Drawing.Image)(resources.GetObject("addGroupPBox.Image")));
            this.addGroupPBox.Location = new System.Drawing.Point(21, 13);
            this.addGroupPBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addGroupPBox.Name = "addGroupPBox";
            this.addGroupPBox.Size = new System.Drawing.Size(132, 33);
            this.addGroupPBox.TabIndex = 13;
            this.addGroupPBox.TabStop = false;
            this.addGroupPBox.Click += new System.EventHandler(this.addGroupPBox_Click);
            // 
            // linkLabelPwdReset
            // 
            this.linkLabelPwdReset.AutoSize = true;
            this.linkLabelPwdReset.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.linkLabelPwdReset.Location = new System.Drawing.Point(338, 16);
            this.linkLabelPwdReset.Name = "linkLabelPwdReset";
            this.linkLabelPwdReset.Size = new System.Drawing.Size(92, 27);
            this.linkLabelPwdReset.TabIndex = 14;
            this.linkLabelPwdReset.TabStop = true;
            this.linkLabelPwdReset.Text = "找回密码";
            this.linkLabelPwdReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPwdReset_LinkClicked);
            // 
            // LogInForm
            // 
            this.AcceptButton = this.sureBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(452, 254);
            this.Controls.Add(this.linkLabelPwdReset);
            this.Controls.Add(this.addGroupPBox);
            this.Controls.Add(this.clearCacheCBox);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.randomPBox);
            this.Controls.Add(this.randomCodeTBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.registerAccountLinkLabel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.sureBtn);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userTBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "店聊通(ShopChatPlus)";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.randomPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addGroupPBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTBox;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sureBtn;
        protected System.Windows.Forms.Button cancelBtn;
       
        private System.Windows.Forms.LinkLabel registerAccountLinkLabel;
        private System.Windows.Forms.TextBox randomCodeTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox randomPBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox clearCacheCBox;
        private System.Windows.Forms.PictureBox addGroupPBox;
        private System.Windows.Forms.LinkLabel linkLabelPwdReset;
    }
}