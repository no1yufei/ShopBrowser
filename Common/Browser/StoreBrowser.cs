using CefSharp;
using CefSharp.WinForms;
using Common.Brower;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;

namespace Common.Browser
{
    public class StoreWebBrowser
    {
        //StoreGroup group;
        string name;
        string proxyIP = "";
        long proxyPort = 0;
        string proxyUser = "";
        string proxyPassword = "";
        string cacheDir;
        string initUrl;
        bool initSuccessed = false;
        List<CefSharp.Cookie> cookies = new List<CefSharp.Cookie>();

        public Store Store;
        public string Address
        {
            get { return chromiumWebBrowser.Address; }
        }
        public string Name
        {
            set { chromiumWebBrowser.Name = value; }
            get { return chromiumWebBrowser.Name; }
        }
        public Control Container;
       

       

        private ChromiumWebBrowser chromiumWebBrowser;
        public ChromiumWebBrowser ChromiumWebBrowser
        {
            get
            {
                return chromiumWebBrowser;
            }
        }

        public StoreWebBrowser(StoreGroup group, string url, String cachedir)
        {
            if (group.IsProxy)
            {
                this.proxyIP = group.ProxyIP;
                this.proxyPort = group.Port;
                this.proxyUser = group.ProxyUserName;
                this.proxyPassword = group.Password;
            }
            this.name = group.GroupName;
            this.cacheDir = cachedir == null ? BrowerHelper.Instatce.GetCacheDir(group) : cachedir;
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(group.GroupName, cachedir, group.IsProxy);
        }
        public StoreWebBrowser(StoreGroup group, string url, bool radomCachedir = true, RequestHandler.RequestDataHandler callback = null)
        {
            if (group.IsProxy)
            {
                this.proxyIP = group.ProxyIP;
                this.proxyPort = group.Port;
                this.proxyUser = group.ProxyUserName;
                this.proxyPassword = group.Password;
            }
            this.name = group.GroupName;
            this.cacheDir = radomCachedir ? BrowerHelper.Instatce.GetCacheDir(Guid.NewGuid().ToString("N")) : BrowerHelper.Instatce.GetCacheDir(group);
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(group.GroupName, this.cacheDir, group.IsProxy, null, callback);
        }
        public StoreWebBrowser(string url, String cachedir = null)
        {
            this.name = "common";
            this.cacheDir = cachedir == null ? BrowerHelper.Instatce.GetCacheDir() : cachedir;
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(name, cachedir, false);
        }
        public StoreWebBrowser(string url, string token, bool radomCachedir, string cachedir = null)
        {
            this.name = "common";
            this.cacheDir = cachedir == null ? BrowerHelper.Instatce.GetCacheDir() : cachedir;
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(name, cachedir, false, token);
        }
        public StoreWebBrowser(StoreGroup group, Store store, String cachedir, string url)
        {
            if (group.IsProxy)
            {
                this.proxyIP = group.ProxyIP;
                this.proxyPort = group.Port;
                this.proxyUser = group.ProxyUserName;
                this.proxyPassword = group.Password;
            }
            this.Store = store;
            this.name = store.DisplayName;
            this.cacheDir = cachedir;
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(group.GroupName, cachedir, group.IsProxy);
            //chromiumWebBrowser.FrameLoadEnd += ChromiumWebBrowser_FrameLoadEnd;
            if (store.Cookies == null)
            {
                return;
            }
            ICookieManager cMang = chromiumWebBrowser.RequestContext.GetCookieManager(null);
            foreach (string ck in store.Cookies.Split(';'))
            {
                int eqPostion = ck.IndexOf('=');
                if (eqPostion < 1)
                {
                    continue;
                }
                CefSharp.Cookie cookie = new CefSharp.Cookie();
                //cookie.Creation = sysCookie.TimeStamp;
                cookie.Domain = store.ServerURL.Replace("https://seller", "");
                cookie.Expires = DateTime.Now.AddYears(1);
                //cookie.HttpOnly = sysCookie.HttpOnly;
                //cookie.LastAccess = sysCookie.;
                cookie.Name = ck.Substring(0, eqPostion);
                if (cookie.Name.Contains("SPC_CDS"))
                {
                    continue;
                }
                cookie.Path = "/";
                //cookie.Secure = sysCookie.Secure;
                cookie.Value = ck.Length - 1 == eqPostion ? "" : ck.Substring(eqPostion + 1, ck.Length - eqPostion - 1);
                cMang.SetCookieAsync(url, cookie);
            }
        }

