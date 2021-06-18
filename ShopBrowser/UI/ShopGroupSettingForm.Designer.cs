namespace ShopeeChat
{
    partial class ShopGroupSettingForm
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
            this.serverTBox = new System.Windows.Forms.TextBox();
            this.userNameTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupCBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.sureBtn = new System.Windows.Forms.Button();
            this.portNumDU = new System.Windows.Forms.NumericUpDown();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.isProxyCkBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.portNumDU)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器名:";
            // 
            // serverTBox
            // 
            this.serverTBox.Enabled = false;
            this.serverTBox.Location = new System.Drawing.Point(94, 115);
            this.serverTBox.Name = "serverTBox";
            this.serverTBox.Size = new System.Drawing.Size(169, 21);
            this.serverTBox.TabIndex = 1;
            // 
            // userNameTBox
            // 
            this.userNameTBox.Enabled = false;
            this.userNameTBox.Location = new System.Drawing.Point(94, 202);
            this.userNameTBox.Name = "userNameTBox";
            this.userNameTBox.Size = new System.Drawing.Size(169, 21);
            this.userNameTBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "登录用户:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务端口:";
            // 
            // passwordTBox
            // 
            this.passwordTBox.Enabled = false;
            this.passwordTBox.Location = new System.Drawing.Point(94, 247);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.Size = new System.Drawing.Size(169, 21);
            this.passwordTBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "登录密码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "店群名称：";
            // 
            // groupCBox
            // 
            this.groupCBox.FormattingEnabled = true;
            this.groupCBox.Location = new System.Drawing.Point(94, 18);
            this.groupCBox.Name = "groupCBox";
            this.groupCBox.Size = new System.Drawing.Size(169, 20);
            this.groupCBox.TabIndex = 9;
            this.groupCBox.SelectedIndexChanged += new System.EventHandler(this.groupCBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "(可选)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "(可选)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(265, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "(必填)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(265, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "(必填)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "使用代理:";
            // 
            // sureBtn
            // 
            this.sureBtn.Location = new System.Drawing.Point(74, 288);
            this.sureBtn.Name = "sureBtn";
            this.sureBtn.Size = new System.Drawing.Size(75, 23);
            this.sureBtn.TabIndex = 16;
            this.sureBtn.Text = "保 存";
            this.sureBtn.UseVisualStyleBackColor = true;
            this.sureBtn.Click += new System.EventHandler(this.sureBtn_Click);
            // 
            // portNumDU
            // 
            this.portNumDU.Enabled = false;
            this.portNumDU.Location = new System.Drawing.Point(94, 161);
            this.portNumDU.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumDU.Name = "portNumDU";
            this.portNumDU.Size = new System.Drawing.Size(169, 21);
            this.portNumDU.TabIndex = 17;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(188, 288);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 18;
            this.cancelBtn.Text = "退 出";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click_1);
            // 
            // isProxyCkBox
            // 
            this.isProxyCkBox.AutoSize = true;
            this.isProxyCkBox.Location = new System.Drawing.Point(94, 87);
            this.isProxyCkBox.Name = "isProxyCkBox";
            this.isProxyCkBox.Size = new System.Drawing.Size(15, 14);
            this.isProxyCkBox.TabIndex = 15;
            this.isProxyCkBox.UseVisualStyleBackColor = true;
            this.isProxyCkBox.CheckedChanged += new System.EventHandler(this.isProxyCkBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(48, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(215, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "(*每个店群修改完成后，必须点击保存)";
            // 
            // ShopGroupSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 329);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.portNumDU);
            this.Controls.Add(this.sureBtn);
            this.Controls.Add(this.isProxyCkBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupCBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userNameTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverTBox);
            this.Controls.Add(this.label1);
            this.Name = "ShopGroupSettingForm";
            this.Text = "店群配置";
            ((System.ComponentModel.ISupportInitialize)(this.portNumDU)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverTBox;
        private System.Windows.Forms.TextBox userNameTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox groupCBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button sureBtn;
        private System.Windows.Forms.NumericUpDown portNumDU;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox isProxyCkBox;
        private System.Windows.Forms.Label label11;
    }
}