
using CefSharp.WinForms;
using Common.Brower;
using Common.Browser;
using SharpBrowser;
using ShopBrowser.Properties;
using ShopeeChat.SysData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ShopChatPlus.View
{
    public partial class ShopeeChatView : UserControl
    {
        Store curStore = null;
        StoreRegion curRegion = null;
        StoreWebBrowser curBrowser = null;

        BrowserMainControl storeWebs ;
        public ShopeeChatView()
        {
            InitializeComponent();
            BrowserMainControl browser = new BrowserMainControl();
            browser.Dock = DockStyle.Fill;
            chatMainSPtContainer.Panel2.Controls.Add(browser);
            storeWebs = browser;
        }

        private void clearWebBrower()
        {
            //clearBrowrContainer();
            storeWebs.Clear();
            return;
        }

        
        //private StoreWebBrowser newShopGroupWebBrower(Store baseStore)
        //{
        //    StoreGroup group = GroupConfigHelper.Instatce.GetStoreGroup(baseStore);

        //    //if (storeWebs.Keys.Contains(baseStore))
        //    //{
        //    //    return storeWebs[baseStore];
        //    //}

        //    string initUrl = baseStore.ServerURL+ "/webchat/conversations";
        //    //string initUrl = baseStore.ServerURL + "/account/signin?next=%2F";
        //    String cacheDir = BrowerHelper.Instatce.GetCacheDir(group, baseStore);
        //    storeWebs.AddNewBrowserTab();
        //    StoreWebBrowser webBrower = new StoreWebBrowser(group,baseStore, cacheDir, initUrl);
        //    webBrower.Name =baseStore.DisplayName;
        //    //initBrowerToContainer(webBrower);

        //    return webBrower;
        //}

      /// <summary>
      /// 清理并释放浏览器资源
      /// </summary>
      /// <param name="webBrower"></param>
        void clearBrower()
        {
            //foreach(StoreWebBrowser storeWeb in storeWebs.Values)
            //{
            //    storeWeb.Dispose();
            //}
            storeWebs.Clear();
        }
        private Control getWebPannel()
        {
            return chatMainSPtContainer.Panel2;
            //return splitContainer4.Panel2;
        }
        
        void addBrowerToContainer(Control container,StoreWebBrowser webBrower)
        {
            //container.Controls.Clear();
            //Console.WriteLine("切换到浏览器:" + webBrower.Name);
            //webBrower.AddToContainner(container, DockStyle.Fill);
            //container.Refresh();
        }
        void clearBrowrContainer()
        {
            //Control webPanel = getWebPannel();
            //webPanel.Controls.Clear();
        }

        //private StoreWebBrowser showStoreWebBrowser(StoreRegion region,Store store)
        //{
        //    try
        //    {
        //        StoreWebBrowser globolWebBrower;
        //        Control webPanel = getWebPannel();
        //        if (storeWebs.Keys.Contains(store))
        //        {
        //            globolWebBrower = storeWebs[store];
        //        }
        //        else
        //        {
        //            globolWebBrower = newShopGroupWebBrower(store);//storeWebs[store];
        //            addBrowerToContainer(webPanel, globolWebBrower);
        //            webPanel.Refresh();
        //            clearBrower();
        //            storeWebs.Add(store, globolWebBrower);
        //        }
        //        return globolWebBrower;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("切换浏览器故障：" + ex.Message);
        //    }
        //}

        public ChromiumWebBrowser LoadChat(StoreRegion region, Store storeINfo)
        {
            
            string baseURL = region.GetSellerURL();
            //curBrowser = //; showStoreWebBrowser(region, storeINfo);
            if (storeINfo.LogStatus == Common.Shopee.API.Data.LoginStatus.Log_Succuss)
            {
                if(curStore != storeINfo)
                {
                    
                    string url = baseURL + "/webchat/conversations";
                    //curBrowser.LoadUrl(url);
                    storeWebs.AddNewBrowserTab(url);
                }
            }
            else
            {
                storeWebs.AddNewBrowserTab("http://www.shopee.cn");
            }
            curRegion = region;
            curStore = storeINfo;
            //return curBrowser == null ?null: curBrowser.ChromiumWebBrowser; ;
            return null;
        }
        private void loadURL(StoreRegion region, Store storeINfo,String url)
        {
         
            string baseURL = region.GetSellerURL();
            //StoreWebBrowser webBrower = showStoreWebBrowser(region,storeINfo);
            //if (!webBrower.Address.Contains(baseURL))
            {
                string fullurl = null;
                if (url.ToLower().Contains("http"))
                {
                    fullurl = url;
                            }
                else
                {
                    fullurl = baseURL + url;
                }

                storeWebs.AddNewBrowserTab(fullurl);
                //webBrower.FrameLoadEnd += webBrower_FrameLoadEnd;
                //webBrower.Tag = getLoginJs(userName, password, Guid.NewGuid().ToString(), baseURL);
            }
        }
        private void loadJS(StoreRegion region, Store storeINfo, String js)
        {
            //String userName = storeINfo.UserName;
           
            //StoreWebBrowser webBrower = showStoreWebBrowser(region, storeINfo);
            //webBrower.LoadJS(js);
        }

        private void storeUrlNav(string endUrl)
        {
            if (curStore != null && null != curRegion)
            {
                loadURL(curRegion, curStore,curRegion.GetSellerURL() + endUrl.Replace("\"",""));
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
        }
        private void chatTSTrip_Click(object sender, EventArgs e)
        {
            if (curStore != null && null != curRegion)
            {
                LoadChat(curRegion, curStore);
            }
            else
            {
                MessageBox.Show("请选择一个账号！");
            }
        }

        private void navUrlTSTrip_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripButton )
            {
                ToolStripButton tsBtn = sender as ToolStripButton;
                if (tsBtn.Tag != null && tsBtn.Tag is string)
                {
                    storeUrlNav(tsBtn.Tag as string);
                }
                
            }
        }

        private void frontPageToolStripButton_Click(object sender, EventArgs e)
        {
            if (curStore != null && null != curRegion)
            {
                string baseURL = curRegion.GetBuyerUrl();
                //StoreWebBrowser webBrower = showStoreWebBrowser(curRegion, curStore);
                //webBrower.LoadUrl(baseURL);
                loadURL(curRegion, curStore,baseURL);
            }
            else
            {
                MessageBox.Show("请选择一个账号！");
            }
        }

        private void transTSb_Click(object sender, EventArgs e)
        {
            if (curStore != null && null != curRegion)
            {
                loadJS(curRegion, curStore, Resources.translate);
            }
            else
            {
                MessageBox.Show("请先在左边[店铺导航栏]中选择一个账号！");
            }
        }
    }
}
