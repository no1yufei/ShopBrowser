namespace ShopeeChat
{
    partial class CaptchaLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptchaLoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.codeTxtLabel = new System.Windows.Forms.Label();
            this.userTBox = new System.Windows.Forms.TextBox();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.captchTBox = new System.Windows.Forms.TextBox();
            this.captchaPBox = new System.Windows.Forms.PictureBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.statusTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.captchaPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密  码:";
            // 
            // codeTxtLabel
            // 
            this.codeTxtLabel.AutoSize = true;
            this.codeTxtLabel.Location = new System.Drawing.Point(11, 90);
            this.codeTxtLabel.Name = "codeTxtLabel";
            this.codeTxtLabel.Size = new System.Drawing.Size(47, 12);
            this.codeTxtLabel.TabIndex = 3;
            this.codeTxtLabel.Text = "验证码:";
            // 
            // userTBox
            // 
            this.userTBox.Enabled = false;
            this.userTBox.Location = new System.Drawing.Point(62, 13);
            this.userTBox.Name = "userTBox";
            this.userTBox.Size = new System.Drawing.Size(156, 21);
            this.userTBox.TabIndex = 4;
            // 
            // passwordTBox
            // 
            this.passwordTBox.Enabled = false;
            this.passwordTBox.Location = new System.Drawing.Point(62, 37);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.PasswordChar = '*';
            this.passwordTBox.Size = new System.Drawing.Size(156, 21);
            this.passwordTBox.TabIndex = 5;
            // 
            // captchTBox
            // 
            this.captchTBox.Location = new System.Drawing.Point(62, 87);
            this.captchTBox.Name = "captchTBox";
            this.captchTBox.Size = new System.Drawing.Size(156, 21);
            this.captchTBox.TabIndex = 6;
            // 
            // captchaPBox
            // 
            this.captchaPBox.Location = new System.Drawing.Point(62, 111);
            this.captchaPBox.Name = "captchaPBox";
            this.captchaPBox.Size = new System.Drawing.Size(156, 49);
            this.captchaPBox.TabIndex = 7;
            this.captchaPBox.TabStop = false;
            this.captchaPBox.Click += new System.EventHandler(this.captchaPBox_Click);
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(62, 165);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 8;
            this.loginBtn.Text = "验证";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(143, 165);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // statusTBox
            // 
            this.statusTBox.Location = new System.Drawing.Point(62, 63);
            this.statusTBox.Name = "statusTBox";
            this.statusTBox.ReadOnly = true;
            this.statusTBox.Size = new System.Drawing.Size(156, 21);
            this.statusTBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "状 态：";
            // 
            // CaptchaLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 216);
            this.Controls.Add(this.statusTBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.captchaPBox);
            this.Controls.Add(this.captchTBox);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.userTBox);
            this.Controls.Add(this.codeTxtLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CaptchaLoginForm";
            this.Text = "验证码验证";
            ((System.ComponentModel.ISupportInitialize)(this.captchaPBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label codeTxtLabel;
        private System.Windows.Forms.TextBox userTBox;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.TextBox captchTBox;
        private System.Windows.Forms.PictureBox captchaPBox;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox statusTBox;
        private System.Windows.Forms.Label label3;
    }
}