        public StoreWebBrowser(string name, String cachedir, string url, string ProxyIP = "", long Port = 0, string ProxyUserName = "", string Password = "")
        {

            this.proxyIP = ProxyIP;
            this.proxyPort = Port;
            this.proxyUser = ProxyUserName;
            this.proxyPassword = Password;

            this.name = name;
            this.cacheDir = cachedir;
            this.initUrl = url;
            chromiumWebBrowser = createChromiumWebBrowser(name, cachedir, ProxyIP == "");
        }
        public void AddToContainner(Control container, DockStyle dock)
        {
            container.Controls.Add(chromiumWebBrowser);
            chromiumWebBrowser.Dock = dock;
            Container = container;
        }

        public void RemoveFromContainner(Control container)
        {
            container.Controls.Remove(chromiumWebBrowser);
            chromiumWebBrowser.Dispose();
        }
        public void LoadUrl(string url, string jsString = "")
        {
            chromiumWebBrowser.FrameLoadEnd -= webBrower_FrameLoadEnd;
            string baseURL = (new Uri(url)).Host;
            if (!chromiumWebBrowser.IsBrowserInitialized)
            {
                chromiumWebBrowser.FrameLoadEnd += webBrower_FrameLoadEnd;
                chromiumWebBrowser.Tag = jsString;
            }
            else if (!chromiumWebBrowser.GetMainFrame().Url.Contains(url))
            {
                chromiumWebBrowser.Load(url);
                chromiumWebBrowser.FrameLoadEnd += webBrower_FrameLoadEnd;
                chromiumWebBrowser.Tag = jsString;
            }
            else
            {
                //正在加载当前的页面，且需要执行JS语句
                if (chromiumWebBrowser.GetMainFrame().Browser.IsLoading && jsString != "")
                {
                    //chromiumWebBrowser.Load(url);
                    chromiumWebBrowser.FrameLoadEnd += webBrower_FrameLoadEnd;
                    chromiumWebBrowser.Tag = jsString;
                }
                else
                {
                    chromiumWebBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsString);
                }
            }
        }
        public void LoadJS(string jsString)
        {
            try
            {
                chromiumWebBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public string GetHTML()
        {
            //获得页面html
            var task = chromiumWebBrowser.GetSourceAsync();
            task.Wait();
            string html = task.Result;
            return html;
        }
        void webBrower_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            ChromiumWebBrowser webBrower = sender as ChromiumWebBrowser;
            webBrower.FrameLoadEnd -= webBrower_FrameLoadEnd;
            //webBrower.AddressChanged += WebBrower_AddressChanged;
            //chromiumWebBrowser.FrameLoadEnd += webBrower_FrameLoadEnd2;
            webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync((String)webBrower.Tag);

        }

        private void WebBrower_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            ChromiumWebBrowser webBrower = sender as ChromiumWebBrowser;
            webBrower.AddressChanged -= WebBrower_AddressChanged;
            //webBrower.FrameLoadEnd += webBrower_FrameLoadEnd;
            webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync((String)webBrower.Tag);
        }


