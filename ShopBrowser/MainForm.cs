

using Common.Brower;
using CefSharp;
using CefSharp.WinForms;
using Common.Collector;
using Common.Collector.T1688;
using Common.Infterface;
using Common.Shopee.API.Data;
using Common.Tools;
using CommonData.SysData.Enum;
using Microsoft.Win32;
using ShopBrowser.Properties;
using ShopeeChat.Shopee;
using ShopeeChat.Shopee.API;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using ShopeeChat.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using Common.UI;
using ShopChatPlus.View;
using Common.Browser;

namespace ShopeeChat
{
    public partial class MainForm : Form
    {
        string thisUserName;
        int updateTimeSpan = 1000 * 150;
        string version = "";
        List<IAppPlug> appPlugs = new List<IAppPlug>();
        ShopTreeView storeTreeView = new ShopTreeView();
        ShopeeChatView chatView = new  ShopeeChatView();
        public MainForm(string user)
        {
            this.thisUserName = user;
            InitializeComponent();
            chatSPContainer.Panel1.Controls.Add(storeTreeView);
            storeTreeView.Dock = DockStyle.Fill;
            storeTreeView.StoreSelected += LoadStoreChat;
            contentSplitPannel.Panel1.Controls.Add(chatView);
            chatView.Dock = DockStyle.Fill;

            TextLogHelper.Instance.Initialize(logRTBox);
            Console.SetOut(TextLogHelper.Instance);
            version = AccessControl.Instance.Version;
            //this.Text = "店大麦(ShopBrowser) 版本号:" + version + " " + AccessControl.Instance.UserLeveLName();
            loadApp();
        }

