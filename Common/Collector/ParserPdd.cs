using Common.Tools.ProdFormater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Collector
{
    public class ParserPdd : Parser
    {
        public ParserPdd()
        {
            PlateName = "pdd";
            EnterURL = "https://mobile.yangkeduo.com/";
            SearchURL = "";//"https://s.1688.com/selloffer/";
            DetailURL = "https://mobile.yangkeduo.com/goods.html?goods_id=";//45577550011
            IsSupportBatch = true;
            IsWebbrower = true;
            IsDataBase = false;

            SKUPrefix = "DPDD";
        }
        public override string GetDetailPageById(string id)
        {
            return DetailURL + id;
        }
        public override string MakeSearchURL(string keyword, string provice, int page = 1, int minPrice = 1, int maxPirce = 10000, int count = 60)
        {
            string url = "https://mobile.yangkeduo.com/search_result.html?search_key="+ keyword;
            return url;
        }
        override public ProdFormat PaserDetailPage(string html)
        {
            try
            {
                PddProdFormat pddProdFormat = new PddProdFormat();
                return pddProdFormat.WebSourceFormat(html);
            }
            catch (Exception xe)
            {
                Console.WriteLine("页面解析错误：" + xe.Message);
            }
            return null;
        }

        /// <summary>
        /// 解析1688的搜索页的概要信息。
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        override public List<ParserProductInfo> ParserOutlineInfosFromSearchPage(string html)
        {
            //{"props"(.*?)</script>
            List<ParserProductInfo> searchResult = new List<ParserProductInfo>();
            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(html);
            if (null != document)
            {
                Regex regex = new Regex(@"{""props""(.*?)</script>");
                Match match = regex.Match(html);
                if (match.Success)
                {
                    string json = match.Value.ToString().Replace("</script>", "");





                }



                //页面产品名称
                //HtmlNodeCollection nodeProducts = document.DocumentNode.SelectNodes("//ul[@id='sm-offer-list' or @class='offer-list-row']/li");
                //if (null != nodeProducts)
                //{
                //    foreach (HtmlNode node in nodeProducts)
                //    {
                //        try
                //        {
                //            HtmlAttribute offerIDAttribute = node.Attributes.FirstOrDefault(p => p.Name == "offerid" || p.Name == "data-offerid");
                //            if (null != offerIDAttribute)
                //            {
                //                ParserProductInfo baseInfo = new ParserProductInfo();
                //                baseInfo.PID = offerIDAttribute.Value;
                //                baseInfo.SKU = SKUPrefix + baseInfo.PID;
                //                HtmlNode titleNode = node.SelectSingleNode("div/div/a");
                //                if (null == titleNode)
                //                {
                //                    titleNode = node.SelectSingleNode("div/a");
                //                }
                //                if (null != titleNode)
                //                {
                //                    baseInfo.URL = titleNode.Attributes.FirstOrDefault(p => p.Name == "href").Value;
                //                    baseInfo.Name = titleNode.Attributes.FirstOrDefault(p => p.Name == "title").Value;
                //                    baseInfo.Price = node.SelectSingleNode("div/div/span").InnerText.Replace("&yen;", "").Replace("\n", "");
                //                    searchResult.Add(baseInfo);
                //                }
                //            }
                //        }
                //        catch (Exception xe)
                //        {
                //            Console.WriteLine("采集1688失败：" + xe.Message);
                //        }
                //    }
                //    Console.WriteLine("采集1688关键字主要信息成功：" + searchResult.Count);
                //}
                //else
                //{

                //    Console.WriteLine("采集1688关键字主要信息失败！未能在页面中找到产品列表，可能操作太快，请稍后再试");
                //}
            }
            else
            {
                Console.WriteLine("采集拼多多关键字主要信息失败！" + html);
            }

            return searchResult;
        }
        /// <summary>
        /// 通过详情页，获取概要信息
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        override public ParserProductInfo ParserOutlineInfosFromDetailPage(string html)
        {
            string websrc = html.Replace("\r", "").Replace("\n", "");
            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(websrc);
            ParserProductInfo baseInfo = new ParserProductInfo();
            if (null != document)
            {
                try
                {
                    //页面产品名称
                    //HtmlNode nodeTitle = document.DocumentNode.SelectSingleNode("//html/head/title");
                    //string title = nodeTitle.InnerHtml;
                    //title = title.Replace(" - 阿里巴巴", "");
                    //baseInfo.Name = title;
                    //HtmlNode node = document.DocumentNode.SelectSingleNode("//html/head/meta[@name='b2c_auction']");
                    //baseInfo.PID = node.GetAttributeValue("content", "");
                    //baseInfo.SKU = SKUPrefix + baseInfo.PID;
                    //node = document.DocumentNode.SelectSingleNode("//html/head/meta[@property='og:product:price']");
                    //baseInfo.Price = node.GetAttributeValue("content", "");
                    //node = document.DocumentNode.SelectSingleNode("//html/head/link[@rel='canonical']");
                    //baseInfo.URL = node.GetAttributeValue("href", "");
                    //node = document.DocumentNode.SelectSingleNode("//html/head/meta[@property = 'og:image']");
                    //baseInfo.MainImageUrl = node.GetAttributeValue("content", "");
                }
                catch (Exception xe)
                {
                    Console.WriteLine("采集详情页概要信息 失败" + xe.Message);
                }
            }
            return baseInfo;
        }
    }
}