        private ChromiumWebBrowser createChromiumWebBrowser(string name, String cachedir, bool isPorxy, string token = null, RequestHandler.RequestDataHandler callback = null)
        {
            RequestContextSettings requestContextSettings = new RequestContextSettings();
            requestContextSettings.PersistSessionCookies = true;
            //requestContextSettings.PersistUserPreferences = true;

            String cachePath = cacheDir;

            requestContextSettings.CachePath = cachePath.Contains(":") ? cachePath : BrowerHelper.Instatce.GetCacheDir();
            Console.WriteLine("缓存路径：" + requestContextSettings.CachePath);
            ChromiumWebBrowser globolWebBrower = new ChromiumWebBrowser(string.Empty);
            globolWebBrower.Name = name;
            //globolWebBrower.RegisterJsObject("interaction", interaction);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSharpSettings.WcfEnabled = true;
          //  globolWebBrower.JavascriptObjectRepository.Register("interaction", interaction, isAsync: false, options: BindingOptions.DefaultBinder);
            configureBrowser(globolWebBrower);
            initDevTools(globolWebBrower);
            globolWebBrower.RequestContext = new RequestContext(requestContextSettings);
            globolWebBrower.IsBrowserInitializedChanged += webBrower_IsBrowserInitializedChanged; ;
            globolWebBrower.LifeSpanHandler = new OpenInSelfLifeSpanHandler();
            NetworkCredential networkCredential = null;
            if (isPorxy)
            {
                networkCredential = new NetworkCredential(proxyUser, proxyPassword);
            }
            RequestHandler rHandler = new RequestHandler(token, networkCredential);
            rHandler.RequestDataCallBack = callback;
            globolWebBrower.RequestHandler = rHandler;
            return globolWebBrower;
        }


        private void webBrower_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            ChromiumWebBrowser webBrower = sender as ChromiumWebBrowser;
            if (webBrower.IsBrowserInitialized)
            {
                setWebProxy(webBrower);
            }
        }
        private void setWebProxy(ChromiumWebBrowser webBrower)
        {
            Cef.UIThreadTaskFactory.StartNew(delegate
            {
                bool success = true;
                if (proxyIP != "")
                {
                    var requestConent = webBrower.GetBrowser().GetHost().RequestContext;
                    Dictionary<string, object> proxySetting = new Dictionary<string, object>();
                    proxySetting["mode"] = "fixed_servers";
                    proxySetting["server"] = proxyIP + ":" + proxyPort;

                    string error1 = string.Empty;
                    success = requestConent.SetPreference("proxy", proxySetting, out error1);
                    Console.WriteLine("店群：" + name + "设置Web代理：" + success + " and " + error1);
                }
                if (success)
                {
                    initSuccessed = true;
                    webBrower.Load(initUrl);
                }
            });
        }
        /// <summary>
        /// 初始化开发者工具
        /// </summary>
        [Conditional("DEBUG")]
        private void initDevTools(ChromiumWebBrowser brower)
        {
            const int f12KeyCode = 123;
            var keyboardHandler = new CefKeyboardHandler();
            keyboardHandler.KeyArrived += (b, t, c, m, i) =>
            {
                if ((t == KeyType.RawKeyDown) && (c == f12KeyCode))
                {
                    brower.ShowDevTools();
                }
            };
            brower.KeyboardHandler = keyboardHandler;
        }

        /// <summary>
        /// this is done every time a new tab is openede
        /// </summary>
        private void configureBrowser(ChromiumWebBrowser browser)
        {
            BrowserSettings config = new BrowserSettings();
            config.FileAccessFromFileUrls = CefState.Enabled;// (!CrossDomainSecurity).ToCefState();
            config.UniversalAccessFromFileUrls = CefState.Enabled; //(!CrossDomainSecurity).ToCefState();
            config.WebSecurity = CefState.Enabled; //WebSecurity.ToCefState();
            config.WebGl = CefState.Enabled;//WebGL.ToCefState();
            browser.BrowserSettings = config;

        }

        public void Dispose()
        {
            chromiumWebBrowser.Dispose();
        }
    }
}
