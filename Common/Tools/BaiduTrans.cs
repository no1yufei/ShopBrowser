using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
{
    public class BaiduTrans
    {
        private string makeQueryString(String q, String to = "en", String from = "auto")
        {
            //textBox2.Text = System.Web.HttpUtility.UrlDecode(textBox1.Text, System.Text.Encoding.GetEncoding("GB2312"));//将Url中的编码转换为简体汉字

            //textBox2.Text = System.Web.HttpUtility.UrlEncode(textBox1.Text, System.Text.Encoding.GetEncoding("GB2312"));//将简体汉字转换为Url编码
            string appid = "20180510000156564";
            MD5 md5 = new MD5CryptoServiceProvider();
            string secretKey = "5cH3lrxAWGm2eOCIdRra";
            string baseURL = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
            int salt = (new Random()).Next();
            String utf8q = System.Web.HttpUtility.UrlEncode(q);
            String qURL = baseURL + "q=" + utf8q;
            qURL += "&from=" + from;
            qURL += "&to=" + to;
            qURL += "&appid=" + appid;
            qURL += "&salt=" + salt;
            string signString = appid + q + salt + secretKey;
            qURL += "&sign=" + BitConverter.ToString(md5.ComputeHash((Encoding.UTF8.GetBytes(signString)))).Replace("-", "").ToLower();
            return qURL;
        }

        private string httpGet(string Url, string postDataStr = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public string Translate(string originalText, string to = "auto", string from = "auto")
        {
            String result = httpGet(makeQueryString(originalText, to, from));
            String desctString = "";
            if (BaiduTransError.IsErrorJason(result))
            {
                BaiduTransError transResult = JsonConvert.DeserializeObject<BaiduTransError>(result);
                desctString = String.Format("{0}-->{1}", transResult.error_code, transResult.error_msg);
            }
            else
            {
                BaiduTransResult transResult = JsonConvert.DeserializeObject<BaiduTransResult>(result);
                foreach (TransText tt in transResult.trans_result)
                {
                    //desctString += String.Format("{0}-->{1}\n", tt.src, tt.dst);
                    desctString += String.Format("{0}\n", tt.dst);
                }

            }
            return desctString;
        }
    }
    public class BaiduTransError
    {
        public String error_code;
        public String error_msg;

        public static bool IsErrorJason(String resultStr)
        {
            return resultStr.StartsWith("{\"error_code\"");
        }
    }
    public class BaiduTransResult
    {
        public String from;
        public String to;
        public TransText[] trans_result;
    }
    public class TransText
    {
        public String src;
        public String dst;
    }
}
