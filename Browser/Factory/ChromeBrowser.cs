using CefSharp;
using CefSharp.WinForms;
using Common.Brower;
using SharpBrowser;
using SharpBrowser.Factory;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Common.Browser

{
    public class ChromeBrowser : ChromiumWebBrowser
    {
        public static string DefaultStoreName = "-";
        string name;
        public string StoreName;

        ProxyIP proxyIp = null;
        public ProxyIP ProxyIp
        {
            get { return proxyIp; }
           
         }
        private Dictionary<int, DownloadItem> downloads = new Dictionary<int, DownloadItem>();
        public List<DownloadItem> DownloadItems
        {
            get { return downloads.Values.ToList(); }
        }
        string cacheDir;
        string initUrl;

        List<CefSharp.Cookie> cookies = new List<CefSharp.Cookie>();
        private  static CefSettings globalCefSettings = new CefSettings();
        private static bool isGlobalInitilized = false;

        private string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        public static string UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36";
        public static string AcceptLanguage = "en-US,en;q=0.9";
        public static string HomepageURL = "https://www.baidu.com";
        public static string NewTabURL = "about:blank";
        public static string InternalURL = "sharpbrowser";
        public static string DownloadPageURL = "sharpbrowser://storage/downloads.html";
        public static string FileNotFoundURL = "sharpbrowser://storage/errors/notFound.html";
        public static string CannotConnectURL = "sharpbrowser://storage/errors/cannotConnect.html";
        public static string SearchURL = "https://www.baidu.com/s?wd=";

      
 
       
       
        Interaction interaction = new Interaction((result) =>
        {
            Console.WriteLine(result);
        });

        public Interaction Interaction
        {
            get
            {
                return interaction;
            }

        }

        public string RefererURL = "";
        public ChromeBrowser()
            :base(string.Empty)
        {
            this.proxyIp = null;
            this.name =  Guid.NewGuid().ToString("N") ;
            this.StoreName = ChromeBrowser.DefaultStoreName ;
            this.cacheDir = string.Empty;
            this.initUrl = string.Empty;
            init();
        }
        public ChromeBrowser(string url, String cachedir, string storename, string name, string cookies, ProxyIP ipinfo = null)
            : base(string.Empty)
        {
            this.proxyIp = ipinfo;
            this.name = name==null? Guid.NewGuid().ToString("N"):name;
            this.StoreName = storename == null ? ChromeBrowser.DefaultStoreName : storename;
            this.cacheDir = cachedir;
            this.initUrl = url;
            init();
            importCookies(url,cookies);
        }

        public ChromeBrowser(ChromeBrowser prevBrowser,string url)
            :base(string.Empty)
        {
            this.proxyIp = prevBrowser.proxyIp;
            this.name = Guid.NewGuid().ToString("N");
            this.StoreName = prevBrowser.StoreName;
            this.cacheDir = prevBrowser.cacheDir;
            this.initUrl = url;
            init();
        }
        private void importCookies(string url,string cookies)
        {
            if (cookies != null && cookies.Length > 5)
            {
                ICookieManager cMang = RequestContext.GetCookieManager(null);
                foreach (string ck in cookies.Split(';'))
                {
                    int eqPostion = ck.IndexOf('=');
                    if (eqPostion < 1)
                    {
                        continue;
                    }
                    CefSharp.Cookie cookie = new CefSharp.Cookie();

                    //cookie.Creation = sysCookie.TimeStamp;
                    cookie.Domain = (new Uri(url)).Host.Replace("www.", ""); ;
                    cookie.Expires = DateTime.Now.AddYears(1);
                    //cookie.HttpOnly = sysCookie.HttpOnly;
                    //cookie.LastAccess = sysCookie.;
                    cookie.Name = ck.Substring(0, eqPostion);
                    
                    cookie.Path = "/";
                    //cookie.Secure = sysCookie.Secure;
                    cookie.Value = ck.Length - 1 == eqPostion ? "" : ck.Substring(eqPostion + 1, ck.Length - eqPostion - 1);
                    cMang.SetCookieAsync(url, cookie);
                }
            }
        }
       
        public void LoadUrl(string url, string jsString = "")
        {
            FrameLoadEnd -= webBrower_FrameLoadEnd;
            string baseURL = (new Uri(url)).Host;
            if (!IsBrowserInitialized)
            {
                FrameLoadEnd += webBrower_FrameLoadEnd;
                Tag = jsString;
            }
            else if (this.GetMainFrame().Url.Contains(url))
            {
                Load(url);
                FrameLoadEnd += webBrower_FrameLoadEnd;
                Tag = jsString;
            }
            else
            {
                //正在加载当前的页面，且需要执行JS语句
                if (this.GetMainFrame().Browser.IsLoading && jsString != "")
                {
                    //Load(url);
                    FrameLoadEnd += webBrower_FrameLoadEnd;
                    Tag = jsString;
                }
                else
                {
                    GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsString);
                }
            }
        }
        public new void Load(string url)
        {
            FrameLoadEnd += webBrower_FrameLoadEnd;
            base.Load(url);
        }
        public void LoadJS(string jsString)
        {
            try
            {
                GetBrowser().MainFrame.ExecuteJavaScriptAsync(jsString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string GetHTML()
        {
            //获得页面html
            var task = this.GetSourceAsync();
            task.Wait();
            string html = task.Result;
            return html;
        }
 
        void webBrower_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            //ChromiumWebBrowser webBrower = sender as ChromiumWebBrowser;
            //webBrower.FrameLoadEnd -= webBrower_FrameLoadEnd;
            ////webBrower.AddressChanged += WebBrower_AddressChanged;
            ////FrameLoadEnd += webBrower_FrameLoadEnd2;
            //webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync((String)webBrower.Tag);
            if(null != CookieDataRecieved)
            {
                CookieVisitor visitor = new CookieVisitor(e.Url);
                visitor.SendCookie += CookieDataRecieved;
                ICookieManager cookieManagesr = this.GetCookieManager();
                cookieManagesr.VisitAllCookies(visitor);
            }
            if(CookieDataRecieved == null && null != PageLoadCompleted)
            {
                PageLoadCompleted(e.Url);
            }
        }

        //private void visitor_SendCookie(string url,string name,string value,int count,int total)
        //{
        //    //obj.Domain.TrimStart('.') + "^" +
        //    // string cookies = obj.Name + ":" + obj.Value + ";";
        //    Console.WriteLine("URL:" + url);
        //    Console.WriteLine("第"+count+"个，共" +total+"个。");
        //    Console.WriteLine(name+"=" + value);
        //}
        private void WebBrower_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            ChromiumWebBrowser webBrower = sender as ChromiumWebBrowser;
            webBrower.AddressChanged -= WebBrower_AddressChanged;
            //webBrower.FrameLoadEnd += webBrower_FrameLoadEnd;
            webBrower.GetBrowser().MainFrame.ExecuteJavaScriptAsync((String)webBrower.Tag);
        }


        private void init()
        {
            Name = name;
            configureBrowser();
            JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            JavascriptObjectRepository.Register("interaction", interaction, isAsync: false, options: BindingOptions.DefaultBinder);
            initHandler();
            initRequestContext();
            initEventHandler();
            initHotKey();
        }
        private void initRequestContext()
        {
            RequestContextSettings requestContextSettings = new RequestContextSettings();
            requestContextSettings.PersistSessionCookies = true;
            requestContextSettings.PersistUserPreferences = true;

            if (cacheDir != null && cacheDir != string.Empty)
            {
                //requestContextSettings.CachePath = this.cacheDir.Contains(":") ? this.cacheDir : cefSettings.RootCachePath + "/default/";
                requestContextSettings.CachePath = this.cacheDir;
            }
            RequestContext = new RequestContext(requestContextSettings);
            Console.WriteLine("当前页面缓存路径：" + requestContextSettings.CachePath);
        }
        /// <summary>
        /// Only once for whole programe
        /// </summary>
        /// <param name="rootCachePath"></param>
        /// <param name="userAgent"></param>
        public static void GlobalInitBrowser(string rootCachePath, string userAgent = null)
        {
            if (!isGlobalInitilized)
            {
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                CefSharpSettings.WcfEnabled = false;
                CefSettings cefSettings = new CefSettings();
               
                cefSettings.RegisterScheme(new CefCustomScheme
                {
                    SchemeName = InternalURL,
                    SchemeHandlerFactory = new MySchemeHandlerFactory()
                });


                if (null != userAgent)
                {
                    cefSettings.UserAgent = userAgent;
                }

                cefSettings.RootCachePath = rootCachePath;
                cefSettings.CachePath = rootCachePath + "/default/";
                cefSettings.PersistSessionCookies = true;
                cefSettings.PersistUserPreferences = true;
                //cefSettings.LogSeverity = LogSeverity.Default;
                //settings.LocalesDirPath = GetCacheDir();
                //settings.LogFile = logFilePath;
                //settings.Locale = "zh-CN";


                //CefSettings settings = new CefSettings();


                //settings.RegisterScheme(new CefCustomScheme {
                //	SchemeName = BrowserTabForm.InternalURL,
                //	SchemeHandlerFactory = new SchemeHandlerFactory()
                //});
                //cefSettings.UserAgent = BrowserTabForm.UserAgent;
                //cefSettings.AcceptLanguageList = AcceptLanguage;

                cefSettings.IgnoreCertificateErrors = true;

                //settings.CachePath = GetAppDir("Cache");
                cefSettings.MultiThreadedMessageLoop = true;
                isGlobalInitilized = true;
                ChromeBrowser.globalCefSettings = cefSettings;
            }

        }
        /// <summary>
        /// this is done every time a new tab is openede
        /// </summary>
        private void configureBrowser()
        {
            BrowserSettings config = new BrowserSettings();
            config.FileAccessFromFileUrls = CefState.Disabled;// (!CrossDomainSecurity).ToCefState();
            config.UniversalAccessFromFileUrls = CefState.Disabled; //(!CrossDomainSecurity).ToCefState();
            config.WebSecurity = CefState.Enabled; //WebSecurity.ToCefState();
            config.WebGl = CefState.Enabled;//WebGL.ToCefState();
            config.Databases = CefState.Enabled;
            config.Javascript = CefState.Enabled;
            config.LocalStorage = CefState.Enabled;
            config.Plugins = CefState.Enabled;
            config.TabToLinks = CefState.Enabled;



            config.ApplicationCache = CefState.Enabled;

            BrowserSettings = config;

        }
        private void initHandler()
        {
            //browser.MenuHandler = new ContextMenuHandler(this);
            
            RequestHandler = new RequestHandler(this);
            LifeSpanHandler = new LifeSpanHandler(this);
            DownloadHandler = new DownloadHandler(this);
 
        }
        private void initEventHandler()
        {
           IsBrowserInitializedChanged += webBrower_IsBrowserInitializedChanged; ;
        }
        private void initHotKey()
        {
            SharpBrowser.KeyboardHandler keyboardHandler = new KeyboardHandler(this);
            this.KeyboardHandler = keyboardHandler;
            //KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.W, true);
            //KeyboardHandler.AddHotKey(this, CloseActiveTab, Keys.Escape, true);
            //KeyboardHandler.AddHotKey(this, AddBlankWindow, Keys.N, true);
            //KeyboardHandler.AddHotKey(this, AddBlankTab, Keys.T, true);
            ////KeyboardHandler.AddHotKey(this, RefreshActiveTab, Keys.F5);
            keyboardHandler.AddHotKey(this, OpenDeveloperTools, Keys.F12);
            //KeyboardHandler.AddHotKey(this, NextTab, Keys.Tab, true);
            //KeyboardHandler.AddHotKey(this, PrevTab, Keys.Tab, true, true);

            // search hotkeys
            //KeyboardHandler.AddHotKey(this, OpenSearch, Keys.F, true);
            //KeyboardHandler.AddHotKey(this, CloseSearch, Keys.Escape);
            //KeyboardHandler.AddHotKey(this, StopActiveTab, Keys.Escape);

            
        }
        private void Browser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
        }
       
        private void webBrower_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
            if (IsBrowserInitialized  )
            {
                setWebProxy();
            }
        }
        private void setWebProxy()
        {
            Cef.UIThreadTaskFactory.StartNew(delegate
            {
                bool success = true;
                if (proxyIp != null)
                {
                    var requestConent = GetBrowser().GetHost().RequestContext;
                    Dictionary<string, object> proxySetting = new Dictionary<string, object>();
                    proxySetting["mode"] = "fixed_servers";
                    proxySetting["server"] = proxyIp.IP + ":" + proxyIp.Port;

                    string error1 = string.Empty;
                    success = requestConent.SetPreference("proxy", proxySetting, out error1);
                    Console.WriteLine("店群：" + name + "设置Web代理：" + success + " and " + error1);
                }
                if (success)
                {
                    Load(initUrl);
                }
            });
        }
        private void OpenDeveloperTools()
        {
            this.ShowDevTools();
        }
        public new void Dispose()
        {
            base.Dispose();
        }
        public delegate void DownloadItemUpdateHandler(DownloadItem item);
        public DownloadItemUpdateHandler DownloadItemChanged;
        public void UpdateDownloadItem(DownloadItem item)
        {
            if(null != DownloadItemChanged)
            {
                DownloadItemChanged(item);
            }
            if(downloads.ContainsKey(item.Id))
            {
                downloads[item.Id]= item;
            }
            else
            {
                downloads.Add(item.Id,item);
            }
            
        }

        public string CalcDownloadPath(DownloadItem item)
        {
            return item.SuggestedFileName;
        }

        public bool DownloadsInProgress()
        {
            foreach (DownloadItem item in downloads.Values)
            {
                if (item.IsInProgress)
                {
                    return true;
                }
            }
            return false;
        }
        public delegate void PageLoadCompletedHandler(string url);
        public PageLoadCompletedHandler PageLoadCompleted;
        public delegate void CookieDataHandler(string url, string name, string value, int count, int total);
        public CookieDataHandler CookieDataRecieved;
    }

    public class CookieVisitor : CefSharp.ICookieVisitor
    {
        public event ChromeBrowser.CookieDataHandler SendCookie;
        string url;
        public CookieVisitor(string url)
        {
            this.url = url;
        }
        public void Dispose()
        {

        }

        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = false;
            if (SendCookie != null)
            {
                SendCookie(url,cookie.Name,cookie.Value,count,total);
            }

            return true;

        }



    }
    //public class ChromeTest
    //{
    //    public static ChromiumWebBrowser Create(WebProxy proxy = null, Action<ChromiumWebBrowser> onInited = null)
    //    {
    //        var result = default(ChromiumWebBrowser);
    //        var settings = new CefSettings();
    //        result = new ChromiumWebBrowser("about:blank");
    //        if (proxy != null)
    //            result.RequestHandler = new RequestHandler(proxy?.Credentials as NetworkCredential);

    //        result.IsBrowserInitializedChanged += (s, e) =>
    //        {
    //            //if (!e.IsBrowserInitialized)
    //            //    return;

    //            var br = (ChromiumWebBrowser)s;
    //            if (proxy != null)
    //            {
    //                var v = new Dictionary<string, object>
    //                {
    //                    ["mode"] = "fixed_servers",
    //                    ["server"] = $"{proxy.Address.Scheme}://{proxy.Address.Host}:{proxy.Address.Port}"
    //                };
    //                if (!br.GetBrowser().GetHost().RequestContext.SetPreference("proxy", v, out string error))
    //                    MessageBox.Show(error);
    //            }

    //            onInited?.Invoke(br);
    //        };

    //        return result;
    //    }


    //    //var p = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));
    //    //var p1 = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));
    //    //var p2 = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));

    //    //wb1 = ChromeTest.Create(p1, b => b.Load("http://speed-tester.info/check_ip.php"));
    //    //groupBox1.Controls.Add(wb1);
    //    //wb1.Dock = DockStyle.Fill;

    //    //wb2 = ChromeTest.Create(p2, b => b.Load("http://speed-tester.info/check_ip.php"));
    //    //groupBox2.Controls.Add(wb2);
    //    //wb2.Dock = DockStyle.Fill;

    //    //wb3 = ChromeTest.Create(p, b => b.Load("http://speed-tester.info/check_ip.php"));
    //    //groupBox3.Controls.Add(wb3);
    //    //wb3.Dock = DockStyle.Fill;
    //}
}
