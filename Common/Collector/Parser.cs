using Common.Tools.ProdFormater;
using CsharpHttpHelper;
using HtmlAgilityPack;
using LitJson;
using Newtonsoft.Json;
using ShopeeChat;
using ShopeeChat.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Collector
{
    public class Parser
    {
        public  List<ParserProductInfo> ParserProductInfos = new List<ParserProductInfo>();
        /// <summary>
        /// 采集平台的名称,用英文和数字标识
        /// </summary>
        public string PlateName ="unkown";

        /// <summary>
        /// 平台入口
        /// </summary>
        public string EnterURL = "www.baidu.com";
        /// <summary>
        /// SKU的前缀,用来区别平台
        /// </summary>
        public string SKUPrefix = "DLTC";
        /// <summary>
        /// 搜索页地址关键字段，用来判断是否在关键页面
        /// </summary>
        public string SearchURL = "www.baidu.com";// = "https://s.1688.com/selloffer/offer_search.htm";
        /// <summary>
        /// 详情页关键字段，用来判断是否在详情页。
        /// </summary>
        public string DetailURL= "www.baidu.com";// = "https://detail.1688.com/offer";//592237780063.html

        /// <summary>
        /// 是否支持批量采集
        /// </summary>
        public bool IsSupportBatch = false;

        /// <summary>
        ///  采集数据源从浏览器
        /// </summary>
        public bool IsWebbrower = true;
        /// <summary>
        /// 采集数据源从数据库采集
        /// </summary>
        public bool IsDataBase = false;
        /// <param name="binfo"></param>
        /// <returns></returns>
        virtual public string GetSKU(ParserProductInfo binfo)
        {
            if(binfo.PID != null && binfo.PID.Length > 0)
            {
                binfo.SKU = SKUPrefix + binfo.PID;
            }
            return binfo.SKU;
        }

        virtual public string GetDetailPageById(string id)
        {
            return id;
        }
        virtual public string MakeSearchURL(string keyword, string province, int page = 1, int minPrice = 1, int maxPrice = 10000, int count = 50)
        {
            return "";
        }
        /// <summary>
        /// 根据传入的HTML页面解析产品
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        virtual public ProdFormat PaserDetailPage(string html)
        {
            throw new Exception("需要实现采集函数");
        }

        /// <summary>
        /// 解析平台的搜索页的概要信息。如果不支持批量采集，不用实现该方法，并设置IsSupportBatch=false
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        virtual public List<ParserProductInfo> ParserOutlineInfosFromSearchPage(string html)
        {
            List<ParserProductInfo> searchResult = new List<ParserProductInfo>();
            Console.WriteLine("当前平台不支持批量采集！");
            return searchResult;
        }
        /// <summary>
        /// 通过详情页，获取概要信息,需要用来在列表页当中呈现当前采集的列表
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        virtual public ParserProductInfo ParserOutlineInfosFromDetailPage(string html)
        {
            ParserProductInfo baseInfo = new ParserProductInfo();
            Console.WriteLine("当前平台不支持采集！");
            return baseInfo;
        }

        /// <summary>
        /// 通过URL直接Html页面，部分平台不支持
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPageHtml(string id,string url, string code = "GBK")
        {
            HtmlHttpHelper hhh = new HtmlHttpHelper();
            if(!url.Contains(DetailURL))
            {
                url = GetDetailPageById(id);
            }
            HttpResult ret = hhh.Get(url, code);
            string html = "";
            if (null != ret.Html && ret.StatusCode == System.Net.HttpStatusCode.OK)
            {
                html = ret.Html;
                html = html.Replace("\r", "").Replace("\n", "");
                Console.WriteLine("获取详情页成功");
            }
            else
            {
                Console.WriteLine("获取详情页失败！" + ret.Html);
            }
            return html;
        }
        /// <summary>
        /// 通过概要详情获取页面，如果之前已经保存过详情页，则直接返回。
        /// </summary>
        /// <param name="binfo"></param>
        /// <returns></returns>
        public string GetPageHtml(ParserProductInfo binfo)
        {
            if(binfo == null || binfo.HTML == null || binfo.HTML.Length < 200)
            {
                binfo.HTML = GetPageHtml(binfo.PID,binfo.URL); ;
            }
            return binfo.HTML;
        }

        protected HtmlAgilityPack.HtmlDocument getHtmlDocument(String htmlText)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            //htmlWeb.UserAgent = "Mozilla/5.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.Unused;

            try
            {
                document.LoadHtml(htmlText);
                statusCode = htmlWeb.StatusCode;
            }
            catch (System.Net.WebException ex)
            {
                return null;
            }
            catch (Exception ex1)
            {
                return null;
            }
            return document;
        }

        public void LoadData(string dataPath)
        {
            if(!System.IO.Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string filename = dataPath + PlateName;
            List < ParserProductInfo > infos = Tool.LoadSerializationFromFile<List<ParserProductInfo>>(filename);
            if(null != infos)
            {
                this.ParserProductInfos = infos;
            }
            
        }
        public void SaveData(string dataPath)
        {
            if (!System.IO.Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            string filename = dataPath + PlateName;
            Tool.SaveSerializationToFile<List<ParserProductInfo>>(filename, this.ParserProductInfos);
        }
    }
    public class ParserStatus
    {
        static public string StatusUnHandle = "未处理";
        static public string StatusUnListed = "已上传";
        static public string StatusListed = "已上架";
        static public string StatusFailToUpload = "上传失败";
    }
}
