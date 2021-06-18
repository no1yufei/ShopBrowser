using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
{
    public class GoogleTrans
    {
        public int TimeSpan = 0;
        private DateTime lastQueryTime = DateTime.MinValue;
        HtmlHttpHelper hhh = new HtmlHttpHelper();
        ///转半角的函数(DBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248//   
        private  string toDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] == 12290)
                {
                    array[i] = (char)46;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }

        private string makeQueryString(String q, String to = "en", String from = "auto")
        {
            string baseURL = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="+from+"&tl="+to+"&dt=t&q="+ System.Web.HttpUtility.UrlDecode(toDBC(q), System.Text.Encoding.UTF8); ;
            return baseURL;
        }

        private string httpGet(string Url, string postDataStr = "")
        {
            hhh.iTimeOut = 100000;
            return hhh.Get(Url).Html;
        }

        static string lastOrgString = null;
        static string lastDecString = null;
        static string lastTo = null;
        public string Translate(string originalText, string to = "auto", string from = "auto")
        {
            if(to.ToLower() == "zh-tw" && from.ToLower()== "zh-cn")
            {
                return Strings.StrConv(originalText, VbStrConv.TraditionalChinese, 0);
            }
             originalText = formatString(originalText);
            if (lastOrgString == originalText && lastTo==to)
            {
                return lastDecString;
            }
            int looptime = 0;
            while((DateTime.Now - lastQueryTime).TotalMilliseconds < TimeSpan *  (1 + (new Random()).NextDouble()))
            {
                looptime++;
                Thread.Sleep((int)(DateTime.Now - lastQueryTime).TotalMilliseconds);
                if(looptime > 20)
                {
                    return "***********系统性错误*****************";
                }
            }
            
            String desctString = "";
            if(originalText != "")
            {
                looptime = TimeSpan > 0 ? 0 : 1;
                lastQueryTime = DateTime.Now;

                
                bool bSucessed = false;
                String result = "未能取得结果";
                while (looptime++ < 2 && !bSucessed)
                {
                    try
                    {
                        result = httpGet(makeQueryString(originalText, to, from));
                        /*下面是解析JArray的部分*/
                        JArray jlist = JArray.Parse(result); //将pois部分视为一个JObject，JArray解析这个JObject的字符串 
                        for (int i = 0; i < jlist[0].Count(); i++)
                        {
                            desctString += jlist[0][i][0].ToString();
                        }

                        lastDecString = desctString;
                        lastTo = to;
                        lastOrgString = originalText;
                        bSucessed = true;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(10000);
                        desctString = "**************************翻译错误：" + result + ex.Message;
                        lastDecString = "";
                        Console.WriteLine("Google翻译：" + result + ex.Message);
                    }
                }
               

            }

            return unformatString(desctString);
        }
        private string formatString(string source)
        {
            return source.Replace("#", "*@*");
        }
        private string unformatString(string source)
        {
            return source.Replace("* @ * ", "#");
        }
    }
   
}
