namespace ShopeeChat.UI
{
    partial class AttachedMessageDialog
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
            this.mainSContainer = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.langTsCBox = new System.Windows.Forms.ToolStripComboBox();
            this.rlLayoutSContainer = new System.Windows.Forms.SplitContainer();
            this.textSPContainer = new System.Windows.Forms.SplitContainer();
            this.orgmsgRTBox = new System.Windows.Forms.RichTextBox();
            this.descMsgRTBox = new System.Windows.Forms.RichTextBox();
            this.TranslateBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainSContainer)).BeginInit();
            this.mainSContainer.Panel2.SuspendLayout();
            this.mainSContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlLayoutSContainer)).BeginInit();
            this.rlLayoutSContainer.Panel1.SuspendLayout();
            this.rlLayoutSContainer.Panel2.SuspendLayout();
            this.rlLayoutSContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textSPContainer)).BeginInit();
            this.textSPContainer.Panel1.SuspendLayout();
            this.textSPContainer.Panel2.SuspendLayout();
            this.textSPContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSContainer
            // 
            this.mainSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSContainer.Name = "mainSContainer";
            this.mainSContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.mainSContainer.Panel1Collapsed = true;
            // 
            // mainSContainer.Panel2
            // 
            this.mainSContainer.Panel2.Controls.Add(this.rlLayoutSContainer);
            this.mainSContainer.Size = new System.Drawing.Size(546, 163);
            this.mainSContainer.SplitterDistance = 25;
            this.mainSContainer.SplitterWidth = 1;
            this.mainSContainer.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.langTsCBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 2);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(347, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox1.Tag = "";
            this.toolStripTextBox1.Text = "文本翻译为：";
            // 
            // langTsCBox
            // 
            this.langTsCBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.langTsCBox.Items.AddRange(new object[] {
            "英语",
            "泰语",
            "马来语",
            "菲律宾语",
            "越南语"});
            this.langTsCBox.Name = "langTsCBox";
            this.langTsCBox.Size = new System.Drawing.Size(121, 25);
            this.langTsCBox.Text = "目标语言";
            // 
            // rlLayoutSContainer
            // 
            this.rlLayoutSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlLayoutSContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.rlLayoutSContainer.Location = new System.Drawing.Point(0, 0);
            this.rlLayoutSContainer.Margin = new System.Windows.Forms.Padding(0);
            this.rlLayoutSContainer.Name = "rlLayoutSContainer";
            this.rlLayoutSContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rlLayoutSContainer.Panel1
            // 
            this.rlLayoutSContainer.Panel1.Controls.Add(this.textSPContainer);
            // 
            // rlLayoutSContainer.Panel2
            // 
            this.rlLayoutSContainer.Panel2.Controls.Add(this.menuStrip1);
            this.rlLayoutSContainer.Panel2.Controls.Add(this.TranslateBtn);
            this.rlLayoutSContainer.Size = new System.Drawing.Size(546, 163);
            this.rlLayoutSContainer.SplitterDistance = 137;
            this.rlLayoutSContainer.SplitterWidth = 1;
            this.rlLayoutSContainer.TabIndex = 3;
            // 
            // textSPContainer
            // 
            this.textSPContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textSPContainer.Location = new System.Drawing.Point(0, 0);
            this.textSPContainer.Name = "textSPContainer";
            // 
            // textSPContainer.Panel1
            // 
            this.textSPContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // textSPContainer.Panel2
            // 
            this.textSPContainer.Panel2.Controls.Add(this.groupBox2);
            this.textSPContainer.Size = new System.Drawing.Size(546, 137);
            this.textSPContainer.SplitterDistance = 273;
            this.textSPContainer.SplitterWidth = 1;
            this.textSPContainer.TabIndex = 0;
            // 
            // orgmsgRTBox
            // 
            this.orgmsgRTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orgmsgRTBox.Location = new System.Drawing.Point(0, 14);
            this.orgmsgRTBox.Margin = new System.Windows.Forms.Padding(0);
            this.orgmsgRTBox.Name = "orgmsgRTBox";
            this.orgmsgRTBox.Size = new System.Drawing.Size(273, 123);
            this.orgmsgRTBox.TabIndex = 2;
            this.orgmsgRTBox.Text = "";
            this.orgmsgRTBox.TextChanged += new System.EventHandler(this.msgRTBox_TextChanged);
            // 
            // descMsgRTBox
            // 
            this.descMsgRTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descMsgRTBox.Location = new System.Drawing.Point(0, 14);
            this.descMsgRTBox.Margin = new System.Windows.Forms.Padding(0);
            this.descMsgRTBox.Name = "descMsgRTBox";
            this.descMsgRTBox.Size = new System.Drawing.Size(272, 123);
            this.descMsgRTBox.TabIndex = 3;
            this.descMsgRTBox.Text = "";
            // 
            // TranslateBtn
            // 
            this.TranslateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TranslateBtn.Location = new System.Drawing.Point(273, -1);
            this.TranslateBtn.Margin = new System.Windows.Forms.Padding(0);
            this.TranslateBtn.Name = "TranslateBtn";
            this.TranslateBtn.Size = new System.Drawing.Size(272, 26);
            this.TranslateBtn.TabIndex = 0;
            this.TranslateBtn.Text = "翻       译";
            this.TranslateBtn.UseVisualStyleBackColor = true;
            this.TranslateBtn.Click += new System.EventHandler(this.TranslateBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.orgmsgRTBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(273, 137);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原 文";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.descMsgRTBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(272, 137);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "译 文";
            // 
            // AttachedMessageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSContainer);
            this.Name = "AttachedMessageDialog";
            this.Size = new System.Drawing.Size(546, 163);
            this.mainSContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSContainer)).EndInit();
            this.mainSContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.rlLayoutSContainer.Panel1.ResumeLayout(false);
            this.rlLayoutSContainer.Panel2.ResumeLayout(false);
            this.rlLayoutSContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlLayoutSContainer)).EndInit();
            this.rlLayoutSContainer.ResumeLayout(false);
            this.textSPContainer.Panel1.ResumeLayout(false);
            this.textSPContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textSPContainer)).EndInit();
            this.textSPContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSContainer;
        private System.Windows.Forms.RichTextBox orgmsgRTBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripComboBox langTsCBox;
        private System.Windows.Forms.SplitContainer rlLayoutSContainer;
        private System.Windows.Forms.SplitContainer textSPContainer;
        private System.Windows.Forms.RichTextBox descMsgRTBox;
        private System.Windows.Forms.Button TranslateBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}