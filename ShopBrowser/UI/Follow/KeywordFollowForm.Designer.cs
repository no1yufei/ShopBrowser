namespace ShopeeChat
{
    partial class KeywordFollowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeywordFollowForm));
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.imageCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newestBuydate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logTxtRTBox = new System.Windows.Forms.RichTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Shopname = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.followerLabel = new System.Windows.Forms.Label();
            this.followingLabel = new System.Windows.Forms.Label();
            this.keyWordTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.messageNumNUD = new System.Windows.Forms.NumericUpDown();
            this.chineseCkBox = new System.Windows.Forms.CheckBox();
            this.followBtn = new System.Windows.Forms.Button();
            this.localSellerCkBox = new System.Windows.Forms.CheckBox();
            this.sendSpanNUD = new System.Windows.Forms.NumericUpDown();
            this.buyerCkBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageNumNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendSpanNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer3.Location = new System.Drawing.Point(0, 161);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.logTxtRTBox);
            this.splitContainer3.Size = new System.Drawing.Size(1406, 1062);
            this.splitContainer3.SplitterDistance = 506;
            this.splitContainer3.SplitterWidth = 6;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageCol,
            this.userName,
            this.newestBuydate,
            this.Order});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1406, 506);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // imageCol
            // 
            this.imageCol.HeaderText = "最近登录时间";
            this.imageCol.MinimumWidth = 8;
            this.imageCol.Name = "imageCol";
            this.imageCol.ReadOnly = true;
            // 
            // userName
            // 
            this.userName.HeaderText = "买家";
            this.userName.MinimumWidth = 8;
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            // 
            // newestBuydate
            // 
            this.newestBuydate.HeaderText = "注册时间";
            this.newestBuydate.MinimumWidth = 8;
            this.newestBuydate.Name = "newestBuydate";
            this.newestBuydate.ReadOnly = true;
            // 
            // Order
            // 
            this.Order.HeaderText = "订单信息";
            this.Order.MinimumWidth = 8;
            this.Order.Name = "Order";
            this.Order.ReadOnly = true;
            // 
            // logTxtRTBox
            // 
            this.logTxtRTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTxtRTBox.Location = new System.Drawing.Point(0, 0);
            this.logTxtRTBox.Margin = new System.Windows.Forms.Padding(4);
            this.logTxtRTBox.Name = "logTxtRTBox";
            this.logTxtRTBox.Size = new System.Drawing.Size(1406, 550);
            this.logTxtRTBox.TabIndex = 0;
            this.logTxtRTBox.Text = "*******关键词请注意使用本地语言";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "最近登录时间";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 164;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "买家";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 165;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "注册时间";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 164;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "打声招呼";
            this.dataGridViewButtonColumn1.MinimumWidth = 8;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Width = 165;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "订单信息";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 166;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Shopname);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.followerLabel);
            this.panel1.Controls.Add(this.followingLabel);
            this.panel1.Controls.Add(this.keyWordTBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.messageNumNUD);
            this.panel1.Controls.Add(this.chineseCkBox);
            this.panel1.Controls.Add(this.followBtn);
            this.panel1.Controls.Add(this.localSellerCkBox);
            this.panel1.Controls.Add(this.sendSpanNUD);
            this.panel1.Controls.Add(this.buyerCkBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1406, 154);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(50, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前账号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.Location = new System.Drawing.Point(771, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 27);
            this.label4.TabIndex = 5;
            this.label4.Text = "粉丝数量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(22, 82);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 24);
            this.label6.TabIndex = 35;
            this.label6.Text = "包括:";
            // 
            // Shopname
            // 
            this.Shopname.AutoSize = true;
            this.Shopname.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Shopname.Location = new System.Drawing.Point(168, 33);
            this.Shopname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Shopname.Name = "Shopname";
            this.Shopname.Size = new System.Drawing.Size(92, 27);
            this.Shopname.TabIndex = 0;
            this.Shopname.Text = "当前账号";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(4, 137);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1398, 13);
            this.progressBar1.TabIndex = 0;
            // 
            // followerLabel
            // 
            this.followerLabel.AutoSize = true;
            this.followerLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.followerLabel.Location = new System.Drawing.Point(898, 33);
            this.followerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.followerLabel.Name = "followerLabel";
            this.followerLabel.Size = new System.Drawing.Size(77, 27);
            this.followerLabel.TabIndex = 4;
            this.followerLabel.Text = "粉丝量:";
            // 
            // followingLabel
            // 
            this.followingLabel.AutoSize = true;
            this.followingLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.followingLabel.Location = new System.Drawing.Point(558, 33);
            this.followingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.followingLabel.Name = "followingLabel";
            this.followingLabel.Size = new System.Drawing.Size(72, 27);
            this.followingLabel.TabIndex = 2;
            this.followingLabel.Text = "关注量";
            // 
            // keyWordTBox
            // 
            this.keyWordTBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.keyWordTBox.Location = new System.Drawing.Point(892, 74);
            this.keyWordTBox.Margin = new System.Windows.Forms.Padding(4);
            this.keyWordTBox.Name = "keyWordTBox";
            this.keyWordTBox.Size = new System.Drawing.Size(180, 34);
            this.keyWordTBox.TabIndex = 27;
            this.keyWordTBox.Text = "衣服";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(430, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "关注数量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(390, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 27);
            this.label3.TabIndex = 23;
            this.label3.Text = "共:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.Location = new System.Drawing.Point(796, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 27);
            this.label5.TabIndex = 26;
            this.label5.Text = "关键词:";
            // 
            // messageNumNUD
            // 
            this.messageNumNUD.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.messageNumNUD.Location = new System.Drawing.Point(433, 75);
            this.messageNumNUD.Margin = new System.Windows.Forms.Padding(4);
            this.messageNumNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.messageNumNUD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.messageNumNUD.Name = "messageNumNUD";
            this.messageNumNUD.Size = new System.Drawing.Size(90, 34);
            this.messageNumNUD.TabIndex = 24;
            this.messageNumNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.messageNumNUD.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // chineseCkBox
            // 
            this.chineseCkBox.AutoSize = true;
            this.chineseCkBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chineseCkBox.Location = new System.Drawing.Point(260, 81);
            this.chineseCkBox.Margin = new System.Windows.Forms.Padding(4);
            this.chineseCkBox.Name = "chineseCkBox";
            this.chineseCkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chineseCkBox.Size = new System.Drawing.Size(108, 28);
            this.chineseCkBox.TabIndex = 34;
            this.chineseCkBox.Text = "内地卖家";
            this.chineseCkBox.UseVisualStyleBackColor = true;
            // 
            // followBtn
            // 
            this.followBtn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.followBtn.Location = new System.Drawing.Point(1126, 69);
            this.followBtn.Margin = new System.Windows.Forms.Padding(4);
            this.followBtn.Name = "followBtn";
            this.followBtn.Size = new System.Drawing.Size(100, 45);
            this.followBtn.TabIndex = 25;
            this.followBtn.Text = "关  注";
            this.followBtn.UseVisualStyleBackColor = true;
            this.followBtn.Click += new System.EventHandler(this.followBtn_Click);
            // 
            // localSellerCkBox
            // 
            this.localSellerCkBox.AutoSize = true;
            this.localSellerCkBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.localSellerCkBox.Location = new System.Drawing.Point(150, 81);
            this.localSellerCkBox.Margin = new System.Windows.Forms.Padding(4);
            this.localSellerCkBox.Name = "localSellerCkBox";
            this.localSellerCkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.localSellerCkBox.Size = new System.Drawing.Size(108, 28);
            this.localSellerCkBox.TabIndex = 33;
            this.localSellerCkBox.Text = "本地卖家";
            this.localSellerCkBox.UseVisualStyleBackColor = true;
            // 
            // sendSpanNUD
            // 
            this.sendSpanNUD.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sendSpanNUD.Location = new System.Drawing.Point(649, 76);
            this.sendSpanNUD.Margin = new System.Windows.Forms.Padding(4);
            this.sendSpanNUD.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.sendSpanNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sendSpanNUD.Name = "sendSpanNUD";
            this.sendSpanNUD.Size = new System.Drawing.Size(80, 34);
            this.sendSpanNUD.TabIndex = 28;
            this.sendSpanNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sendSpanNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buyerCkBox
            // 
            this.buyerCkBox.AutoSize = true;
            this.buyerCkBox.Checked = true;
            this.buyerCkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buyerCkBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buyerCkBox.Location = new System.Drawing.Point(74, 81);
            this.buyerCkBox.Margin = new System.Windows.Forms.Padding(4);
            this.buyerCkBox.Name = "buyerCkBox";
            this.buyerCkBox.Size = new System.Drawing.Size(72, 28);
            this.buyerCkBox.TabIndex = 32;
            this.buyerCkBox.Text = "买家";
            this.buyerCkBox.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label7.Location = new System.Drawing.Point(728, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 27);
            this.label7.TabIndex = 29;
            this.label7.Text = "秒";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label8.Location = new System.Drawing.Point(524, 78);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 27);
            this.label8.TabIndex = 30;
            this.label8.Text = "条,每条间隔";
            // 
            // KeywordFollowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 1223);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KeywordFollowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关键词消息群发";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FollowForm_FormClosing);
            this.Load += new System.EventHandler(this.FollowForm_Load);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageNumNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sendSpanNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox logTxtRTBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Shopname;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label followerLabel;
        private System.Windows.Forms.Label followingLabel;
        private System.Windows.Forms.TextBox keyWordTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown messageNumNUD;
        private System.Windows.Forms.CheckBox chineseCkBox;
        private System.Windows.Forms.Button followBtn;
        private System.Windows.Forms.CheckBox localSellerCkBox;
        private System.Windows.Forms.NumericUpDown sendSpanNUD;
        private System.Windows.Forms.CheckBox buyerCkBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn imageCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn newestBuydate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
    }
}