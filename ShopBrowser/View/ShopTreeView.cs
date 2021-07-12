

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
using ShopeeChat;

namespace ShopChatPlus.View
{
    public partial class ShopTreeView : UserControl
    {
        public delegate void StoreEventHandler(StoreGroup curGroup,StoreRegion curRetion, Store curStore);

        public StoreEventHandler StoreSelected;
        public TreeNode SelectedNode = null;

        public ShopTreeView()
        {
            InitializeComponent();
        }
        public void InitTreeView()
        {
            initTreeView();
        }
        private void ChatForm_Load(object sender, EventArgs e)
        {
            initTreeView();
            GroupConfigHelper.Instatce.RegistStoreInfoUpdateHanlder(UpdateGroupDisplay);
        }

        private void storeTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.SelectedNode = e.Node;
            TreeNode node = e.Node;
            if (node != null && node.Tag is Store)
            {
                Store storeINfo = node.Tag as Store;
                TreeNode countryNode = node.Parent;
                StoreRegion region = countryNode.Tag as StoreRegion;
                TreeNode groupNode = countryNode.Parent;
                StoreGroup group = groupNode.Tag as StoreGroup;
                if (null != StoreSelected)
                {
                    StoreSelected(group, region, storeINfo);
                }
            }
            
        }

      
        private void initGroupTreeView(StoreGroup group,TreeNode groupNode)
        {
            foreach (StoreRegion region in group.Regions)
            {
                if(region.Stores.Count <= 0)
                {
                    continue;
                }
                TreeNode regionNode = new TreeNode(region.RegionName + "(店铺数:" + region.Stores.Count + ")");
                groupNode.Nodes.Add(regionNode);
                regionNode.ImageIndex = 2;
                regionNode.Tag = region;
                foreach (Store storeinof in region.Stores)
                {
                    storeinof.Token = null;
                    TreeNode storeNode;
                    if (group.Plateform==1 && region.RegionID != "gl")
                    {
                        storeNode = new TreeNode(storeinof.DisplayName + " [准备登录,请稍后]");
                    }
                    else
                    {
                        storeNode = new TreeNode(storeinof.DisplayName + " [已绑定]");
                    }
                    storeNode.ImageIndex = 4;
                    storeNode.Tag = storeinof;
                    regionNode.Nodes.Add(storeNode);
                    //loadChat(region, storeinof);
                }
                //addRegionWebBrower(region);
                if (region.Stores.Count > 0)
                {
                    //loadChat(region, region.Stores[0]);
                }
            }
        }
        bool isNewMesage = false;
        bool isNewOrder = false;


        private void initTreeView()
        {
            //regionWebs.Clear();
            //stopUpdateTimer();
            storeTreeView.Nodes.Clear();
            //upateUnreadInfo(GroupConfig.Instatce.Groups);
            foreach (StoreGroup group in GroupConfigHelper.Instatce.Groups)
            {
                TreeNode groupNode = new TreeNode(group.GroupName);
                groupNode.ImageIndex = 0;
                groupNode.Tag = group;
                initGroupTreeView(group, groupNode);
                storeTreeView.Nodes.Add(groupNode);
            }
            storeTreeView.ExpandAll();
        }
   
