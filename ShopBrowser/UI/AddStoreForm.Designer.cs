namespace ShopeeChat.UI
{
    partial class AddStoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStoreForm));
            this.label1 = new System.Windows.Forms.Label();
            this.accountTBox = new System.Windows.Forms.TextBox();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.regionCBox = new System.Windows.Forms.ComboBox();
            this.groupCBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.localCkBox = new System.Windows.Forms.CheckBox();
            this.importCookieBtn = new System.Windows.Forms.Button();
            this.onlyCookieCkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账 号";
            // 
            // accountTBox
            // 
            this.accountTBox.Location = new System.Drawing.Point(119, 100);
            this.accountTBox.Name = "accountTBox";
            this.accountTBox.Size = new System.Drawing.Size(188, 21);
            this.accountTBox.TabIndex = 1;
            // 
            // passwordTBox
            // 
            this.passwordTBox.Location = new System.Drawing.Point(119, 146);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.Size = new System.Drawing.Size(188, 21);
            this.passwordTBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密 码";
            // 
            // regionCBox
            // 
            this.regionCBox.FormattingEnabled = true;
            this.regionCBox.Location = new System.Drawing.Point(119, 60);
            this.regionCBox.Name = "regionCBox";
            this.regionCBox.Size = new System.Drawing.Size(188, 20);
            this.regionCBox.TabIndex = 4;
            // 
            // groupCBox
            // 
            this.groupCBox.FormattingEnabled = true;
            this.groupCBox.Location = new System.Drawing.Point(119, 18);
            this.groupCBox.Name = "groupCBox";
            this.groupCBox.Size = new System.Drawing.Size(188, 20);
            this.groupCBox.TabIndex = 5;
            this.groupCBox.SelectedIndexChanged += new System.EventHandler(this.groupCBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "地 区";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "店 群";
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(79, 239);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 8;
            this.addBtn.Text = "添 加";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(214, 239);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "退 出";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // localCkBox
            // 
            this.localCkBox.AutoSize = true;
            this.localCkBox.Location = new System.Drawing.Point(119, 182);
            this.localCkBox.Name = "localCkBox";
            this.localCkBox.Size = new System.Drawing.Size(72, 16);
            this.localCkBox.TabIndex = 10;
            this.localCkBox.Text = "本地店铺";
            this.localCkBox.UseVisualStyleBackColor = true;
            this.localCkBox.CheckedChanged += new System.EventHandler(this.localCkBox_CheckedChanged);
            // 
            // importCookieBtn
            // 
            this.importCookieBtn.Location = new System.Drawing.Point(214, 178);
            this.importCookieBtn.Name = "importCookieBtn";
            this.importCookieBtn.Size = new System.Drawing.Size(93, 23);
            this.importCookieBtn.TabIndex = 11;
            this.importCookieBtn.Text = "导入Cookie";
            this.importCookieBtn.UseVisualStyleBackColor = true;
            this.importCookieBtn.Visible = false;
            this.importCookieBtn.Click += new System.EventHandler(this.importCookieBtn_Click);
            // 
            // onlyCookieCkBox
            // 
            this.onlyCookieCkBox.AutoSize = true;
            this.onlyCookieCkBox.Location = new System.Drawing.Point(119, 204);
            this.onlyCookieCkBox.Name = "onlyCookieCkBox";
            this.onlyCookieCkBox.Size = new System.Drawing.Size(108, 16);
            this.onlyCookieCkBox.TabIndex = 12;
            this.onlyCookieCkBox.Text = "只用Cookie登录";
            this.onlyCookieCkBox.UseVisualStyleBackColor = true;
            this.onlyCookieCkBox.Visible = false;
            // 
            // AddStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 284);
            this.ControlBox = false;
            this.Controls.Add(this.onlyCookieCkBox);
            this.Controls.Add(this.importCookieBtn);
            this.Controls.Add(this.localCkBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupCBox);
            this.Controls.Add(this.regionCBox);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accountTBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddStoreForm";
            this.Text = "添加店铺";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox accountTBox;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox regionCBox;
        private System.Windows.Forms.ComboBox groupCBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckBox localCkBox;
        private System.Windows.Forms.Button importCookieBtn;
        private System.Windows.Forms.CheckBox onlyCookieCkBox;
    }
}