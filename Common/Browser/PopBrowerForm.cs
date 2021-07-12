
using Common.Browser;
using SharpBrowser.Factory;
using ShopeeChat.SysData;
using ShopeeChat.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Brower
{
    public partial class PopBrowerForm : Form
    {
        public  string SuccessUrl = "baidu";
        Store store;
        ChromeBrowser chromeBrowser;
        Dictionary<string, Dictionary<string, string>> cookies = new Dictionary<string, Dictionary<string, string>>();
        public PopBrowerForm(StoreGroup group, Store store, String url)
        {
            InitializeComponent();
            String cacheDir = BrowerHelper.Instatce.GetCacheDir(group, store);
            this.store = store;
            ProxyIP proIP = null;
            if (group.IsProxy)
            {
                proIP = new ProxyIP();
                proIP.IP = group.ProxyIP;
                proIP.Port = group.Port;
                proIP.UserName = group.ProxyUserName;
                proIP.Password = group.Password;

            }
            chromeBrowser = new ChromeBrowser(url, cacheDir, store.UserName, null,"", proIP);
            chromeBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(chromeBrowser);
            chromeBrowser.CookieDataRecieved += recieveCookie;
        }
        private void pageLoaded(string url)
        {
            if (url.Trim(new char[]{ '/','\\'}).StartsWith(SuccessUrl.Trim(new char[] { '/', '\\' }))
                && !url.ToLower().Contains("login"))
            {
                store.Cookies = "";
                foreach (string name in cookies[url].Keys)
                {
                    store.Cookies += name + "=" + cookies[url][name] + ";";
                }
                MessageBox.Show("店铺绑定成功！");
            }
        }
        private void recieveCookie(string url,string name,string value,int count,int total)
        {
            if(!cookies.Keys.Contains(url))
            {
                cookies.Add(url, new Dictionary<string, string>());
            }
            if(!cookies[url].Keys.Contains(name))
            {
                cookies[url].Add(name,value);
            }
            else
            {
                cookies[url][name] = value;
            }
            if(count +1 == total)
            {
                pageLoaded(url);
            }
        }
        
    }

}
