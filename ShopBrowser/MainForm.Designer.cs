namespace ShopeeChat
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.chatFormNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageListTabIcon = new System.Windows.Forms.ImageList(this.components);
            this.mainSPContainer = new System.Windows.Forms.SplitContainer();
            this.chatSPContainer = new System.Windows.Forms.SplitContainer();
            this.contentSplitPannel = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.transGBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logRTBox = new System.Windows.Forms.RichTextBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.accountMangerTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.creatShopGroupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addStoreTMSI = new System.Windows.Forms.ToolStripMenuItem();
            this.createTemplateTMSI = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.还原配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粉丝关注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消息群发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.订单列表群发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.买家群发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索群发ToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MinTSItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MaxTSItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTSItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitTSMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTMSI = new System.Windows.Forms.ToolStripMenuItem();
            this.DNSTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ipconfigTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.transUISwitchBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mainSPContainer)).BeginInit();
            this.mainSPContainer.Panel1.SuspendLayout();
            this.mainSPContainer.Panel2.SuspendLayout();
            this.mainSPContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chatSPContainer)).BeginInit();
            this.chatSPContainer.Panel2.SuspendLayout();
            this.chatSPContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contentSplitPannel)).BeginInit();
            this.contentSplitPannel.Panel2.SuspendLayout();
            this.contentSplitPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.chatFormNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chatFormNotifyIcon_MouseDoubleClick);
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
            // mainSPContainer
            // 
            this.mainSPContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSPContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSPContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSPContainer.Name = "mainSPContainer";
            this.mainSPContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSPContainer.Panel1
            // 
            this.mainSPContainer.Panel1.Controls.Add(this.menuStrip);
            // 
            // mainSPContainer.Panel2
            // 
            this.mainSPContainer.Panel2.Controls.Add(this.chatSPContainer);
            this.mainSPContainer.Size = new System.Drawing.Size(899, 816);
            this.mainSPContainer.SplitterDistance = 36;
            this.mainSPContainer.SplitterWidth = 1;
            this.mainSPContainer.TabIndex = 4;
            // 
            // chatSPContainer
            // 
            this.chatSPContainer.BackColor = System.Drawing.SystemColors.Control;
            this.chatSPContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatSPContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.chatSPContainer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatSPContainer.Location = new System.Drawing.Point(0, 0);
            this.chatSPContainer.Margin = new System.Windows.Forms.Padding(4);
            this.chatSPContainer.Name = "chatSPContainer";
            // 
            // chatSPContainer.Panel2
            // 
            this.chatSPContainer.Panel2.AutoScroll = true;
            this.chatSPContainer.Panel2.Controls.Add(this.statusStrip1);
            this.chatSPContainer.Panel2.Controls.Add(this.contentSplitPannel);
            this.chatSPContainer.Size = new System.Drawing.Size(899, 779);
            this.chatSPContainer.SplitterDistance = 322;
            this.chatSPContainer.SplitterWidth = 1;
            this.chatSPContainer.TabIndex = 2;
            // 
            // contentSplitPannel
            // 
            this.contentSplitPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentSplitPannel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.contentSplitPannel.Location = new System.Drawing.Point(0, 0);
            this.contentSplitPannel.Name = "contentSplitPannel";
            this.contentSplitPannel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // contentSplitPannel.Panel2
            // 
            this.contentSplitPannel.Panel2.Controls.Add(this.splitContainer3);
            this.contentSplitPannel.Panel2Collapsed = true;
            this.contentSplitPannel.Size = new System.Drawing.Size(576, 779);
            this.contentSplitPannel.SplitterDistance = 638;
            this.contentSplitPannel.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.transGBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(576, 137);
            this.splitContainer3.SplitterDistance = 346;
            this.splitContainer3.TabIndex = 1;
            // 
            // transGBox
            // 
            this.transGBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transGBox.Location = new System.Drawing.Point(0, 0);
            this.transGBox.Margin = new System.Windows.Forms.Padding(0);
            this.transGBox.Name = "transGBox";
            this.transGBox.Padding = new System.Windows.Forms.Padding(0);
            this.transGBox.Size = new System.Drawing.Size(346, 137);
            this.transGBox.TabIndex = 0;
            this.transGBox.TabStop = false;
            this.transGBox.Text = "文本翻译";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logRTBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(226, 137);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统消息";
            // 
            // logRTBox
            // 
            this.logRTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRTBox.Location = new System.Drawing.Point(0, 16);
            this.logRTBox.Margin = new System.Windows.Forms.Padding(0);
            this.logRTBox.Name = "logRTBox";
            this.logRTBox.ReadOnly = true;
            this.logRTBox.Size = new System.Drawing.Size(226, 121);
            this.logRTBox.TabIndex = 0;
            this.logRTBox.Text = "";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountMangerTSMI,
            this.粉丝关注ToolStripMenuItem,
            this.消息群发ToolStripMenuItem,
            this.toolStripSeparator2,
            this.MinTSItem,
            this.MaxTSItem,
            this.zoomTSItem,
            this.toolStripSeparator1,
            this.quitTSMItem});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(87, 27);
            this.toolStripDropDownButton1.Text = "店 聊 通";
            this.toolStripDropDownButton1.ToolTipText = "店大麦 V2.0.0.5";
            // 
            // accountMangerTSMI
            // 
            this.accountMangerTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatShopGroupToolStripMenuItem1,
            this.addStoreTMSI,
            this.createTemplateTMSI,
            this.importDataTSMI,
            this.还原配置ToolStripMenuItem,
            this.networkSettingToolStripMenuItem});
            this.accountMangerTSMI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.accountMangerTSMI.Image = ((System.Drawing.Image)(resources.GetObject("accountMangerTSMI.Image")));
            this.accountMangerTSMI.ImageTransparentColor = System.Drawing.Color.White;
            this.accountMangerTSMI.Name = "accountMangerTSMI";
            this.accountMangerTSMI.Size = new System.Drawing.Size(188, 30);
            this.accountMangerTSMI.Text = "店铺设置(D)";
            // 
            // creatShopGroupToolStripMenuItem1
            // 
            this.creatShopGroupToolStripMenuItem1.Name = "creatShopGroupToolStripMenuItem1";
            this.creatShopGroupToolStripMenuItem1.Size = new System.Drawing.Size(134, 24);
            this.creatShopGroupToolStripMenuItem1.Text = "添加店群";
            // 
            // addStoreTMSI
            // 
            this.addStoreTMSI.Name = "addStoreTMSI";
            this.addStoreTMSI.Size = new System.Drawing.Size(134, 24);
            this.addStoreTMSI.Text = "添加店铺";
            // 
            // createTemplateTMSI
            // 
            this.createTemplateTMSI.Name = "createTemplateTMSI";
            this.createTemplateTMSI.Size = new System.Drawing.Size(134, 24);
            this.createTemplateTMSI.Text = "创建模板";
            // 
            // importDataTSMI
            // 
            this.importDataTSMI.Name = "importDataTSMI";
            this.importDataTSMI.Size = new System.Drawing.Size(134, 24);
            this.importDataTSMI.Text = "导入模板";
            // 
            // 还原配置ToolStripMenuItem
            // 
            this.还原配置ToolStripMenuItem.Name = "还原配置ToolStripMenuItem";
            this.还原配置ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.还原配置ToolStripMenuItem.Text = "清除配置";
            // 
            // networkSettingToolStripMenuItem
            // 
            this.networkSettingToolStripMenuItem.Name = "networkSettingToolStripMenuItem";
            this.networkSettingToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.networkSettingToolStripMenuItem.Text = "网络设置";
            // 
            // 粉丝关注ToolStripMenuItem
            // 
            this.粉丝关注ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.粉丝关注ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("粉丝关注ToolStripMenuItem.Image")));
            this.粉丝关注ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.粉丝关注ToolStripMenuItem.Name = "粉丝关注ToolStripMenuItem";
            this.粉丝关注ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.粉丝关注ToolStripMenuItem.Text = "粉丝关注(F)";
            // 
            // 消息群发ToolStripMenuItem
            // 
            this.消息群发ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.订单列表群发ToolStripMenuItem,
            this.买家群发ToolStripMenuItem,
            this.搜索群发ToolStripMenuItem});
            this.消息群发ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.消息群发ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("消息群发ToolStripMenuItem.Image")));
            this.消息群发ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.消息群发ToolStripMenuItem.Name = "消息群发ToolStripMenuItem";
            this.消息群发ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.消息群发ToolStripMenuItem.Text = "消息群发(Q)";
            // 
            // 订单列表群发ToolStripMenuItem
            // 
            this.订单列表群发ToolStripMenuItem.Name = "订单列表群发ToolStripMenuItem";
            this.订单列表群发ToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.订单列表群发ToolStripMenuItem.Text = "订单列表群发";
            // 
            // 买家群发ToolStripMenuItem
            // 
            this.买家群发ToolStripMenuItem.Name = "买家群发ToolStripMenuItem";
            this.买家群发ToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.买家群发ToolStripMenuItem.Text = "聊聊列表群发";
            // 
            // 搜索群发ToolStripMenuItem
            // 
            this.搜索群发ToolStripMenuItem.Name = "搜索群发ToolStripMenuItem";
            this.搜索群发ToolStripMenuItem.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
            // 
            // MinTSItem
            // 
            this.MinTSItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinTSItem.Image = ((System.Drawing.Image)(resources.GetObject("MinTSItem.Image")));
            this.MinTSItem.Name = "MinTSItem";
            this.MinTSItem.Size = new System.Drawing.Size(188, 30);
            this.MinTSItem.Text = "最小化";
            this.MinTSItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.MinTSItem.Click += new System.EventHandler(this.MinTSItem_Click);
            // 
            // MaxTSItem
            // 
            this.MaxTSItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaxTSItem.Image = ((System.Drawing.Image)(resources.GetObject("MaxTSItem.Image")));
            this.MaxTSItem.Name = "MaxTSItem";
            this.MaxTSItem.Size = new System.Drawing.Size(188, 30);
            this.MaxTSItem.Text = "最大化";
            this.MaxTSItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.MaxTSItem.Click += new System.EventHandler(this.MaxTSItem_Click);
            // 
            // zoomTSItem
            // 
            this.zoomTSItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zoomTSItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomTSItem.Image")));
            this.zoomTSItem.Name = "zoomTSItem";
            this.zoomTSItem.Size = new System.Drawing.Size(188, 30);
            this.zoomTSItem.Text = "还原";
            this.zoomTSItem.Click += new System.EventHandler(this.zoomTSItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // quitTSMItem
            // 
            this.quitTSMItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.quitTSMItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.quitTSMItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.quitTSMItem.Image = ((System.Drawing.Image)(resources.GetObject("quitTSMItem.Image")));
            this.quitTSMItem.Name = "quitTSMItem";
            this.quitTSMItem.Size = new System.Drawing.Size(188, 30);
            this.quitTSMItem.Text = "退出";
            this.quitTSMItem.Click += new System.EventHandler(this.quitTSMItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTMSI,
            this.帮助信息ToolStripMenuItem,
            this.软件版本ToolStripMenuItem});
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.关于ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("关于ToolStripMenuItem.Image")));
            this.关于ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(123, 30);
            this.关于ToolStripMenuItem.Text = "版本V2.0.0.1";
            // 
            // toolTMSI
            // 
            this.toolTMSI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DNSTSMI,
            this.ipconfigTSMI});
            this.toolTMSI.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolTMSI.Image = ((System.Drawing.Image)(resources.GetObject("toolTMSI.Image")));
            this.toolTMSI.ImageTransparentColor = System.Drawing.Color.White;
            this.toolTMSI.Name = "toolTMSI";
            this.toolTMSI.Size = new System.Drawing.Size(188, 30);
            this.toolTMSI.Text = "系统工具";
            // 
            // DNSTSMI
            // 
            this.DNSTSMI.Name = "DNSTSMI";
            this.DNSTSMI.Size = new System.Drawing.Size(162, 24);
            this.DNSTSMI.Text = "设置DNS";
            // 
            // ipconfigTSMI
            // 
            this.ipconfigTSMI.Name = "ipconfigTSMI";
            this.ipconfigTSMI.Size = new System.Drawing.Size(162, 24);
            this.ipconfigTSMI.Text = "查看网络设置";
            // 
            // 帮助信息ToolStripMenuItem
            // 
            this.帮助信息ToolStripMenuItem.Name = "帮助信息ToolStripMenuItem";
            this.帮助信息ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.帮助信息ToolStripMenuItem.Text = "帮助信息";
            // 
            // 软件版本ToolStripMenuItem
            // 
            this.软件版本ToolStripMenuItem.Name = "软件版本ToolStripMenuItem";
            this.软件版本ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.软件版本ToolStripMenuItem.Text = "软件版本";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(132, 27);
            this.toolStripLabel1.Text = "13425255929";
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripMenuItem1,
            this.关于ToolStripMenuItem,
            this.toolStripLabel1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(899, 36);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(36, 30);
            this.toolStripMenuItem1.ToolTipText = "关 闭";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.quitTSMItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transUISwitchBtn,
            this.toolStripStatusLabel1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 749);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(576, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // transUISwitchBtn
            // 
            this.transUISwitchBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.transUISwitchBtn.Image = ((System.Drawing.Image)(resources.GetObject("transUISwitchBtn.Image")));
            this.transUISwitchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transUISwitchBtn.Name = "transUISwitchBtn";
            this.transUISwitchBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.transUISwitchBtn.ShowDropDownArrow = false;
            this.transUISwitchBtn.Size = new System.Drawing.Size(108, 28);
            this.transUISwitchBtn.Text = "打开翻译界面";
            this.transUISwitchBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.transUISwitchBtn.Click += new System.EventHandler(this.transUISwitchBtn_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 0);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 816);
            this.ControlBox = false;
            this.Controls.Add(this.mainSPContainer);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.mainSPContainer.Panel1.ResumeLayout(false);
            this.mainSPContainer.Panel1.PerformLayout();
            this.mainSPContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSPContainer)).EndInit();
            this.mainSPContainer.ResumeLayout(false);
            this.chatSPContainer.Panel2.ResumeLayout(false);
            this.chatSPContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chatSPContainer)).EndInit();
            this.chatSPContainer.ResumeLayout(false);
            this.contentSplitPannel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contentSplitPannel)).EndInit();
            this.contentSplitPannel.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.NotifyIcon chatFormNotifyIcon;
        private System.Windows.Forms.ImageList imageListTabIcon;
        private System.Windows.Forms.SplitContainer mainSPContainer;
        private System.Windows.Forms.SplitContainer chatSPContainer;
        private System.Windows.Forms.SplitContainer contentSplitPannel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox transGBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox logRTBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem accountMangerTSMI;
        private System.Windows.Forms.ToolStripMenuItem creatShopGroupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addStoreTMSI;
        private System.Windows.Forms.ToolStripMenuItem createTemplateTMSI;
        private System.Windows.Forms.ToolStripMenuItem importDataTSMI;
        private System.Windows.Forms.ToolStripMenuItem 还原配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粉丝关注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消息群发ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订单列表群发ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 买家群发ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator 搜索群发ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MinTSItem;
        private System.Windows.Forms.ToolStripMenuItem MaxTSItem;
        private System.Windows.Forms.ToolStripMenuItem zoomTSItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitTSMItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolTMSI;
        private System.Windows.Forms.ToolStripMenuItem DNSTSMI;
        private System.Windows.Forms.ToolStripMenuItem ipconfigTSMI;
        private System.Windows.Forms.ToolStripMenuItem 帮助信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 软件版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton transUISwitchBtn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}