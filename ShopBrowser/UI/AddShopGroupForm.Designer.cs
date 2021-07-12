namespace ShopeeChat
{
    partial class AddShopGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddShopGroupForm));
            this.cancelBtn = new System.Windows.Forms.Button();
            this.sureBtn = new System.Windows.Forms.Button();
            this.isProxyCkBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userNameTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupNameTBox = new System.Windows.Forms.TextBox();
            this.portNumDU = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.pfCBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.portNumDU)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cancelBtn.Location = new System.Drawing.Point(167, 222);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 27);
            this.cancelBtn.TabIndex = 35;
            this.cancelBtn.TabStop = false;
            this.cancelBtn.Text = "取 消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // sureBtn
            // 
            this.sureBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sureBtn.Location = new System.Drawing.Point(62, 222);
            this.sureBtn.Name = "sureBtn";
            this.sureBtn.Size = new System.Drawing.Size(75, 27);
            this.sureBtn.TabIndex = 34;
            this.sureBtn.Text = "确 定";
            this.sureBtn.UseVisualStyleBackColor = true;
            this.sureBtn.Click += new System.EventHandler(this.sureBtn_Click);
            // 
            // isProxyCkBox
            // 
            this.isProxyCkBox.AutoSize = true;
            this.isProxyCkBox.Location = new System.Drawing.Point(84, 87);
            this.isProxyCkBox.Name = "isProxyCkBox";
            this.isProxyCkBox.Size = new System.Drawing.Size(15, 14);
            this.isProxyCkBox.TabIndex = 33;
            this.isProxyCkBox.UseVisualStyleBackColor = true;
            this.isProxyCkBox.CheckedChanged += new System.EventHandler(this.isProxyCkBox_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label10.Location = new System.Drawing.Point(13, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 20);
            this.label10.TabIndex = 32;
            this.label10.Text = "使用代理:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label9.Location = new System.Drawing.Point(255, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 20);
            this.label9.TabIndex = 31;
            this.label9.Text = "(必填)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label8.Location = new System.Drawing.Point(255, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "(必填)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label7.Location = new System.Drawing.Point(255, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "(可选)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label6.Location = new System.Drawing.Point(255, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 28;
            this.label6.Text = "(可选)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.Location = new System.Drawing.Point(13, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "店群名称:";
            // 
            // passwordTBox
            // 
            this.passwordTBox.Enabled = false;
            this.passwordTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.passwordTBox.Location = new System.Drawing.Point(84, 189);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.Size = new System.Drawing.Size(169, 25);
            this.passwordTBox.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.Location = new System.Drawing.Point(13, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "登录密码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(13, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "服务端口:";
            // 
            // userNameTBox
            // 
            this.userNameTBox.Enabled = false;
            this.userNameTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.userNameTBox.Location = new System.Drawing.Point(84, 162);
            this.userNameTBox.Name = "userNameTBox";
            this.userNameTBox.Size = new System.Drawing.Size(169, 25);
            this.userNameTBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(13, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "登录用户:";
            // 
            // serverTBox
            // 
            this.serverTBox.Enabled = false;
            this.serverTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.serverTBox.Location = new System.Drawing.Point(84, 109);
            this.serverTBox.Name = "serverTBox";
            this.serverTBox.Size = new System.Drawing.Size(169, 25);
            this.serverTBox.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(13, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "服务器名:";
            // 
            // groupNameTBox
            // 
            this.groupNameTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.groupNameTBox.Location = new System.Drawing.Point(84, 48);
            this.groupNameTBox.Name = "groupNameTBox";
            this.groupNameTBox.Size = new System.Drawing.Size(169, 25);
            this.groupNameTBox.TabIndex = 36;
            this.groupNameTBox.Text = "新建店群";
            // 
            // portNumDU
            // 
            this.portNumDU.Enabled = false;
            this.portNumDU.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.portNumDU.Location = new System.Drawing.Point(84, 135);
            this.portNumDU.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumDU.Name = "portNumDU";
            this.portNumDU.Size = new System.Drawing.Size(168, 25);
            this.portNumDU.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label11.Location = new System.Drawing.Point(13, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 20);
            this.label11.TabIndex = 38;
            this.label11.Text = "平台名称:";
            // 
            // pfCBox
            // 
            this.pfCBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pfCBox.FormattingEnabled = true;
            this.pfCBox.Items.AddRange(new object[] {
            "所有平台(支持所有平台)",
            "Shopee跨境,本地和全球账户",
            "Lazada跨境",
            "淘宝中国",
            "VPS或云服务器"});
            this.pfCBox.Location = new System.Drawing.Point(84, 7);
            this.pfCBox.Name = "pfCBox";
            this.pfCBox.Size = new System.Drawing.Size(169, 27);
            this.pfCBox.TabIndex = 39;
            // 
            // AddShopGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 255);
            this.Controls.Add(this.pfCBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.portNumDU);
            this.Controls.Add(this.groupNameTBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.sureBtn);
            this.Controls.Add(this.isProxyCkBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userNameTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.serverTBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddShopGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加店群";
            ((System.ComponentModel.ISupportInitialize)(this.portNumDU)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button sureBtn;
        private System.Windows.Forms.CheckBox isProxyCkBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userNameTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverTBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox groupNameTBox;
        private System.Windows.Forms.NumericUpDown portNumDU;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox pfCBox;
    }
}