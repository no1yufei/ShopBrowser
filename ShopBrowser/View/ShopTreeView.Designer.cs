namespace ShopChatPlus.View
{
    partial class ShopTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopTreeView));
            this.treeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyShopIDTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.chatFormNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageListTabIcon = new System.Windows.Forms.ImageList(this.components);
            this.storeTreeView = new System.Windows.Forms.TreeView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.delteGroupDTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.treeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeContextMenuStrip
            // 
            this.treeContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.treeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyShopIDTSM,
            this.toolStripSeparator1,
            this.delteGroupDTSM});
            this.treeContextMenuStrip.Name = "treeContextMenuStrip";
            this.treeContextMenuStrip.Size = new System.Drawing.Size(181, 76);
            // 
            // copyShopIDTSM
            // 
            this.copyShopIDTSM.Name = "copyShopIDTSM";
            this.copyShopIDTSM.Size = new System.Drawing.Size(180, 22);
            this.copyShopIDTSM.Text = "复制ShopID";
            this.copyShopIDTSM.Click += new System.EventHandler(this.copyShopIDTSM_Click);
            // 
            // treeImageList
            // 
            this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
            this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImageList.Images.SetKeyName(0, "group0.png");
            this.treeImageList.Images.SetKeyName(1, "group1.png");
            this.treeImageList.Images.SetKeyName(2, "region0.png");
            this.treeImageList.Images.SetKeyName(3, "region1.png");
            this.treeImageList.Images.SetKeyName(4, "store0.png");
            this.treeImageList.Images.SetKeyName(5, "store1.png");
            this.treeImageList.Images.SetKeyName(6, "user0.png");
            this.treeImageList.Images.SetKeyName(7, "user1.png");
            this.treeImageList.Images.SetKeyName(8, "list0.png");
            this.treeImageList.Images.SetKeyName(9, "list1.png");
            this.treeImageList.Images.SetKeyName(10, "choose0.png");
            this.treeImageList.Images.SetKeyName(11, "choose1.png");
            // 
            // chatFormNotifyIcon
            // 
            this.chatFormNotifyIcon.BalloonTipText = "店聊通";
            this.chatFormNotifyIcon.BalloonTipTitle = "店聊通";
            this.chatFormNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("chatFormNotifyIcon.Icon")));
            this.chatFormNotifyIcon.Text = "店聊通";
            this.chatFormNotifyIcon.Visible = true;
            // 
            // imageListTabIcon
            // 
            this.imageListTabIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabIcon.ImageStream")));
            this.imageListTabIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabIcon.Images.SetKeyName(0, "聊聊整合.png");
            this.imageListTabIcon.Images.SetKeyName(1, "采集上货.png");
            this.imageListTabIcon.Images.SetKeyName(2, "流量统计.png");
            this.imageListTabIcon.Images.SetKeyName(3, "违规统计.png");
            this.imageListTabIcon.Images.SetKeyName(4, "营销管理.png");
            this.imageListTabIcon.Images.SetKeyName(5, "订单状态.png");
            this.imageListTabIcon.Images.SetKeyName(6, "对账辅助.png");
            this.imageListTabIcon.Images.SetKeyName(7, "代发贴单.png");
            this.imageListTabIcon.Images.SetKeyName(8, "产品管理.png");
            this.imageListTabIcon.Images.SetKeyName(9, "订单处理.png");
            this.imageListTabIcon.Images.SetKeyName(10, "粉丝关注.png");
            this.imageListTabIcon.Images.SetKeyName(11, "消息群发.png");
            this.imageListTabIcon.Images.SetKeyName(12, "计划任务.png");
            // 
            // storeTreeView
            // 
            this.storeTreeView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.storeTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.storeTreeView.ContextMenuStrip = this.treeContextMenuStrip;
            this.storeTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeTreeView.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.storeTreeView.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.storeTreeView.ImageIndex = 0;
            this.storeTreeView.ImageList = this.treeImageList;
            this.storeTreeView.Indent = 25;
            this.storeTreeView.ItemHeight = 25;
            this.storeTreeView.Location = new System.Drawing.Point(0, 0);
            this.storeTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.storeTreeView.Name = "storeTreeView";
            this.storeTreeView.SelectedImageIndex = 11;
            this.storeTreeView.ShowNodeToolTips = true;
            this.storeTreeView.Size = new System.Drawing.Size(162, 279);
            this.storeTreeView.TabIndex = 1;
            this.storeTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.storeTreeView_AfterSelect);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // delteGroupDTSM
            // 
            this.delteGroupDTSM.Name = "delteGroupDTSM";
            this.delteGroupDTSM.Size = new System.Drawing.Size(180, 22);
            this.delteGroupDTSM.Text = "删除店群";
            this.delteGroupDTSM.Click += new System.EventHandler(this.delteGroupTSM_Click);
            // 
            // ShopTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.storeTreeView);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ShopTreeView";
            this.Size = new System.Drawing.Size(162, 279);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.treeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.ContextMenuStrip treeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyShopIDTSM;
        private System.Windows.Forms.NotifyIcon chatFormNotifyIcon;
        private System.Windows.Forms.ImageList imageListTabIcon;
        private System.Windows.Forms.TreeView storeTreeView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem delteGroupDTSM;
    }
}