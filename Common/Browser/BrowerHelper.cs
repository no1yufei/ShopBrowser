
using CefSharp;
using CefSharp.Handler;
using CefSharp.WinForms;
using ShopeeChat;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace Common.Browser
{
    public class BrowerHelper
    {
        static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string cacheDirectory = "\\cache";
        string logFilePath = Environment.CurrentDirectory + "\\cefsharplib.cfg";
        string userName = "";
        
        static private BrowerHelper instatce;
        static public BrowerHelper Instatce
        {
            get
            {
                if (null == instatce)
                {
                    instatce = new BrowerHelper();
                    try
                    {
                        instatce.initBrowser();
                    }
                    catch (Exception ex)
                    {
                        string downloadUrl = "https://download.microsoft.com/download/9/3/F/93FCF1E7-E6A4-478B-96E7-D4B285925B00/vc_redist.x86.exe";
                        MessageBox.Show("您操作系统缺少系统运行必备的补丁，点击确定后，请安装下载的补丁后重新运行程序。错误消息：" + ex.Message);
                        BrowerHelper.instatce.OpenURLViaSysBrower(downloadUrl);
                        //MessageBox.Show("如果系统没有自动打开浏览器开始下载，请将网址复制到浏览器中，手动下载！链接：" + downloadUrl);
                    }

                }
                return instatce;
            }
        }
        public void OpenURLViaSysBrower(String url)
        {
            System.Diagnostics.Process.Start(url);
        }
        public void Initialize(String username)
        {
            if (username != userName)
            {
                this.userName = username;
                initBrowerCache();
            }
        }
        static public void ClearCache()
        {
            try
            {
                Directory.Delete(basePath + cacheDirectory, true);
            }
            catch (Exception ex)
            {
                throw new Exception("暂时无法清空缓存:" + ex.Message);
            }
        }

        /// <summary>
        /// this is done just once, to globally initialize CefSharp/CEF
        /// </summary>
        private void initBrowser()
        {
            ChromeBrowser.GlobalInitBrowser(GetCacheDir(),HtmlHttpHelper.UserAgent);
        }

        string cachetempPath = Environment.CurrentDirectory + "\\cache\\";
        private void initBrowerCache()
        {
            if (!Directory.Exists(cachetempPath))
            {
                string zipFile = Environment.CurrentDirectory + "\\cache.zip";
                if (File.Exists(zipFile))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(zipFile, cachetempPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("解压缓存文件失败：" + ex.Message);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("解压缓存文件失败：未发现缓存文件");
                    return;
                }
            }
            foreach (StoreGroup group in GroupConfigHelper.Instatce.Groups)
            {
                foreach (StoreRegion region in group.Regions)
                {
                    foreach (Store store in region.Stores)
                    {
                        String cachePath = GetCacheDir();
                        cachePath += "\\" + group.ID.ToString();
                        if (!Directory.Exists(cachePath))
                        {
                            Directory.CreateDirectory(cachePath);
                        }
                        cachePath += "\\" + store.UserName;
                        if (!Directory.Exists(cachePath))
                        {
                            try
                            {
                                Directory.CreateDirectory(cachePath);
                                CopyDir(cachetempPath + region.RegionID, cachePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
        }
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    try
                    {
                        // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                        if (Directory.Exists(file))
                            CopyDir(file, aimPath + Path.GetFileName(file));
                        // 否则直接Copy文件
                        else
                            File.Copy(file, aimPath + Path.GetFileName(file), true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("复制出错!" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法复制!" + ex.Message);
            }
        }
        public string GetCacheDir(StoreGroup group)
        {
            String cachePath = GetCacheDir();
            cachePath += "\\" + group.ID.ToString();
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            return cachePath;
        }

        public string GetCacheDir(StoreGroup group, Store store)
        {
            String cachePath = GetCacheDir();
            cachePath += "\\" + group.ID.ToString();
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            cachePath += "\\" + store.UserName;
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
                CopyDir(cachetempPath + store.RegionID, cachePath);
            }
            return cachePath;
        }

        public string GetCacheDir(StoreRegion region)
        {
            String cachePath = GetCacheDir();
            cachePath += "\\" + region.RegionID;
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            return cachePath;
        }

        public string GetCacheDir(Store store)
        {
            String cachePath = GetCacheDir();
            cachePath += "\\" + store.UserName;
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            return cachePath;
        }

        public string GetCacheDir()
        {
            String cachePath = basePath + cacheDirectory;
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            cachePath += "\\" + userName;
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            return cachePath;
        }
        public string GetCacheDir(string childDirs)
        {
            String cachePath = GetCacheDir();
            foreach (string childDir in childDirs.Split('\\'))
            {
                cachePath += "\\" + childDir;
                if (!Directory.Exists(cachePath))
                {
                    Directory.CreateDirectory(cachePath);
                }
            }
            return cachePath;
        }
    }
  
    internal class OpenInSelfLifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return false;
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //throw new NotImplementedException();
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            //throw new NotImplementedException();
        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            chromiumWebBrowser.Load(targetUrl);
            return true; //Return true to cancel the popup creation copyright by codebye.com.
        }
    }
    public class ResourceRequestHandler:CefSharp.Handler.ResourceRequestHandler
    {
        private string _token = "";
        public ResourceRequestHandler(string token)
        {
            _token = token;
        }

        protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            Uri url;
            if (Uri.TryCreate(request.Url, UriKind.Absolute, out url) == false)
            {
                return CefReturnValue.Cancel;
            }
            //String urlStr = request.Url;
            //request.Url = urlStr.Replace("cf.shopee.tw", "s-cf-tw.shopeesz.com/");
            //if (null != _token && _token.Length > 5)
            //{
            //    var headers = request.Headers;
            //    headers["Authorization"] = _token; //传递进去认证Token
            //    request.Headers = headers;
            //}
            //request.Url = request.Url.Replace("cf.shopee.tw", "s-cf-tw.shopeesz.com/");
            return CefReturnValue.Continue;
        }
    }
    public class RequestHandler : CefSharp.Handler.RequestHandler
    {
        public delegate void RequestDataHandler(string url, string method, string data);

        public RequestDataHandler RequestDataCallBack = null;
        private NetworkCredential _credential;
        private string _token = "";

        public RequestHandler(String token="",NetworkCredential credential = null) : base()
        {
            _token = token;
            _credential = credential;
        }
        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            //if(!request.IsReadOnly)
            //{
            //    request.Url = request.Url.Replace("cf.shopee.tw", "s-cf-tw.shopeesz.com/");
            //}
           
              //  return new ResourceRequestHandler(_token);
        
            return base.GetResourceRequestHandler(chromiumWebBrowser, browser, frame, request, isNavigation, isDownload, requestInitiator, ref disableDefaultHandling);
        }

        protected override bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            if (isProxy == true)
            {
                if (_credential == null)
                    throw new NullReferenceException("credential is null");

                callback.Continue(_credential.UserName, _credential.Password);
                return true;
            }

            return false;
        }

        //public override bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        //{
        //    if (isProxy == true)
        //    {
        //        if (_credential == null)
        //            throw new NullReferenceException("credential is null");

        //        callback.Continue(_credential.UserName, _credential.Password);
        //        return true;
        //    }

        //    return false;
        //}

        //public override bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        //{

        //    if (null != RequestDataCallBack)
        //    {
        //        var m = request.Method;

        //        if (request.Method == "POST")
        //        {
        //            using (var postData = request.PostData)
        //            {
        //                if (postData != null)
        //                {
        //                    var elements = postData.Elements;

        //                    var charSet = request.GetCharSet();

        //                    foreach (var element in elements)
        //                    {
        //                        if (element.Type == PostDataElementType.Bytes)
        //                        {
        //                            var body = element.GetBody(charSet);
        //                            RequestDataCallBack(request.Url, request.Method, body);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}
        //public override CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        //{
        //    if (null != RequestDataCallBack)
        //    {
        //        var m = request.Method;

        //        if (request.Method == "POST")
        //        {
        //            using (var postData = request.PostData)
        //            {
        //                if (postData != null)
        //                {
        //                    var elements = postData.Elements;

        //                    var charSet = request.GetCharSet();

        //                    foreach (var element in elements)
        //                    {
        //                        if (element.Type == PostDataElementType.Bytes)
        //                        {
        //                            var body = element.GetBody(charSet);
        //                            RequestDataCallBack(request.Url, request.Method, body);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return CefReturnValue.Continue;
        //}

        public class ChromeTest
        {
            public static ChromiumWebBrowser Create(WebProxy proxy = null, Action<ChromiumWebBrowser> onInited = null)
            {
                var result = default(ChromiumWebBrowser);
                var settings = new CefSettings();
                result = new ChromiumWebBrowser("about:blank");
                if (proxy != null)
                    result.RequestHandler = new RequestHandler(null,proxy?.Credentials as NetworkCredential);

                result.IsBrowserInitializedChanged += (s, e) =>
                {
                    //if (!e.IsBrowserInitialized)
                    //    return;

                    var br = (ChromiumWebBrowser)s;
                    if (proxy != null)
                    {
                        var v = new Dictionary<string, object>
                        {
                            ["mode"] = "fixed_servers",
                            ["server"] = $"{proxy.Address.Scheme}://{proxy.Address.Host}:{proxy.Address.Port}"
                        };
                        if (!br.GetBrowser().GetHost().RequestContext.SetPreference("proxy", v, out string error))
                            MessageBox.Show(error);
                    }

                    onInited?.Invoke(br);
                };

                return result;
            }


            //var p = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));
            //var p1 = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));
            //var p2 = new WebProxy("Scheme://Host:Port", true, new[] { "" }, new NetworkCredential("login", "pass"));

            //wb1 = ChromeTest.Create(p1, b => b.Load("http://speed-tester.info/check_ip.php"));
            //groupBox1.Controls.Add(wb1);
            //wb1.Dock = DockStyle.Fill;

            //wb2 = ChromeTest.Create(p2, b => b.Load("http://speed-tester.info/check_ip.php"));
            //groupBox2.Controls.Add(wb2);
            //wb2.Dock = DockStyle.Fill;

            //wb3 = ChromeTest.Create(p, b => b.Load("http://speed-tester.info/check_ip.php"));
            //groupBox3.Controls.Add(wb3);
            //wb3.Dock = DockStyle.Fill;
        }
    }
    public class CookieVisitor : CefSharp.ICookieVisitor
    {
        public event Action<CefSharp.Cookie> SendCookie;

        public void Dispose()
        {
            Console.WriteLine("throw new NotImplementedException();");
        }

        public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            deleteCookie = false;
            if (SendCookie != null)
            {
                SendCookie(cookie);
            }

            return true;
        }
    }

}

