using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Tools.ProdFormater
{
    public class TaobaoProdFormat : ProdFormat
    {
        public override ProdFormat WebSourceFormat(string webSrc)
        {
            string webSource = webSrc.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(webSource);

            if (null != document)
            {
                wAbbrName = "TAB";
                pOneSend = 1;
                ////*[@id="J_ShopInfo"]/div[2]/div[3]/a[1]
                HtmlNode nodeShopInfo = document.DocumentNode.SelectSingleNode("//*[@id='J_ShopInfo']/div[2]/div[3]/a[1]");
                if (null != nodeShopInfo)
                {
                    sShopUrl = "https:" + nodeShopInfo.Attributes["href"].Value.ToString();
                }
                HtmlNode nodeJPine = document.DocumentNode.SelectSingleNode("//*[@id='J_Pine']");
                if (null != nodeJPine)
                {
                    offerid = nodeJPine.Attributes["data-itemid"].Value.ToString();
                    catid = nodeJPine.Attributes["data-catid"].Value.ToString();
                    dcatid = "";
                    parentdcatid = "";
                    //memberid = nodeJPine.Attributes["data-sellerid"].Value.ToString();


                    //shopid = nodeJPine.Attributes["data-shopid"].Value.ToString();

                    wLink = "https://item.taobao.com/item.htm?id=" + offerid;
                }

                beginAmount = 1;

                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                pCurrency = "CNY";
                //标题//*[@id="J_Title"]
                HtmlNode nodeJTitle = document.DocumentNode.SelectSingleNode("//*[@id='J_Title']/h3");
                if (null != nodeJTitle)
                {
                    pTitle = nodeJTitle.Attributes["data-title"].Value.ToString();

                }
                //J_StrPriceModBox
                //HtmlNode nodeJStrPriceModBox = document.DocumentNode.SelectSingleNode("//*[@id='J_StrPriceModBox']");
                //if (null != nodeJStrPriceModBox)
                //{
                //    string strprice = nodeJStrPriceModBox.OuterHtml;
                //    strprice = Regex.Replace(strprice, "<(.*?)>", "");
                //    refPrice = strprice.Replace("价格", "").Replace("¥", "").Trim();
                //    //try
                //    //{
                //    //    saleCount = long.Parse(nodeJSellCounter.InnerHtml.Trim());
                //    //}
                //    //catch (Exception ee)
                //    //{
                //    //    saleCount = 0;
                //    //}

                //}
                //价格//*[@id="J_PromoPrice"]
                HtmlNode nodeJPromoPrice = document.DocumentNode.SelectSingleNode("//*[@id='J_PromoPrice']");
                if (null != nodeJPromoPrice)
                {
                    //price = nodeJPromoPrice.OuterHtml;
                    //Regex regexPrice = new Regex(@"\d+.\d+");
                    //Match matchPrice = regexPrice.Match(price);
                    //if (matchPrice.Success)
                    //{
                    //    price = matchPrice.Groups[0].Value.ToString().Trim();
                    //}
                }
                //累计评论//*[@id="J_RateCounter"]
                //HtmlNode nodeJRateCounter = document.DocumentNode.SelectSingleNode("//*[@id='J_RateCounter']");
                //if (null != nodeJRateCounter)
                //{
                //    rateCounter = nodeJRateCounter.InnerHtml;
                //}
                //交易成功//*[@id="J_SellCounter"]
                //HtmlNode nodeJSellCounter = document.DocumentNode.SelectSingleNode("//*[@id='J_SellCounter']");
                //if (null != nodeJSellCounter)
                //{
                //    try
                //    {
                //        saleCount = long.Parse(nodeJSellCounter.InnerHtml.Trim());
                //    }catch(Exception ee)
                //    {
                //        saleCount = 0;
                //    }
                //}
                //浙江金华//*[@id="J-From"]
                HtmlNode nodeJFrom = document.DocumentNode.SelectSingleNode("//*[@id='J-From']");
                if (null != nodeJFrom)
                {
                    sProvince = nodeJFrom.InnerHtml;
                    sCity = nodeJFrom.InnerHtml;
                }
                //轮播图//*[@id="J_UlThumb"]
                HtmlNode nodeJUlThumb = document.DocumentNode.SelectSingleNode("//*[@id='J_UlThumb']");
                if (null != nodeJUlThumb)
                {
                    pSlideImages = new List<string>();
                    HtmlNodeCollection nodesImg = nodeJUlThumb.SelectNodes(".//img");
                    if (nodesImg != null)  //没有img节点时出错
                    {
                        //处理html字符串中img标签的src属性
                        foreach (HtmlNode hnImg in nodesImg)
                        {
                            if (hnImg.Attributes["src"] != null)
                            {
                                string link = hnImg.Attributes["src"].Value.ToString();
                                link = Regex.Replace(link, "_\\d+x\\d+(.*?)$", "");
                                link = (link.StartsWith("http")?"": "http:") + link;
                                if (!pSlideImages.Contains(link)&& link.Contains("tbvideo")==false)
                                {
                                    pSlideImages.Add(link);
                                    if (pThumbnail == null || pThumbnail.Equals(""))
                                    {
                                        pThumbnail = link;
                                    }
                                }
                            }
                        }
                    }
                }
                Dictionary<string, List<string[]>> dictVariNameInfo = new Dictionary<string, List<string[]>>();
                //sku 尺码 颜色等等//*[@id="J_isku"] 变体1和变体2
                HtmlNode nodeJisku = document.DocumentNode.SelectSingleNode("//*[@id='J_isku']");
                if (null != nodeJisku)
                {
                    //skuInfo = nodeJisku.InnerHtml;
                    ////两个ul里面的  尺码和款式
                    //string vari1Name = "",vari2Name = "";
                    //List<string> var
                    //List<string[]> vari1Infos, vari2Infos;

                    //*[@id="J_isku"]/div/dl[1]/dd/ul/li[1]/a

                    //*[@id="J_isku"]/div/dl[2]/dt
                    //*[@id="J_isku"]/div/dl[2]/dd/ul/li[1]/a

                    pVariImages = new List<string>();
                    for (int i = 1; i < 3; i++)
                    {

                        HtmlNode hnDt = nodeJisku.SelectSingleNode(".//div/dl[" + i + "]/dt");
                        if (null != hnDt)
                        {
                            //变体类型名称 例如尺码，颜色
                            string variName = hnDt.InnerText.ToString().Trim();
                            List<string[]> listVariInfo = new List<string[]>();
                            for (int j = 1; j < 99; j++)
                            {

                                HtmlNode hnLi = nodeJisku.SelectSingleNode(".//div/dl[" + i + "]/dd/ul/li[" + j + "]");
                                HtmlNode hnA = nodeJisku.SelectSingleNode(".//div/dl[" + i + "]/dd/ul/li[" + j + "]/a");
                                if (null != hnLi && null != hnA)
                                {
                                    string[] variInfo = new string[3];//编号，名称，图片
                                    if (hnLi.Attributes.Contains("data-value"))
                                    {
                                        variInfo[0] = hnLi.Attributes["data-value"].Value.ToString().Trim();
                                    }
                                    variInfo[1] = Regex.Replace(hnA.InnerHtml.ToString().Replace("\n", ""), "<.*?>", "").Trim();
                                    if (hnA.Attributes.Contains("style"))
                                    {
                                        string skuImage = hnA.Attributes["style"].Value.ToString().Replace("background:url(", "https:");
                                        variInfo[2] = Regex.Replace(skuImage, "_\\d+x\\d+(.*?)$", "").Trim();
                                        pVariImages.Add(variInfo[2]);

                                    }
                                    if (variInfo[0].Length > 0 && variInfo[1].Length > 0)
                                    {
                                        listVariInfo.Add(variInfo);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            dictVariNameInfo.Add(variName, listVariInfo);
                        }
                    }
                }
                if (dictVariNameInfo.Count > 0)
                {
                    skuProps = new List<SkuPropsItem>();
                    foreach (var vitem in dictVariNameInfo)
                    {
                        if (vitem.Key.Equals("数量")==false)
                        {
                            SkuPropsItem spi = new SkuPropsItem();
                            skuProps.Add(spi);
                            spi.prop = vitem.Key;
                            spi.value = new List<SkuPropsItemValue>();// new SkuPropsItemValue();
                            List<string[]> variContent = vitem.Value;
                            foreach (string[] item in variContent)
                            {
                                SkuPropsItemValue spiv = new SkuPropsItemValue();
                                spiv.name = item[1];
                                spiv.imageUrl = item[2];
                                spi.value.Add(spiv);
                            }
                        }
                    }
                }
                
                //属性详情//*[@id="attributes"]
                HtmlNode nodeAttributes = document.DocumentNode.SelectSingleNode("//*[@id='attributes']");
                if (null != nodeAttributes)
                {
                    pProperty = new List<string>();

                    string attributes = nodeAttributes.InnerHtml;
                    attributes = attributes.Replace("</li>", "|");
                    attributes = Regex.Replace(attributes, "<.*?>", "");
                    attributes = attributes.Replace("&nbsp;", " ");
                    attributes = Regex.Replace(attributes, "\\s+", " ");
                    attributes = attributes.Trim();


                    string[] proPropertys = attributes.Split('|');

                    for (int j = 0; j < proPropertys.Length; j++)
                    {
                        bool isContain = false;
                        for (int k = 0; k < ProdFormatTextTool.filterString.Length; k++)
                        {
                            if (proPropertys[j].Contains(ProdFormatTextTool.filterString[k]) == true)
                            {
                                isContain = true;
                                break;
                            }
                        }
                        if (isContain == false && proPropertys[j].Trim() != "")
                        {
                            pProperty.Add(proPropertys[j]);
                        }
                    }
                }
                foreach (string item in pProperty)
                {
                    pDescription = pDescription + item + "\r\n";
                }
                //图文详情//*[@id="description"]
                //HtmlNode nodeDescription = document.DocumentNode.SelectSingleNode("//*[@id='description']");
                //if (null != nodeDescription)
                //{
                //    pDescription = Regex.Replace(nodeDescription.InnerHtml, "<(.*?)>", "");
                //    pDescription = ProdFormatTextTool.replaceHtmlSpecChar(pDescription);
                //    pDescImages = new List<string>();
                //    HtmlNodeCollection nodesImg = nodeDescription.SelectNodes(".//img");
                //    if (nodesImg != null)  //没有img节点时出错
                //    {
                //        //处理html字符串中img标签的src属性
                //        foreach (HtmlNode hnImg in nodesImg)
                //        {
                //            if (hnImg.Attributes["src"] != null)
                //            {
                //                string link = hnImg.Attributes["src"].Value.ToString();
                //                link = Regex.Replace(link, "_\\d+x\\d+(.*?)$", "");
                //                if (!pDescImages.Contains(link))
                //                {
                //                    pDescImages.Add(link);
                //                }
                //            }
                //        }
                //    }
                //}



                //string skuMap = "";// (.*?)propertyMemoMap
                Regex regexSkuMap = new Regex(@"skuMap(.*?)propertyMemoMap");
                Match matchSkuMap = regexSkuMap.Match(webSource);
                if (matchSkuMap.Success)
                {
                    skuMap = new List<SkuMapItem>();

                    string skuMapTemp = matchSkuMap.Value.ToString().Trim();
                    skuMapTemp = skuMapTemp.Replace("skuMap", "").Trim();
                    skuMapTemp = skuMapTemp.Replace(",propertyMemoMap", "").Trim();
                    skuMapTemp = Regex.Replace(skuMapTemp, "^:", "");
                    skuMapTemp = skuMapTemp.Trim();

                    JObject joSkuMap = (JObject)JsonConvert.DeserializeObject(skuMapTemp);
                    foreach (KeyValuePair<string, JToken> item in joSkuMap)
                    {
                        SkuMapItem smi = new SkuMapItem();

                        string variNameTemp = item.Key.ToString();//;20509:28317;1627207:22238305;
                        variNameTemp = Regex.Replace(variNameTemp, "^;", "");
                        variNameTemp = Regex.Replace(variNameTemp, ";$", "");
                        variNameTemp = variNameTemp.Replace(";", ">");
                        JObject joSkuMapSub = (JObject)item.Value;
                        string vPrice = joSkuMapSub["price"].ToString();
                        string vStock = joSkuMapSub["stock"].ToString();
                        string vSkuId = joSkuMapSub["skuId"].ToString();
                        foreach (var vni in dictVariNameInfo)
                        {
                            string variKey = vni.Key;
                            List<string[]> listVariInfo = vni.Value;
                            foreach (string[] variInfo in listVariInfo)
                            {
                                if (variNameTemp.Contains(variInfo[0]))
                                {
                                    variNameTemp = variNameTemp.Replace(variInfo[0], variInfo[1]);
                                }
                            }
                        }
                        smi.name = variNameTemp;
                        smi.price = double.Parse(vPrice);
                        if (pMinPrice == 0)
                        {
                            pMinPrice = smi.price;
                        }
                        else if(pMinPrice> smi.price)
                        {
                            pMinPrice = smi.price;
                        }
                        if (pMaxPrice == 0)
                        {
                            pMaxPrice = smi.price;
                        }
                        else if(pMaxPrice < smi.price)
                        {
                            pMaxPrice = smi.price;
                        }
                        smi.canBookCount = Int32.Parse(vStock);
                        smi.discountPrice = double.Parse(vPrice);
                        smi.specId = vSkuId;
                        smi.skuId = vSkuId;
                        skuMap.Add(smi);
                    }
                }
                return this;
            }
            return null;
        }
        
     }
}