        AttachedMessageDialog tranMsgDialog = null;
        public void LoadStoreChat(StoreGroup group,StoreRegion region,Store Store)
        {
           
            try
            {
                if (Store.LogStatus == LoginStatus.UnLog)
                {
                    MessageBox.Show("店铺还未登录，请稍后：" + Store.LogMessage[Store.LogStatus]);
                }
                else if (Store.LogStatus != LoginStatus.Log_Succuss)
                {
                    MessageBox.Show("店铺登录出现错误错误，需要：" + Store.LogMessage[Store.LogStatus]);
                    CaptchaLoginForm addStore = new CaptchaLoginForm(GroupConfigHelper.Instatce.GetStoreGroup(Store), Store, storeTreeView.UpdateGroupDisplay);
                    addStore.ShowDialog();
                }
                ChromiumWebBrowser curBorwer= chatView.LoadChat(region, Store);
                if(null == curBorwer)
                {
                    return;
                }
                if (tranMsgDialog == null)
                {
                    tranMsgDialog = new AttachedMessageDialog(Store, curBorwer);
                    transGBox.Controls.Add(tranMsgDialog);
                    tranMsgDialog.Dock = DockStyle.Fill;
                }
                else
                {
                    tranMsgDialog.SetTargerWebBrower(Store, curBorwer);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace.ToString());
            }
           
        }
        private void ChatForm_Load(object sender, EventArgs e)
        {
            loadAppMenu();
            openHelp();
            verfiyAccountStatus();
        }
        private void verfiyAccountStatus()
        {
            string rUrl = "";
            string msg = "";
            switch(AccessControl.Instance.UserStatus)
            {
                case UserStatus.Arrears:
                    {
                        rUrl = "http://www.uc.dianliaotong.com/#/cash/recharge";
                        msg = "您的账户已欠费，请到用户中心充值。在您账户余额小于0的状态下，我们将无法为您保存店铺配置。";
                        break;
                    }
                case UserStatus.AvactivePending:
                    {
                        rUrl = "http://www.uc.dianliaotong.com/#/cash/reactive";
                        msg = "您的账户尚未验证，请到用户中心“账务管理->账户验证”进行验证，在账户验证之前，我们将无法为您保存店铺配置。";
                        break;
                    }
                case UserStatus.Forbidden:
                    {
                        rUrl = "http://www.uc.dianliaotong.com";
                        msg = "您的账户被禁用，请到用户中心查看或联系管理员。";
                        break;
                    }
                case UserStatus.All:
                    {
                        rUrl = "http://www.uc.dianliaotong.com";
                        msg = "您的账户状态为非正常，请到用户中心查看或联系管理员。";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            DialogResult result = DialogResult.Yes;
            if(msg != "")
            {
                result = MessageBox.Show(msg, "账户控制", MessageBoxButtons.YesNo);
            }
            if(rUrl != "")
            {
                BrowerHelper.Instatce.OpenURLViaSysBrower(rUrl);
            }
        }
        private void loadAppMenu()
        {
            //mainToolStrip.Items.Clear();
            ////mainTabControl.TabPages.Clear();
            ////imageListTabIcon.Images.Clear();
            //foreach (IAppPlug app in appPlugs)
            //{
            //    if (app.InButton)
            //    {
            //        if (mainToolStrip.Items.Count > 0 && mainToolStrip.Items.Count % 3 == 0)
            //        {
            //            ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
            //            toolStripSeparator.Margin = new Padding(5,0,5,0);
            //            mainToolStrip.Items.Add(toolStripSeparator);
            //        }
            //        ///加入按钮图标
            //        ToolStripButton tsBtn = new ToolStripButton();
            //        tsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            //        tsBtn.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //        tsBtn.Image = app.Icon;
            //        tsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            //        tsBtn.Name = app.Name;
            //        //tsBtn.Size = new System.Drawing.Size(44, 44);
            //        tsBtn.Text = app.Name;
            //        tsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            //        tsBtn.Tag = app;
            //        tsBtn.Click += App_Click;
            //        mainToolStrip.Items.Add(tsBtn);




            //        //imageListTabIcon.Images.Add(app.Icon);
            //        //TabPage tp = new TabPage();
            //        //tp.ImageIndex = imageListTabIcon.Images.Count -1;
            //        ////tp.Location = new System.Drawing.Point(4, 68);
            //        //tp.Name = app.Name;
            //        //tp.Text = app.Name;
            //        ////tp.Padding = new System.Windows.Forms.Padding(3);
            //        ////tp.Size = new System.Drawing.Size(1404, 715);
            //        //tp.TabIndex = imageListTabIcon.Images.Count - 1;
            //        //tp.UseVisualStyleBackColor = true;
            //        //tp.Controls.Clear();
            //        //tp.Enter += Tp_Enter;
            //        //tp.Tag = app;
            //        //mainTabControl.TabPages.Add(tp);
            //    }
            //    if(app.InMenu)
            //    {
            //        ///加入菜单栏
            //        if (app.Menu.Length > 3 && app.Menu.Contains("|") && !app.Menu.StartsWith("|"))
            //        {
            //            ToolStripItem[] firstLevelTSItems = menuStrip.Items.Find(app.Menu.Split('|')[0], false);
            //            ToolStripMenuItem firstLevelTSItem = null;
            //            if (firstLevelTSItems == null || firstLevelTSItems.Length < 1)
            //            {
            //                firstLevelTSItem = new ToolStripMenuItem(app.Menu.Split('|')[0]);
            //                firstLevelTSItem.Name = app.Menu.Split('|')[0];

            //                firstLevelTSItem.Font = new System.Drawing.Font("微软雅黑", 10F);
            //                firstLevelTSItem.Image = app.Icon;
            //                firstLevelTSItem.ImageTransparentColor = System.Drawing.Color.White;
            //                firstLevelTSItem.Size = new System.Drawing.Size(94, 28);
            //                firstLevelTSItem.Text = app.Menu.Split('|')[0]; ;
            //                menuStrip.Items.Insert(menuStrip.Items.Count -1, firstLevelTSItem);
            //            }
            //            else
            //            {
            //                firstLevelTSItem = (ToolStripMenuItem)firstLevelTSItems[0];
            //            }

            //            ToolStripItem[] secondLevelTSItems = firstLevelTSItem.DropDownItems.Find(app.Menu.Split('|')[1], false);
            //            String secondeItemName = app.Menu.Split('|')[1];
            //            if (secondLevelTSItems != null && secondLevelTSItems.Length > 0)
            //            {
            //                secondeItemName += "[同名]" + DateTime.Now.ToShortDateString();
            //            }
            //            ToolStripMenuItem secondLevelTSItem = new ToolStripMenuItem(secondeItemName);
            //            secondLevelTSItem.Image = app.Icon;
            //            secondLevelTSItem.Name = secondeItemName;
            //            secondLevelTSItem.Tag = app;
            //            secondLevelTSItem.Click += App_Click;
            //            firstLevelTSItem.DropDownItems.Add(secondLevelTSItem);
            //        }
            //    }
                
            //}
        }

     

        private void App_Click(object sender, EventArgs e)
        {
            //ToolStripItem ctl = (ToolStripItem)sender;
            //if (ctl.Tag is IAppPlug)
            //{
            //    IAppPlug app = ctl.Tag as IAppPlug;
            //    switch (app.AppType)
            //    {
            //        case AppType.Inner:
            //            {
            //                TabPage appTabP = null;
            //                Control[] tabs = mainTabControl.Controls.Find(app.Name, false);
            //                if (tabs.Length > 0 && !app.MultiInstance)
            //                {
            //                    appTabP = (TabPage)tabs[0];
            //                }
            //                else
            //                {
            //                    imageListTabIcon.Images.Add(app.Icon);
            //                    appTabP = new TabPage(app.Name + (app.MultiInstance ? "_" + (tabs.Length + 1) : ""));
            //                   // appTabP.ImageIndex = imageListTabIcon.Images.Count - 1;
            //                    appTabP.Name = app.Name;
            //                    appTabP.ToolTipText = app.Name;
            //                    mainTabControl.Controls.Add(appTabP);
            //                    appTabP.Controls.Add(app.MainForm);
            //                    app.MainForm.Dock = DockStyle.Fill;


            //                }
            //                mainTabControl.SelectedTab = appTabP;
            //                break;
            //            }
            //        case AppType.PopUp:
            //            {
            //                AppPopUpForm form = new AppPopUpForm(app.MainForm);
            //                form.Name = app.Name;
            //                form.Text = app.Name;
            //                form.ShowDialog();
            //                break;
            //            }
            //        default:
            //            MessageBox.Show("启动程序，未实现！");
            //            break;
            //    }
            //}
        }
        private void TranMsgDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            tranMsgDialog = null;
        }



        //string[] template = new string[] { "region", "usename", "password", "sellerurl","shopgroup" };
        private void createTemplateTMSI_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                try
                {
                    //CVSOperator.CreateCSV(saveFileDialog.FileName, template);

                    DataTable dt_ShopInfo = new DataTable();
                    dt_ShopInfo.Columns.Add("region");//地区名[台湾,新加坡,马来西亚,泰国,印度尼西亚,菲律宾,越南]
                    dt_ShopInfo.Columns.Add("usename");//虾皮后台登录名
                    dt_ShopInfo.Columns.Add("password");//后台登录密码
                    dt_ShopInfo.Columns.Add("sellerurl");//后台网址[可忽略不填]
                    dt_ShopInfo.Columns.Add("shopgroup"); //店群名[一定跟添加的店群名一致，如未添加可不填]
                    NPOIOperator.SaveDataTableToExcel(saveFileDialog.FileName, dt_ShopInfo);
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void importDataTSMI_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Filter = "Excel文件(*.xls)|*.xls";
            if (DialogResult.OK == openfileDialog.ShowDialog())
            {
                try
                {
                    //Dictionary<string, List<String>> datas = CVSOperator.OpenCSV(openfileDialog.FileName);
                    Dictionary<string, List<String>> datas = new Dictionary<string, List<string>>();
                    DataTable dt = NPOIOperator.ExcelToDataTable(openfileDialog.FileName);
                    datas.Add("region", new List<string>());
                    datas.Add("usename", new List<string>());
                    datas.Add("password", new List<string>());
                    datas.Add("sellerurl", new List<string>());
                    datas.Add("shopgroup", new List<string>());
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        string rt = dt.Rows[i][0].ToString().Trim();
                        string ut = dt.Rows[i][1].ToString().Trim();
                        string pt = dt.Rows[i][2].ToString().Trim();
                        string set = dt.Rows[i][3].ToString().Trim();
                        string sht = dt.Rows[i][4].ToString().Trim();
                        if (rt.Equals("台湾") || rt.Equals("新加坡") || rt.Equals("马来西亚") || rt.Equals("泰国")
    ||                      rt.Equals("印度尼西亚") || rt.Equals("菲律宾") || rt.Equals("越南"))
                        {
                            if (!string.IsNullOrEmpty(ut) && !string.IsNullOrEmpty(pt))
                            {
                                datas["region"].Add(rt);
                                datas["usename"].Add(ut);
                                datas["password"].Add(pt);
                                datas["sellerurl"].Add(set);
                                datas["shopgroup"].Add(sht);
                            }
                        }
                    }


                    GroupConfigHelper.Instatce.ClearStoreData();

                    List<StoreGroup> shopGroups = GroupConfigHelper.Instatce.Groups;
                    for (int i = 0; i < datas["region"].Count; i++)
                    {
                        string country = datas["region"][i];
                        StoreGroup group = null;
                        if(datas["shopgroup"][i] == null || datas["shopgroup"][i] == "" || shopGroups.Count == 1)
                        {
                            group =  shopGroups[0];
                        }
                        else if (shopGroups.Count > 1)
                        {
                            group = shopGroups.FirstOrDefault(p => p.GroupName == datas["shopgroup"][i]);
                        }
                        if(null == group)
                        {
                            MessageBox.Show("店群\"" + datas["shopgroup"][i]+"\"尚未在系统中设置，请前往店群设置中设置后在导入！");
                            return;
                        }

                        StoreRegion region = group.Regions.FirstOrDefault(p => p.RegionName == country);
                        if (region == null)
                        {
                            //region = new Region(country, datas["sellerurl"][i]);
                            //storeDatas.Add(region);
                            MessageBox.Show("不正确的地区名称:" + country);
                            return;
                        }

                        Store strinfo = region.Stores.FirstOrDefault(p => p.UserName == datas["usename"][i]);
                        if (null == strinfo)
                        {
                            strinfo = new Store(StoreRegionMap.GetRegionID(datas["region"][i]),datas["usename"][i], datas["password"][i]);
                            region.Stores.Add(strinfo);
                        }
                        else
                        {
                            strinfo.SetValue(StoreRegionMap.GetRegionID(datas["region"][i]), datas["usename"][i], datas["password"][i]);
                        }
                    }
                    LogInForm logForm = new LogInForm("用户验证",true);
                    logForm.ShowDialog();
                    if (!logForm.IsLogSuccess)
                    {
                        GroupConfigHelper.Instatce.ClearStoreData();
                      
                    }
                    GroupConfigHelper.Instatce.Save();
                    storeTreeView.InitTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入数据错误："+ ex.Message);
                }
            }
        }
   
       


