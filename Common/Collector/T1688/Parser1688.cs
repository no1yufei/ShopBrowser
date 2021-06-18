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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Collector.T1688
{
    public class Parser1688 : Parser
    {
        public Parser1688()
        {
            PlateName = "ali";
            EnterURL = "https://www.1688.com/";
            SearchURL = "";//"https://s.1688.com/selloffer/";
            DetailURL = "https://detail.1688.com/offer/";//592237780063.html
            IsSupportBatch = true;
            IsWebbrower = true;
            IsDataBase = false;

            SKUPrefix = "DALI";
        }
        public override string GetDetailPageById(string id)
        {
            return DetailURL + id +".html";
        }
        public override string MakeSearchURL(string keyword, string provice,int page= 1,int minPrice =1, int maxPirce = 10000,int count = 60)
        {
            string url = "https://s.1688.com/selloffer/rpc_async_render.jsonp?priceStart=1&uniqfield=pic_tag_id&filt=y&n=y&templateConfigName=marketOfferresult&offset=0&asyncCount=0&startIndex=0&async=false&enableAsync=false&rpcflag=new&_pageName_=market";
            url += "&pageSize=" + (count >  80?80:count);
            url += "&beginPage=" + page;
            url += "&priceStart=" + minPrice;
            url += "&priceEnd=" + maxPirce;
            url += "&province=" + Tool.GetGBKHexString(provice,'%');
            url += "&keywords="+ Tool.GetGBKHexString(keyword, '%');
            return url;
        }
        override public ProdFormat PaserDetailPage(string html)
        {
            try
            {
                AliProdFormat aliProdFormat = new AliProdFormat();
                return aliProdFormat.WebSourceFormat(html);
            }
            catch(Exception xe)
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
            List<ParserProductInfo> searchResult = new List<ParserProductInfo>();
            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(html);
            if (null != document)
            {
                //页面产品名称
                HtmlNodeCollection nodeProducts = document.DocumentNode.SelectNodes("//ul[@id='sm-offer-list' or @class='offer-list-row']/li");
                if (null != nodeProducts)
                {
                    foreach (HtmlNode node in nodeProducts)
                    {
                        try
                        {
                            HtmlAttribute offerIDAttribute = node.Attributes.FirstOrDefault(p => p.Name == "offerid" || p.Name == "data-offerid");
                            if (null != offerIDAttribute)
                            {
                                ParserProductInfo baseInfo = new ParserProductInfo();
                                baseInfo.PID = offerIDAttribute.Value;
                                baseInfo.SKU = SKUPrefix + baseInfo.PID;
                                HtmlNode titleNode = node.SelectSingleNode("div/div/a");
                                if (null == titleNode)
                                {
                                    titleNode = node.SelectSingleNode("div/a");
                                }
                                if (null != titleNode)
                                {
                                    baseInfo.URL = "https://detail.1688.com/offer/" + baseInfo.PID + ".html";
                                    //titleNode.Attributes.FirstOrDefault(p => p.Name == "href").Value;
                                    baseInfo.Name = titleNode.Attributes.FirstOrDefault(p => p.Name == "title").Value;
                                    baseInfo.Price = node.SelectSingleNode("div/div/span").InnerText.Replace("&yen;", "").Replace("\n", "");
                                    searchResult.Add(baseInfo);
                                }
                            }
                        }
                        catch (Exception xe)
                        {
                            Console.WriteLine("采集1688失败：" + xe.Message);
                        }
                    }
                    Console.WriteLine("采集1688关键字主要信息成功：" + searchResult.Count);
                }
                else
                {
                    nodeProducts = document.DocumentNode.SelectNodes("//ul[@id='sm-offer-list' or @class='offer-list']/div[@class='card-container']/div[@class='common-offer-card']");
                    if (null != nodeProducts)
                    {
                        foreach (HtmlNode node in nodeProducts)
                        {
                            try
                            {
                                HtmlNode descNode
                                    = node.SelectSingleNode("div[@class='desc-container']");
                                //HtmlAttribute offerIDAttribute = node.Attributes.FirstOrDefault(p => p.Name == "offerid" || p.Name == "data-offerid");
                                if (null != descNode && null != descNode.SelectSingleNode("div/a"))
                                {
                                    ParserProductInfo baseInfo = new ParserProductInfo();
                                    baseInfo.URL = descNode.SelectSingleNode("div/a").Attributes.FirstOrDefault(p => p.Name == "href").Value;
                                    baseInfo.PID = baseInfo.URL.Replace("https://detail.1688.com/offer/","").Replace(".html","");
                                    baseInfo.SKU = SKUPrefix + baseInfo.PID;
                                    baseInfo.Name = descNode.SelectSingleNode("div/a/div").InnerText;
                                    baseInfo.Price = descNode.SelectSingleNode("div/div[@class='price']").InnerText.Replace("&yen;", "").Replace("\n", "");
                                    searchResult.Add(baseInfo);
             
                                }
                            }
                            catch (Exception xe)
                            {
                                Console.WriteLine("采集1688失败：" + xe.Message);
                            }
                        }
                        Console.WriteLine("采集1688关键字主要信息成功：" + searchResult.Count);
                    }
                    else
                    {

                        Console.WriteLine("采集1688关键字主要信息失败！未能在页面中找到产品列表，可能操作太快，请稍后再试");
                    }
                }
            }
            else
            {
                Console.WriteLine("采集1688关键字主要信息失败！" + html);
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
                    HtmlNode nodeTitle = document.DocumentNode.SelectSingleNode("//html/head/title");
                    string title = nodeTitle.InnerHtml;
                    title = title.Replace(" - 阿里巴巴", "");
                    baseInfo.Name = title;
                    HtmlNode node = document.DocumentNode.SelectSingleNode("//html/head/meta[@name='b2c_auction']");
                    baseInfo.PID = node.GetAttributeValue("content", "");
                    baseInfo.SKU = SKUPrefix + baseInfo.PID;
                    node = document.DocumentNode.SelectSingleNode("//html/head/meta[@property='og:product:price']");
                    baseInfo.Price = node.GetAttributeValue("content", "");
                    node = document.DocumentNode.SelectSingleNode("//html/head/link[@rel='canonical']");
                    baseInfo.URL = node.GetAttributeValue("href", "");
                    node = document.DocumentNode.SelectSingleNode("//html/head/meta[@property = 'og:image']");
                    baseInfo.MainImageUrl = node.GetAttributeValue("content", "");
                }
                catch (Exception xe)
                {
                    Console.WriteLine("采集详情页概要信息 失败" + xe.Message);
                }
            }
            //if (!ParserProductInfos.Contains(baseInfo))
            //{
            //    ParserProductInfos.Insert(0, baseInfo);
            //}
            return baseInfo;
        }

    }
  
}