        void upateTreeNodeIcoDisplay(TreeNode node,String name,string url)
        {
            if (node.ImageIndex > 3 ||url == null)
            {
                return;
            }
            int defaultImg = 0;

            if (node != null && node.Tag is ShopCustomerInfo)
            {
                defaultImg = 2;
            }
           
            try
            {
                    node.ImageIndex = 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
      
        void updateStoreDisplay(Store store)
        {
            foreach (TreeNode regionNode in storeTreeView.Nodes)
            {
                foreach (TreeNode treeStore in regionNode.Nodes)
                {
                    if ((treeStore.Tag as Store).UserName == store.UserName)
                    {
                        updateStoreTreeDisplay(treeStore);
                    }
                }
            }
        }

        void updateRegionDisplay(StoreRegion region)
        {
            foreach (TreeNode groupNode in storeTreeView.Nodes)
            {
                foreach(TreeNode regionNode in groupNode.Nodes)
                {
                    if ((regionNode.Tag as StoreRegion).RegionName == region.RegionName)
                    {
                        updatetRegionTreeDisplay(regionNode);
                    }
                }
            }
        }
        delegate void updateGroupDisplayDele(StoreGroup group);
        public void UpdateGroupDisplay(StoreGroup group)
        {
            if(storeTreeView.InvokeRequired)
            {
                storeTreeView.Invoke(new updateGroupDisplayDele(UpdateGroupDisplay), new object[] { group });
            }
            else
            {
                try
                {
                    foreach (TreeNode groupNode in storeTreeView.Nodes)
                    {
                        if ((groupNode.Tag as StoreGroup).GroupName == group.GroupName)
                        {
                            updateGroupTreeDisplay(groupNode);
                        }
                    }
                }
                catch (Exception xe)
                {
                    Console.WriteLine(xe.Message);
                }
            }
        }
        void updateStoreTreeDisplay(TreeNode storeNode)
        {
            Store storeInfo = storeNode.Tag as Store;
            updateNoSIPStoreTreeDisplay(storeNode);
            if (storeInfo.IsSIP)
            {
                updateSIPStoreTreeDisplay(storeNode);
            }
           
        }
        void updateSIPStoreTreeDisplay(TreeNode storeNode)
        {
            try
            {
                Store storeInfo = storeNode.Tag as Store;
                storeNode.Nodes.Clear();
                foreach (string  region in storeInfo.SIPOrderSummaryInfos.Keys)
                {
                    OrderSummaryInfo sumInfo = storeInfo.SIPOrderSummaryInfos[region];
                    if (null != sumInfo && sumInfo.toship > 0)
                    {
                        TreeNode sipStoreNode = new TreeNode();
                        sipStoreNode.Text = StoreRegionMap.GetRegionName(region) + " ["+ sumInfo.toship + "个订单]";
                        storeNode.Nodes.Add(sipStoreNode);
                        sipStoreNode.ImageIndex = 8;
                    }
                }
                storeNode.Text = "[SIP]" + storeNode.Text;
                storeNode.ExpandAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新消息数:" + ex.Message);
            }
        }
        void updateNoSIPStoreTreeDisplay(TreeNode storeNode)
        {
            try
            {
                 Store storeInfo = storeNode.Tag as Store;
                string prevTxt = storeInfo.IsLocalAccount ? "[本地]" : "";
                if (storeInfo.UnReadCount > 0 || storeInfo.Token != null)
                {
                    if(storeInfo.OrderSummaryInfo.toship > 0)
                    {
                        storeNode.Text = prevTxt+storeInfo.DisplayName + " [" + storeInfo.UnReadCount + "条未读," + storeInfo.OrderSummaryInfo.toship + "个订单]";
                    }
                    else
                    {
                        storeNode.Text = prevTxt + storeInfo.DisplayName + " [" + storeInfo.UnReadCount + "条未读]";
                    }
                    
                    storeNode.ForeColor = storeInfo.UnReadCount > 0 ? Color.Red : Color.Black;
                    if (null != storeInfo.ShopInfo && null != storeInfo.ShopInfo.user)
                    {
                        upateTreeNodeIcoDisplay(storeNode, storeInfo.UserName, storeInfo.ShopInfo.user.avatar);
                    }
                }
                else if (storeInfo.LogStatus != LoginStatus.Log_Succuss && storeInfo.LogStatus != LoginStatus.UnLog)
                {
                    storeNode.Text = prevTxt + storeInfo.DisplayName + " "+storeInfo.LogMessage[storeInfo.LogStatus] +"[请点击这里手动登录]";
                    //storeNode.BackColor = Color.BlueViolet; ;
                    storeNode.ForeColor = Color.BlueViolet;
                }
                else
                {
                    storeNode.Text = prevTxt + storeInfo.DisplayName + " [尝试登录中]";
                    //storeNode.BackColor = Color.BlueViolet; ;
                    storeNode.ForeColor = Color.BlueViolet;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("更新消息数:" + ex.Message);
            }
        }
        void updatetRegionTreeDisplay(TreeNode regionNode)
        {
            foreach (TreeNode storeNode in regionNode.Nodes)
            {
                updateStoreTreeDisplay(storeNode);
            }
        }
        void updateGroupTreeDisplay(TreeNode groupNode)
        {
            foreach (TreeNode regionNode in groupNode.Nodes)
            {
                updatetRegionTreeDisplay(regionNode);
            }
        }
        void updateTreeDisplay()
        {
            try
            {
                foreach (TreeNode groupNode in storeTreeView.Nodes)
                {
                    updateGroupTreeDisplay(groupNode);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        private void storeTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.Node == null) return;
            if(null == storeTreeView.SelectedNode)
            {
                return;
            }
        }
   
       


        private void copyShopIDTSM_Click(object sender, EventArgs e)
        {
            TreeNode node = storeTreeView.SelectedNode;
            if (node != null && node.Tag is Store)
            {
                try
                {
                    Store store = node.Tag as Store;
                    Clipboard.SetText(store.ShopID.ToString());
                    MessageBox.Show("复制剪切板的shopID：" + Clipboard.GetText() + ",请直接使用Ctrl+V粘贴！");
                }
                catch(Exception xe)
                {
                    MessageBox.Show("复制失败："+xe.Message);
                }
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个店铺！");
            }
        }
        private void delteGroupTSM_Click(object sender, EventArgs e)
        {
            if (storeTreeView.SelectedNode.Tag is StoreGroup)
            {
                GroupConfigHelper.Instatce.Groups.Remove(storeTreeView.SelectedNode.Tag as StoreGroup);
                storeTreeView.SelectedNode.TreeView.Nodes.Remove(storeTreeView.SelectedNode);
                GroupConfigHelper.Instatce.Save();
            }
            else
            {
                MessageBox.Show("请选择一个店群节点！");
            }
        }
        private void addStoreTsBtn_Click(object sender, EventArgs e)
        {
            AddStoreForm addStore = new AddStoreForm(GroupConfigHelper.Instatce.Groups);
            addStore.ShowDialog();
            this.InitTreeView();
        }

        private void addGroupBtn_Click(object sender, EventArgs e)
        {
            AddShopGroupForm addGroupForm = new AddShopGroupForm();
            addGroupForm.ShowDialog();
            if (null != addGroupForm.ShopGroup)
            {
                addGroupForm.ShopGroup.Regions = StoreRegionMap.GetRegionList();
                GroupConfigHelper.Instatce.Groups.Add(addGroupForm.ShopGroup);
            }

            this.InitTreeView();
        }

    }
}