        private void CreatShopGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddShopGroupForm addGroupForm = new AddShopGroupForm();
            addGroupForm.ShowDialog();
            if (null != addGroupForm.ShopGroup)
            {
                addGroupForm.ShopGroup.Regions = StoreRegionMap.GetRegionList();
                GroupConfigHelper.Instatce.Groups.Add(addGroupForm.ShopGroup);
            }

            storeTreeView.InitTreeView();
            //MessageBox.Show("如需使用，加入QQ群了解更多!");
            //MessageBox.Show("本功能可以创建店群，所有店群在所有功能页面上统一管理。本功能仅对注册超过两个月的用户开放！");
        }

        private void NetworkSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShopGroupSettingForm setForm = new ShopGroupSettingForm(GroupConfigHelper.Instatce.Groups);
            setForm.ShowDialog();
            storeTreeView.InitTreeView();
            //MessageBox.Show("如需使用，加入QQ群了解更多!");
            //MessageBox.Show("本功能可以为店群设置代理服务器，不同店群可以使用不同代理服务器，本功能仅对注册超过两个月的用户开放!");
        }


        private void 买家群发ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("卖家可以使用本功能同时（定时）给多个卖家发送消息,消息可以自动翻译，注册时间小于两个月的用户仅能给当前列表的第一个用户发送消息");
            //MessageBox.Show("如需使用，加入QQ群了解更多!");
            TreeNode node = storeTreeView.SelectedNode;

            if (node != null && node.Tag is Store)
            {
                Store storeinfo = storeTreeView.SelectedNode.Tag as Store;
                if (storeinfo.CustomerInfos.Count > 0)
                {
                    ShopCustomerInfo customerInfo = storeinfo.CustomerInfos[0];
                    MessageDialog msgDlg = new MessageDialog(storeinfo);
                    msgDlg.Show();
                    msgDlg.SetCustomer(storeinfo, customerInfo);
                    msgDlg.Visible = true;
                }
                else
                {
                    MessageBox.Show("聊天列表加载中，稍后再试！");
                }
            }
        }



        private void CreatShopGroupToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            //AddShopGroupForm asgf = new AddShopGroupForm();
            //asgf.ShowDialog();
            AddShopGroupForm addGroupForm = new AddShopGroupForm();
            addGroupForm.ShowDialog();
            if (null != addGroupForm.ShopGroup)
            {
                addGroupForm.ShopGroup.Regions = StoreRegionMap.GetRegionList();
                GroupConfigHelper.Instatce.Groups.Add(addGroupForm.ShopGroup);
            }

            storeTreeView.InitTreeView();
            GroupConfigHelper.Instatce.Save();
        }

        private void NetworkSettingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShopGroupSettingForm netSettingForm = new ShopGroupSettingForm(GroupConfigHelper.Instatce.Groups);
            netSettingForm.ShowDialog();
        }
   

        private void 帮助信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //openHelp();
        }
        private void openHelp()
        {
            //string helpUrl = "http://www.dianliaotong.com/news/news.html";// "http://www.dianliaotong.com/news.html";
            //WelcomeForm welcome = new WelcomeForm(helpUrl);
        }

        private void 软件版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }
 

        private void DNSTSMI_Click(object sender, EventArgs e)
        {
            MachineInfo.Instance.SetIPAddress(null,null,null,new string[]{ "114.114.114.114","8.8.8.8"});
            MessageBox.Show(getIPCoinfig());
        }

        private void ipconfigTSMI_Click(object sender, EventArgs e)
        {
            MessageBox.Show(getIPCoinfig());
        }
        private string getIPCoinfig()
        {
            //转载请注明来自 http://www.uzhanbao.com
            Process cmd = new Process();
            cmd.StartInfo.FileName = "ipconfig.exe";//设置程序名   
            cmd.StartInfo.Arguments = "/all";  //参数   
                                               //重定向标准输出   
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;//不显示窗口（控制台程序是黑屏）   
                                                //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//暂时不明白什么意思   
                                                /* 
                                         收集一下 有备无患 
                                                关于:ProcessWindowStyle.Hidden隐藏后如何再显示？ 
                                                hwndWin32Host = Win32Native.FindWindow(null, win32Exinfo.windowsName); 
                                                Win32Native.ShowWindow(hwndWin32Host, 1);     //先FindWindow找到窗口后再ShowWindow 
                                                */
            cmd.Start();
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            return info;
        }

        private void addStoreTMSI_Click(object sender, EventArgs e)
        {
            AddStoreForm addStore = new AddStoreForm(GroupConfigHelper.Instatce.Groups);
            addStore.ShowDialog();
            storeTreeView.InitTreeView();
        }

       

  
        private void registedTSM_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;
            if (node != null && node.Tag is StoreGroup)
            {
                PopInputForm input = new PopInputForm();
                if(DialogResult.OK == input.ShowDialog())
                {

                    string URL = input.Code;
                    StoreRegion region = input.Region;
                    StoreGroup group = node.Tag as  StoreGroup;
                    PopBrowerForm popBrower = new PopBrowerForm(group, region, URL);
                    popBrower.Show();
                }
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个店群！");
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            quitForm();
            e.Cancel = true;
        }
        private void quitForm()
        {
            DialogResult result = MessageBox.Show("是否退出？选否,最小化到托盘", "操作提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else if (result == DialogResult.No)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
            }
        }
        private void chatFormNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
        }

        private void 定向群发ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;
            if (node != null && node.Tag is Store)
            {
                Store storeINfo = node.Tag as Store;
                TreeNode countryNode = node.Parent;
                StoreRegion region = countryNode.Tag as StoreRegion;
                SearchedMessageForm searMsg = new SearchedMessageForm(region,storeINfo);
                searMsg.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
           
        }

      

        private void 订单列表群发ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;

            if (node != null && node.Tag is Store)
            {
                Store storeinfo = storeTreeView.SelectedNode.Tag as Store;

                OrderMessageDialog msgDlg = new OrderMessageDialog(storeinfo);
                msgDlg.Show();
                //msgDlg.FormClosing += MsgDlg_FormClosing;
                msgDlg.Visible = true;
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
        }


        private void 定向关粉ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;
            if (node != null && node.Tag is Store)
            {
                Store storeINfo = node.Tag as Store;
                TreeNode countryNode = node.Parent;
                StoreRegion region = countryNode.Tag as StoreRegion;
                if (storeINfo.ShopInfo != null && storeINfo.ShopInfo.user != null)
                {
                    storeINfo.CcrsfToken = string.Empty;
                    //loadFollowing(region, storeINfo);

                }
                else
                {
                    MessageBox.Show("尚未准备就绪，请稍后再试！");
                }
            }
            else
            {
                MessageBox.Show("请选择一个账号！");
            }
        }

        private void 关键词关粉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;

            if (node != null && node.Tag is Store)
            {
                Store storeINfo = node.Tag as Store;
                TreeNode countryNode = node.Parent;
                ShopeeAPI aPI = new ShopeeAPI();
                if (!aPI.IsLogin(storeINfo))
                {
                    MessageBox.Show("请等待登录成功！");
                    return;
                }
                StoreRegion region = countryNode.Tag as StoreRegion;
                KeywordFollowForm form = new KeywordFollowForm(region, storeINfo);
                if (!AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
        }


     
        void loadApp()
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo fi in di.GetFiles("App.*.dll"))
            {
                Assembly lib = Assembly.LoadFrom(fi.FullName);
                foreach (Type t in lib.GetExportedTypes())
                {
                    if (t.GetInterface(typeof(IAppPlug).FullName) != null)
                    {
                        IAppPlug plug = (IAppPlug)Activator.CreateInstance(t);
                        plug.Initialize(GroupConfigHelper.Instatce);
                        appPlugs.Add(plug);
                    }
                }
            }
        }
        private void 还原配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogInForm logForm = new LogInForm("清除数据-用户验证", true);
            logForm.ShowDialog();
            if (logForm.IsLogSuccess)
            {
                GroupConfigHelper.Instatce.ClearStoreData();
                GroupConfigHelper.Instatce.Save();
                storeTreeView.InitTreeView();
            }

        }

        private void 粉丝关注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;

            if (node != null && node.Tag is Store)
            {
                Store storeINfo = node.Tag as Store;
                TreeNode countryNode = node.Parent;
                ShopeeAPI aPI = new ShopeeAPI();
                if (!aPI.IsLogin(storeINfo))
                {
                    MessageBox.Show("请等待登录成功！");
                    return;
                }
                StoreRegion region = countryNode.Tag as StoreRegion;
                KeywordFollowForm form = new KeywordFollowForm(region, storeINfo);
                if (!AccessControl.Instance.IsLevelRight(UserLevel.VIPUser))
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
        }

        private void quitTSMItem_Click(object sender, EventArgs e)
        {
            quitForm();
        }

        private void AboutTSLabel_Click(object sender, EventArgs e)
        {

        }

        private void MinTSItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
        }

        private void MaxTSItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void zoomTSItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void transUISwitchBtn_Click(object sender, EventArgs e)
        {
            contentSplitPannel.Panel2Collapsed = !contentSplitPannel.Panel2Collapsed;
            transUISwitchBtn.Text = (contentSplitPannel.Panel2Collapsed ? "打开" : "关闭") + "翻译界面";
        }
    }
}
