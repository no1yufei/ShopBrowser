
using CsharpHttpHelper;
//using CsharpHttpHelper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ShopeeChat
{
    public class HtmlHttpHelper
    {
        static public string UserAgent =  "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Safari/537.36";
        //static public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36";
        public List<Cookie> Cookies = new List<Cookie>();
        public string sCookies="";
       
        public string sPostString = "";
        public string Referer = "";
        public string Origin = ""; 
        public string Authorization;
        public string sAcceptEncoding = "";
        public int iTimeOut = 10000;//超时时间E:\WorkSpace\OnlineShop\Code\DeliveryInfoManager\VariableInfoForm.cs
        public bool bError = false;
        public bool bProxy = false;
        public string sProxyIP = "";
        public string sProxyPort = "0";
        public string sProxyUserName = "";
        public string sProxyPassWord = "";
        public string Header = "";
        public string sContentType = "";

        public string FormateCookies;
       
        //HttpHelper http = new HttpHelper();


        public string GetHtmlCode(string sUrl, string sCode)//根据不同方式
        {
            if (iTimeOut == 0)
            {
                iTimeOut = 10000;
            }
            try
            {
                if (sPostString == "")
                {
                    return Get(sUrl, sCode).Html;
                }
                else
                {
                    return Post(sUrl, sPostString, sCode).Html;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public HttpResult Put(string sUrl, string sPutString, string sCode = "UTF-8")//根据不同方式
        {
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }

            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "put",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "*/*",//    可选项有默认值
                ContentType = sContentType.Length >0? sContentType:"application/json;text/plain;charset=UTF-8",//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = sPutString,//Post数据     可选项GET时不需要写
                PostDataType = PostDataType.String,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                ProxyIp = "ieproxy",
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                //PostdataByte = System.Text.Encoding.Default.GetBytes("测试一下"),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来
            };
            // item.Header.Add("Content-Length: 44");
            
            //item.Header.ContentLength = 44;
            //item.Accept = "application/json, text/javascript, */*; q=0.01";
            //item.Header.Add("X-Requested-With: XMLHttpRequest");
            item.Header.Add("Accept-Encoding: gzip, deflate, br");//gzip, deflate, br
            item.Header.Add("Accept-Language: zh-CN,zh;q=0.9");//zh-CN,zh;q=0.9
            if(Origin != null && Origin.Length > 5)
            {
                item.Header.Add("origin: " + Origin);//zh-CN,zh;q=0.9
            }
            if (Origin != null && Origin.Length > 5)
            {
                //sc-fe-ver: v200410_drc_hotfix
                item.Header.Add("sc-fe-ver: " + "v200410_drc_hotfix");//zh-CN,zh;q=0.9
            }
            if (Authorization != null && Authorization.Length > 10)
            {
                item.Header.Add(string.Format("Authorization: {0}", Authorization));
            }


            if (Cookies.Count > 0)
            {
                foreach (Cookie c in Cookies)
                {
                    if (sCookies.Contains(c.Name))
                    {
                        string prefix = sCookies.Length > 0 ? "," : "";
                        sCookies += prefix + c.Name + "=" + c.Value;
                    }
                }
            }
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //result.CookieCollection;
            //取出返回的Cookie
            SetCookiesAdv(result.Cookie, result.CookieCollection);
            //返回的Html内容
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }
            else
            {
                bError = true;
            }
            return result;

        }
        const string version11 = "1.1";
        public HttpResult Post(string sUrl, string sPostString, string sCode= "UTF-8",bool vserion10 = false)//根据不同方式
        {
                        Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }

            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "post",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/plain, */*",//    可选项有默认值
                ContentType = sContentType.Length > 0 ? sContentType : "application/json;text/plain;charset=UTF-8",//返回类型    可选项有默认值//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = sPostString,//Post数据     可选项GET时不需要写
                PostDataType = PostDataType.String,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                //ProxyIp = "ieproxy",
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                //PostdataByte = System.Text.Encoding.Default.GetBytes("测试一下"),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来
            };
            // item.Header.Add("Content-Length: 44");

            //item.Header.ContentLength = 44;
            //item.Accept = "application/json, text/javascript, */*; q=0.01";
            item.ProtocolVersion = vserion10 ? HttpVersion.Version10 : HttpVersion.Version11;
            //item.Header.Add("X-CSRFToken: x2p6IfWVMVao5EqfoMAp2H2pfPn68GcL");
            //item.Header.Add("X-API-SOURCE: XMLHttpRequest");
            //item.Header.Add("X-Requested-With: pc");
            item.Header.Add("Accept-Encoding: gzip, deflate, br");//gzip, deflate, br
            item.Header.Add("Accept-Language: zh-CN,zh;q=0.8");//zh-CN,zh;q=0.9
            if (Authorization != null && Authorization.Length > 10)
            {
                item.Header.Add(string.Format("Authorization: {0}", Authorization));
            }


            if (Cookies.Count > 0)
            {
                foreach(Cookie c in Cookies)
                {
                    if(sCookies.Contains(c.Name))
                    {
                        string prefix = sCookies.Length > 0 ? "," : "";
                        sCookies += prefix + c.Name + "=" + c.Value;
                    }
                }
            }
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //result.CookieCollection;
            //取出返回的Cookie
            SetCookiesAdv(result.Cookie, result.CookieCollection);
            //返回的Html内容
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }
            else
            {
                bError = true;
            }
            return result;

        }
        public HttpResult PostImage(string sUrl, Byte[] postByte,string filename=null, string sCode = "UTF-8", bool vserion10 = false)//根据不同方式
        {
            string boundary = "----WebKitFormBoundaryd016lOy7AUFpArCn";
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch{}

            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "post",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/plain, */*",//    可选项有默认值
                ContentType = "multipart/form-data; boundary=" + boundary,//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                PostDataType = PostDataType.Byte,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                PostdataByte = bulidImageBytes(boundary, postByte, filename),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来
            };
            item.ProtocolVersion = vserion10 ? HttpVersion.Version10 : HttpVersion.Version11;
            item.Header.Add("Accept-Encoding: gzip, deflate, br");//gzip, deflate, br
            item.Header.Add("Accept-Language: zh-CN,zh;q=0.9");//zh-CN,zh;q=0.9
            if (Authorization != null && Authorization.Length > 10)
            {
                item.Header.Add(string.Format("Authorization: {0}", Authorization));
            }

            if (Cookies.Count > 0)
            {
                foreach (Cookie c in Cookies)
                {
                    if (sCookies.Contains(c.Name))
                    {
                        string prefix = sCookies.Length > 0 ? "," : "";
                        sCookies += prefix + c.Name + "=" + c.Value;
                    }
                }
            }
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
             SetCookiesAdv(result.Cookie, result.CookieCollection);
            //返回的Html内容
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }
            else
            {
                bError = true;
            }
            return result;
        }
        private byte[] bulidImageBytes(string boundary, byte[] imageBytes, string filename = null)
        {
            string startBoundary = "--" + boundary;
            string endBoundary = "--" + boundary + "--";
            string contentType = "Content-Disposition: form-data; name=\"file\"; filename=\""+(filename==null?"blob":filename)+"\"" + Environment.NewLine; ;
            contentType += "Content-Type:image/png" + Environment.NewLine;
            contentType += Environment.NewLine;
            string head = startBoundary + Environment.NewLine + contentType;
            byte[] headBytes = System.Text.Encoding.ASCII.GetBytes(head);
            byte[] endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes(Environment.NewLine+endBoundary);

            byte[] dataBytes = new byte[headBytes.Length + imageBytes.Length+ endBoundaryBytes.Length];
            headBytes.CopyTo(dataBytes, 0);
            imageBytes.CopyTo(dataBytes,headBytes.Length);
            endBoundaryBytes.CopyTo(dataBytes,headBytes.Length + imageBytes.Length);
            return dataBytes;
        }
        public Dictionary<String, String> CookiesDic = new Dictionary<string, string>();
        private void SetCookiesAdv(string sHtml,CookieCollection cookieCollection)
        {
            if (sHtml == null || sHtml == "")
            {
                return;
            }
            this.FormateCookies = sHtml;
           
            //Set-Cookie: b_110128=0; domain=.qidian.com; expires=Fri, 15-Sep-2023 15:48:41 GMT; path=/
            //CookiesDic.Clear();

            foreach (string cstr in sCookies.Split(';'))
            {
                if (!cstr.Contains("="))
                {
                    continue;
                }
                int length = cstr.Length;
                int position = cstr.IndexOf('=');
                string sName = cstr.Substring(0, position).Trim();
                string sValue = cstr.Substring(position + 1, length - position - 1);

                if (!CookiesDic.Keys.Contains(sName))
                {
                    CookiesDic.Add(sName, sValue);
                }
            }
            foreach (String cookieStr in sHtml.Split(new char[] { ',',';'}))
            {
                if(cookieStr.IndexOf('=')> 0)
                {
                    int length = cookieStr.Split(';')[0].Length;
                    int position = cookieStr.Split(';')[0].IndexOf('=');
                    string sName = cookieStr.Split(';')[0].Substring(0, position).Trim();

                    string sValue = cookieStr.Split(';')[0].Substring(position + 1, length - position - 1);
                    if(sName.ToLower()  != "domain" && sName.ToLower() != "expires" && sName.ToLower() != "path" && "max-age" != sName.ToLower())
                    {
                        
                        if(CookiesDic.Keys.Contains(sName))
                        {
                           CookiesDic[sName] = sValue;
                        }
                        else
                        {
                            CookiesDic.Add(sName,sValue);
                        }
                    }
                }
            }
            
           string  scookie = "";
            foreach (string sName in CookiesDic.Keys)
            {
                string sValue = CookiesDic[sName];
                scookie += sName + "=" + sValue + ";";
            }
            sCookies = scookie;
        }
        private void SetCookiesDic(string sHtml)
        {
            if (sHtml == null || sHtml == "")
            {
                return;
            }
            //Set-Cookie: b_110128=0; domain=.qidian.com; expires=Fri, 15-Sep-2023 15:48:41 GMT; path=/
            //CookiesDic.Clear();
            foreach (string cstr in sHtml.Split(';'))
            {
                if (!cstr.Contains("="))
                {
                    continue;
                }
                string[] cs = cstr.Split('=');
                string sName = cs[0];
                string sValue = cs.Length > 1 ? cs[1] : "";
                if (!CookiesDic.Keys.Contains(sName))
                {
                    CookiesDic.Add(sName, sValue);
                }
            }
           
        }
        //private void SetCookies(string sHtml)
        //{
        //    if (sHtml == null || sHtml == "")
        //    {
        //        return;
        //    }
        //    //Set-Cookie: b_110128=0; domain=.qidian.com; expires=Fri, 15-Sep-2023 15:48:41 GMT; path=/
        //    string sName = "";
        //    string sValue = "";
        //    MatchCollection mc;
        //    Match m;
        //    Regex r;
        //    if (!sCookies.EndsWith(";") && sCookies != "")
        //    {
        //        sCookies += ";";
        //    }
        //    r = new Regex("(?<sName>.*?)=(?<sValue>.*?);", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //    mc = r.Matches(sHtml);
        //    for (int i = 0; i < mc.Count; i++)
        //    {
        //        sName = mc[i].Groups["sName"].Value.Trim();
        //        sValue = mc[i].Groups["sValue"].Value.Trim();
        //        if (sName.ToLower().Trim() == "expires" || sName.ToLower().Trim() == "path")
        //        {
        //            continue;
        //        }
        //        r = new Regex(sName + "\\s*=\\s*.*?;", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        //        m = r.Match(sCookies);
        //        if (m.Success)
        //        {
        //            sCookies = sCookies.Replace(m.Value, sName + "=" + sValue + ";");
        //        }
        //        else
        //        {
        //            sCookies += sName + "=" + sValue + ";";
        //        }
        //    }
        //}
        public HttpResult Get(string sUrl, string sCode = "UTF-8")//根据不同方式
        {
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }
            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "get",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "*/*",//    可选项有默认值
                ContentType = "text/plain;charset=UTF-8",//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                //Postdata = "C:\\PERKYSU_20121129150608_ScrubLog.txt",//Post数据     可选项GET时不需要写
                //PostDataType = PostDataType.FilePath,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                ProxyIp = "ieproxy",
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                //PostdataByte = System.Text.Encoding.Default.GetBytes("测试一下"),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来
            };
            
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            item.Accept=("text/html,application / xhtml + xml,application / xml; q = 0.9,image / webp,image / apng,*/*;q=0.8,application/signed-exchange;v=b3");
            item.Header.Add("Accept-Encoding: gzip, deflate, br");//gzip, deflate, br
            item.Header.Add("Accept-Language: zh-CN,zh;q=0.9");//zh-CN,zh;q=0.9
            item.Header.Add(string.Format("Authorization: {0}", Authorization));
            foreach (string h in Header.Split(';'))
            {
                if (null != h && "" != h)
                {
                    item.Header.Add(h);
                }

            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
             SetCookiesAdv(result.Cookie, result.CookieCollection);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }

            else
            {
                bError = true;
            }
            //返回的Html内容
            return result;

        }
        public HttpResult DownLoad(string sUrl,String filePath, string sCode = "UTF-8")//根据不同方式
        {
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }
            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                //Encoding = Encoding.Default,
                Method = "get",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "*/*",//    可选项有默认值
                ContentType = "text/plain;charset=UTF-8",//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                //CerPath = "d:\\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                //Postdata = "C:\\PERKYSU_20121129150608_ScrubLog.txt",//Post数据     可选项GET时不需要写
                //PostDataType = PostDataType.FilePath,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                ProxyIp = "ieproxy",
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                //PostdataByte = System.Text.Encoding.Default.GetBytes("测试一下"),//如果PostDataType为Byte时要设置本属性的值
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来
            };

            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            item.Accept = ("text/html,application / xhtml + xml,application / xml; q = 0.9,image / webp,image / apng,*/*;q=0.8,application/signed-exchange;v=b3");
            item.Header.Add("Accept-Encoding: gzip, deflate, br");//gzip, deflate, br
            item.Header.Add("Accept-Language: zh-CN,zh;q=0.9");//zh-CN,zh;q=0.9
            item.Header.Add(string.Format("Authorization: {0}", Authorization));
            foreach (string h in Header.Split(';'))
            {
                if (null != h && "" != h)
                {
                    item.Header.Add(h);
                }

            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.DownLoadFile(item,filePath);
            //取出返回的Cookie
             SetCookiesAdv(result.Cookie, result.CookieCollection);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }

            else
            {
                bError = true;
            }
            //返回的Html内容
            return result;

        }
        public HttpResult Delete(string sUrl, string sCode = "UTF-8")//根据不同方式
        {
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }

            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Method = "DELETE",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/plain, */*",//    可选项有默认值
                ContentType = sContentType.Length > 0 ? sContentType : "application/json;text/plain;charset=UTF-8",//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                PostDataType = PostDataType.String,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                ProxyIp = "ieproxy",
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来

            };
 
            if (Origin != null && Origin.Length > 5)
            {
                item.Header.Add("origin: " + Origin);//zh-CN,zh;q=0.9
            }

            if (Authorization != null && Authorization.Length > 10)
            {
                item.Header.Add(string.Format("Authorization: {0}", Authorization));
            }


            if (Cookies.Count > 0)
            {
                foreach (Cookie c in Cookies)
                {
                    if (sCookies.Contains(c.Name))
                    {
                        string prefix = sCookies.Length > 0 ? "," : "";
                        sCookies += prefix + c.Name + "=" + c.Value;
                    }
                }
            }
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
             SetCookiesAdv(result.Cookie, result.CookieCollection);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }

            else
            {
                bError = true;
            }
            //返回的Html内容
            return result;

        }

        public HttpResult Delete(string sUrl, string sDeleteString, string sCode = "UTF-8")//根据不同方式
        {
            Encoding e = null;
            try
            {
                e = Encoding.GetEncoding(sCode);
            }
            catch
            {

            }

            HttpItem item = new HttpItem()
            {
                URL = sUrl,//URL     必需项
                Encoding = e,//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Method = "DELETE",//URL     可选项 默认为Get
                Timeout = iTimeOut,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = iTimeOut,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = sCookies,//字符串Cookie     可选项
                UserAgent = UserAgent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/plain, */*",//    可选项有默认值
                ContentType = sContentType.Length > 0 ? sContentType : "application/json;text/plain;charset=UTF-8",//返回类型    可选项有默认值
                Referer = Referer,//来源URL     可选项
                Allowautoredirect = true,//是否根据３０１跳转     可选项
                Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = sDeleteString,//Post数据     可选项GET时不需要写
                PostDataType = PostDataType.String,//默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                //ProxyIp = "192.168.1.105：8015",//代理服务器ID 端口可以直接加到后面以：分开就行了    可选项 不需要代理 时可以不设置这三个参数
                ProxyIp = "ieproxy",
                //CookieCollection = cookieCollection,//可以直接传一个Cookie集合进来

            };

            if (Origin != null && Origin.Length > 5)
            {
                item.Header.Add("origin: " + Origin);//zh-CN,zh;q=0.9
            }
            //if (Origin != null && Origin.Length > 5)
            //{
            //    //sc-fe-ver: v200410_drc_hotfix
            //    item.Header.Add("sc-fe-ver: " + "v200410_drc_hotfix");//zh-CN,zh;q=0.9
            //}
            if (Authorization != null && Authorization.Length > 10)
            {
                item.Header.Add(string.Format("Authorization: {0}", Authorization));
            }


            if (Cookies.Count > 0)
            {
                foreach (Cookie c in Cookies)
                {
                    if (sCookies.Contains(c.Name))
                    {
                        string prefix = sCookies.Length > 0 ? "," : "";
                        sCookies += prefix + c.Name + "=" + c.Value;
                    }
                }
            }
            if (bProxy)
            {
                item.ProxyIp = sProxyIP + ":" + sProxyPort;
                item.ProxyUserName = sProxyUserName;
                item.ProxyPwd = sProxyPassWord;
            }
            //得到HTML代码
            HttpHelper http = new HttpHelper();
            HttpResult result = http.GetHtml(item);
            //取出返回的Cookie
             SetCookiesAdv(result.Cookie, result.CookieCollection);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                bError = false;
            }

            else
            {
                bError = true;
            }
            //返回的Html内容
            return result;

        }
    }
}
