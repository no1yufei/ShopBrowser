using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Tools.ProdFormater
{
    public class AliProdFormat : ProdFormat
    {
        private string removeBrandInTitle(string name,string htmlTitle,string description)
        {
            string brand = getStringKeyValue("品牌", description);
            if (brand.Length > 0 && name.Contains(brand))
            {
                name = name.Replace(brand, "");
            }
            else
            {
                ///html的title 不含系统添加的商标，但是会有长度限制，一般会等于或者短于页面内的标题
                string[] str = name.Replace(htmlTitle.Substring(0, 3), "=").Split('=');
                if (str.Length > 1 && str[0] != null && str[0].Length > 0)
                {
                    name = name.Replace(str[0], "");
                }
            }
            return name;
        }

        public override ProdFormat WebSourceFormat(string webSrc)
        {
            string webSource = webSrc.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(webSource);

            if (null != document)
            {
                HtmlNode nodeError = document.DocumentNode.SelectSingleNode("//html/body/div/div/p");
                if(null != nodeError)
                {
                    if (nodeError.InnerHtml.Contains("验证"))
                    {
                        NetError = true;
                        Console.WriteLine("网页需要验证，暂停采集！");
                        return this;
                    }
                }

                //页面产品名称

                HtmlNode nodeTitle = document.DocumentNode.SelectSingleNode("//html/head/title");
                string htmlTitle = "";
                if (null != nodeTitle)
                {
                    if(nodeTitle.InnerHtml.Contains("404-阿里巴巴"))
                    {
                        Console.WriteLine("Error 404抱歉，您要访问的页面不存在,有可能我们的网页正在维护或者您输入的网址不正确");
                        return null;
                    }
                    if (nodeTitle.InnerHtml.Contains("1688.com"))
                    {
                        Console.WriteLine("***************/n系统查询已经被拒绝，请重新登录1688或升级VIP！/n**********************");
                        //return null;
                    }
                    htmlTitle = nodeTitle.InnerHtml.Replace(" - 阿里巴巴", "").Replace("1688.com","");
                    if(htmlTitle.Contains('_') || htmlTitle.Contains('-'))
                    {
                        try
                        {
                            htmlTitle = htmlTitle.Split(new char[] { '_', '-'})[1].Trim();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("标题不含类别：" + ex.Message);
                        }
                    }
                       
                }

                string  description = getHeadMetaValue(document, "name", "description");
                if(description.Length < 1)
                {
                    description = getHeadMetaValue(document, "property", "og:description");
                }
                description = description.Substring(description.IndexOf('。') + 1);
                description = description.Substring(description.IndexOf('。') + 1);
                description = description.Replace(description.Substring(description.LastIndexOf('。') + 1), "");
                pDescription = description;

                string title = getHeadMetaValue(document, "property", "og:title", "content");
                pTitle = removeBrandInTitle(title,htmlTitle, description);
                
                //for (int i = 0; i < ProdFormatTextTool.titleFilter.Length; i++)
                //{
                //    title = title.Replace(ProdFormatTextTool.titleFilter[i], "");
                //}
                //wTitle = title;
                wAbbrName = "ALI";

                HtmlNode nodeOneSend = document.DocumentNode.SelectSingleNode(".//*[@id='mod-detail-title']");
                if (null != nodeOneSend)
                {
                    string oneSendStr = nodeOneSend.InnerText;

                    if (oneSendStr.Contains("一件代发"))
                    {
                        pOneSend = 1;
                    }
                    pOneKey = 0;
                    //pTitle = nodeOneSend.InnerText.Replace("一件代发", "").Trim();
                }

                wLink = getHeadLinkValue(document, "rel", "canonical", "href");
                string content = getHeadMetaValue(document, "name", "keywords", "content");
                List<string> keywords = new List<string>();
                string[] kws = content.Split(',');
                foreach (string item in kws)
                {
                    keywords.Add(item.Trim());
                }
                wKeywords = keywords.ToArray();

                content = getHeadMetaValue(document, "name", "location", "content");

                content = content.Replace("province =", "").Trim();
                content = content.Replace("province=", "").Trim();
                content = content.Replace(";city=", "|").Trim();
                string[] localtions = content.Split('|');
                if (localtions.Length > 0)
                {
                    if (localtions.Length > 1)
                    {
                        sProvince = localtions[0];
                        sCity = localtions[1];
                    }
                    else
                    {
                        sProvince = localtions[0];
                        sCity = "";
                    }
                }
                else
                {
                    sProvince = "";
                    sCity = "";
                }

                pThumbnail = getHeadMetaValue(document, "property", "og:image", "content");
                pCurrency = getHeadMetaValue(document, "property", "og:product:currency", "content");

                content = getHeadMetaValue(document, "property", "og:product:nick", "content");
                content.Replace("name=", "").Replace("; url= //", "|").Replace("; url= https://", "|");
                string[] nick_url = content.Split('|');
                if (nick_url.Length > 0)
                {
                    if (nick_url.Length > 1)
                    {
                        //sNickName = nick_url[0].Trim();
                        sShopUrl = "https://" + nick_url[1].Trim();
                    }
                    else
                    {
                        //sNickName = nick_url[0].Trim();
                        sShopUrl = "";
                    }
                }
                else
                {
                    //sNickName = "";
                    sShopUrl = "";
                }

                HtmlNode nodeDescDetail = document.DocumentNode.SelectSingleNode(".//*[@id='mod-detail-description']");
                HtmlNode nodeSlideImage = document.DocumentNode.SelectSingleNode(".//*[@id='dt-tab']/div/ul");
                HtmlNode nodeVideo = document.DocumentNode.SelectSingleNode(".//*[@id='detail-main-video-content']/div/video");
                HtmlNode nodeProPropert = document.DocumentNode.SelectSingleNode(".//*[@id='mod-detail-attributes']/div[1]/table/tbody");
                HtmlNode nodeTopSales = document.DocumentNode.SelectSingleNode(".//*[@id='site_content']/div[2]");
                //HtmlNode nodeContact = document.DocumentNode.SelectSingleNode(".//*[@id='site_content']/div[2]/div[3]/div/div[2]");
                HtmlNode nodeRelated = document.DocumentNode.SelectSingleNode(".//*[@id='mod-detail-word']/div[2]/div");

                //.//*[@id='desc-lazyload-container']

                //轮播图片      //*[@id='dt-tab']
                //销量          //*[@id='mod-detail-otabs']/div
                //.//*[@id='mod-detail-otabs']/div/ul/li[2]/a/span/em
                //产品属性      //*[@id='mod-detail-attributes']
                //详情          //*[@id='de-description-detail']
                //店铺销量排行   //*[@id='site_content']/div[2]/div[2]/div
                //联系方式      //*[@id='site_content']/div[2]/div[3]/div
                //相关产品      //*[@id='mod-detail-word']/div[2]

                if (nodeVideo != null)
                {
                    pVideos = new List<string>();
                    pVideos.Add(nodeVideo.Attributes["src"].Value.ToString());
                }

                



                if (nodeDescDetail != null)
                {
                    //pDescription = ProdFormatTextTool.replaceHtmlSpecChar(nodeDescDetail.InnerText).Trim();
                    HtmlNodeCollection nodesImg = nodeDescDetail.SelectNodes(".//img");  //使用xpath语法进行查询   
                    if (nodesImg != null)  //没有img节点时出错
                    {
                        //处理html字符串中img标签的src属性
                        pDescImages = new List<string>();
                        foreach (HtmlNode imgTag in nodesImg)
                        {
                            if (imgTag.Attributes["src"] != null)
                            {
                                string imgHttpPath = imgTag.Attributes["src"].Value.ToString();
                                imgHttpPath = Regex.Replace(imgHttpPath, ".\\d+x\\d+.", ".");
                                if (!pDescImages.Contains(imgHttpPath))
                                {
                                    pDescImages.Add(imgHttpPath);
                                }
                            }
                        }
                    }
                }

                if (nodeSlideImage != null)
                {
                    HtmlNodeCollection nodesImg = nodeSlideImage.SelectNodes(".//img");
                    if (nodesImg != null)  //没有img节点时出错
                    {
                        pSlideImages = new List<string>();
                        //处理html字符串中img标签的src属性
                        foreach (HtmlNode imgTag in nodesImg)
                        {
                            if (imgTag.Attributes["src"] != null)
                            {
                                string imgHttpPath = imgTag.Attributes["src"].Value;
                                imgHttpPath = Regex.Replace(imgHttpPath, ".\\d+x\\d+.", ".");
                                if (imgHttpPath.Contains("lazyload.png") == false)
                                {
                                    if (!pSlideImages.Contains(imgHttpPath))
                                    {
                                        pSlideImages.Add(imgHttpPath);
                                    }
                                }

                            }
                            if (imgTag.Attributes["data-lazy-src"] != null)
                            {
                                string imgHttpPath = imgTag.Attributes["data-lazy-src"].Value;
                                imgHttpPath = imgHttpPath.Replace(".60x60.jpg", ".jpg");
                                if (imgHttpPath.Contains("lazyload.png") == false)
                                {
                                    if (!pSlideImages.Contains(imgHttpPath))
                                    {
                                        pSlideImages.Add(imgHttpPath);
                                    }
                                }
                            }
                        }
                    }
                }
                if (nodeProPropert != null)
                {
                    pProperty = new List<string>();
                    string proProperty = Regex.Replace(nodeProPropert.InnerHtml, @">\s+<", "><");
                    proProperty = Regex.Replace(proProperty, @"\t", "");
                    proProperty = proProperty.Replace(@"<tr>", "");
                    proProperty = proProperty.Replace(@"</tr>", "");
                    proProperty = proProperty.Replace(@"</td>", "");
                    proProperty = proProperty.Replace(@"<td class=""de-value"">", ":");
                    proProperty = Regex.Replace(proProperty, @"<td class=""de-feature"">", "\r\n");
                    string[] proPropertys = Regex.Replace(proProperty, "\r\n", "|").Split('|');

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

                if (nodeTopSales != null)
                {
                    pRelatedProductUrl = new List<string>();
                    HtmlNodeCollection nodesRelatedProductUrlLinks = nodeTopSales.SelectNodes(".//a");
                    if (nodesRelatedProductUrlLinks != null)  //没有img节点时出错
                    {
                        //处理html字符串中img标签的src属性
                        foreach (HtmlNode aTag in nodesRelatedProductUrlLinks)
                        {
                            if (aTag.Attributes["href"] != null)
                            {
                                string linkHttpPath = aTag.Attributes["href"].Value;
                                if (linkHttpPath.Contains("https://detail.1688.com/offer/"))
                                {
                                    pRelatedProductUrl.Add(linkHttpPath);
                                }
                            }
                        }
                    }
                }

                if (nodeRelated != null)
                {
                    pRelatedProductShortName = new List<string>();
                    HtmlNodeCollection nodesRelatedProductLink = nodeRelated.SelectNodes(".//a");
                    if (nodesRelatedProductLink != null)  //没有img节点时出错
                    {
                        //处理html字符串中img标签的src属性
                        foreach (HtmlNode aTag in nodesRelatedProductLink)
                        {
                            string aTitle = aTag.InnerText;
                            if (aTitle != "")
                            {
                                aTitle = Regex.Replace(aTitle, @"\s+", "");
                                pRelatedProductShortName.Add(aTitle);
                            }
                        }
                    }
                }

                //============================json 解析==detailConfig===detailData============================

                Regex regex_detailConfig = new Regex("var iDetailConfig(.*?)var iDetailData");
                Match match_detailConfig = regex_detailConfig.Match(webSource);
                if (match_detailConfig.Success)
                {
                    string detailConfig = match_detailConfig.Value.ToString().Trim();
                    detailConfig = detailConfig.Replace("var iDetailConfig =", "");
                    detailConfig = detailConfig.Replace(";var iDetailData", "");
                    detailConfig = detailConfig.Trim();

                    JObject joDetailConfig = (JObject)JsonConvert.DeserializeObject(detailConfig);
                    offerid = joDetailConfig["offerid"].ToString();//产品id
                    catid = joDetailConfig["catid"].ToString();
                    dcatid = joDetailConfig["dcatid"].ToString();//"1047340"
                    parentdcatid = joDetailConfig["parentdcatid"].ToString();

                    int acount = 0;
                    int.TryParse(joDetailConfig["beginAmount"].ToString(), out acount);
                    beginAmount = acount;// : "26"

                    currentTime = joDetailConfig["currentTime"].ToString();// :
                    long time = long.Parse(currentTime);
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime date = start.AddMilliseconds(time).ToLocalTime();
                    currentTime = date.ToString("yyyy-MM-dd HH:mm:ss");

                    string refPrice = joDetailConfig["refPrice"].ToString().Trim();// : "0.90"
                    try
                    {
                        pMinPrice = double.Parse(refPrice);
                        pMaxPrice = double.Parse(refPrice);
                    }
                    catch (Exception e)
                    {
                        pMinPrice = 0;
                        pMaxPrice = 0;
                    }



                    //JsonData jd_detailConfig = JsonMapper.ToObject(detailConfig);

                    //offerid = jd_detailConfig["offerid"].ToString();//产品id
                    //catid = jd_detailConfig["catid"].ToString();
                    //dcatid = jd_detailConfig["dcatid"].ToString();//"1047340"
                    //parentdcatid = jd_detailConfig["parentdcatid"].ToString();//"1047331"
                    //                                                          //memberid = jd_detailConfig["memberid"].ToString();//"rcjj888"
                    //                                                          //isSlsjSeller = jd_detailConfig["isSlsjSeller"].ToString();//"true"实力卖家
                    //                                                          //unit = jd_detailConfig["unit"].ToString();//"米"
                    //                                                          //priceUnit = jd_detailConfig["priceUnit"].ToString();//元
                    //int acount = 0;
                    //int.TryParse(jd_detailConfig["beginAmount"].ToString(),out acount);
                    //beginAmount = acount;// : "26"

                    //currentTime = jd_detailConfig["currentTime"].ToString();// :
                    //long time = long.Parse(currentTime);
                    //DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    //DateTime date = start.AddMilliseconds(time).ToLocalTime();
                    //currentTime = date.ToString("yyyy-MM-dd HH:mm:ss");

                    //string refPrice = jd_detailConfig["refPrice"].ToString().Trim();// : "0.90"
                    //try
                    //{
                    //    pMinPrice = double.Parse(refPrice);
                    //    pMaxPrice = double.Parse(refPrice);
                    //}
                    //catch (Exception e)
                    //{
                    //    pMinPrice = 0;
                    //    pMaxPrice = 0;
                    //}

                }

                Regex regex_detailData = new Regex("var iDetailData(.*?)iDetailData");
                Match match_detailData = regex_detailData.Match(webSource);
                if (match_detailData.Success)
                {

                    string detailData = match_detailData.Value.ToString().Trim();
                    detailData = detailData.Replace("var iDetailData =", "");
                    detailData = Regex.Replace(detailData, "iDetailData$", "");
                    detailData = detailData.Trim();
                    detailData = Regex.Replace(detailData, ";$", "");
                    detailData = detailData.Trim();

                    JObject joDetailData = (JObject)JsonConvert.DeserializeObject(detailData);

                    if (joDetailData.Property("sku") != null && joDetailData.Property("sku").ToString() != "")
                    {

                        JObject joSku = (JObject)joDetailData["sku"];

                        string[] pPriceScope = joSku["price"].ToString().Split('-');// price :// "0.10-8.88"
                        try
                        {
                            if (pPriceScope.Length > 1)
                            {
                                pMinPrice = double.Parse(pPriceScope[0].Trim());
                                pMaxPrice = double.Parse(pPriceScope[1].Trim());
                            }
                            else if (pPriceScope[0].Length > 0)
                            {
                                pMinPrice = double.Parse(pPriceScope[0].Trim());
                                pMaxPrice = pMinPrice;
                            }
                        }
                        catch (Exception e)
                        {

                        }

                        //if (JsonDataContainsKey(jd_detailData, "priceRange"))
                        //{
                        //    priceRange = new List<List<double>>();
                        //    JsonData jd_priceRange = jd_detailData["priceRange"];//
                        //    for (int i = 0; i < jd_priceRange.Count; i++)
                        //    {
                        //        List<double> prsub = new List<double>();
                        //        JsonData jd_pr = jd_priceRange[i];
                        //        for (int j = 0; j < jd_pr.Count; j++)
                        //        {
                        //            prsub.Add(double.Parse(jd_pr[j].ToString()));
                        //        }
                        //        priceRange.Add(prsub);
                        //    }
                        //}

                        //if (JsonDataContainsKey(jd_detailData, "priceRangeOriginal"))
                        //{
                        //    priceRangeOriginal = new List<List<double>>();
                        //    JsonData jd_priceRangeOriginal = jd_detailData["priceRangeOriginal"];//

                        //    for (int i = 0; i < jd_priceRangeOriginal.Count; i++)
                        //    {
                        //        List<double> prosub = new List<double>();
                        //        JsonData jd_pro = jd_priceRangeOriginal[i];
                        //        for (int j = 0; j < jd_pro.Count; j++)
                        //        {
                        //            prosub.Add(double.Parse(jd_pro[j].ToString()));
                        //        }
                        //        priceRangeOriginal.Add(prosub);
                        //    }

                        //}
                        //独立变体，一般是两个，例如：颜色和尺码，等等
                        if (joSku.Property("skuProps") != null && joSku.Property("skuProps").ToString() != "")
                        {
                            JArray jaSkuProps = (JArray)joSku["skuProps"];
                            skuProps = new List<SkuPropsItem>();
                            pVariImages = new List<string>();
                            for (int i = 0; i < jaSkuProps.Count; i++)
                            {
                                JObject joSkuProp = (JObject)jaSkuProps[i];
                                SkuPropsItem spi = new SkuPropsItem();
                                //变体类型的名称，名称例如：颜色，尺码
                                spi.prop = ProdFormatTextTool.replaceHtmlSpecChar(joSkuProp["prop"].ToString().Replace("|", "").Replace("#", ""));
                                spi.value = new List<SkuPropsItemValue>();
                                //JsonData jd_values = jd_sku["value"];
                                JArray jaValues = (JArray)joSkuProp["value"];
                                for (int j = 0; j < jaValues.Count; j++)
                                {
                                    JObject jov = (JObject)jaValues[j];
                                    SkuPropsItemValue siv = new SkuPropsItemValue();
                                    if (jov.Property("name") != null && jov.Property("name").ToString() != "")
                                    {
                                        //变体名，例如XL，黄色
                                        siv.name = ProdFormatTextTool.replaceHtmlSpecChar(jov["name"].ToString().Replace("|", "").Replace("#", ""));
                                    }
                                    if (jov.Property("imageUrl") != null && jov.Property("imageUrl").ToString() != "")
                                    {
                                        //变体图片链接
                                        siv.imageUrl = Regex.Replace(jov["imageUrl"].ToString(), ".\\d+x\\d+.", ".");
                                        pVariImages.Add(siv.imageUrl);
                                    }
                                    spi.value.Add(siv);
                                }
                                skuProps.Add(spi);



                            }
                        }
                        //复合变体
                        if (joSku.Property("skuMap") != null && joSku.Property("skuMap").ToString() != "")
                        {
                            skuMap = new List<SkuMapItem>();
                            JObject joSkuProps = (JObject)joSku["skuMap"];
                            foreach (JToken child in joSkuProps.Children())
                            {
                                var property = child as JProperty;
                                string key = property.Name.ToString();
                                //String propertyValue = property.Value.ToString();
                                SkuMapItem smi = new SkuMapItem();
                                JObject joSkup = (JObject)joSkuProps[key];
                                smi.name = ProdFormatTextTool.replaceHtmlSpecChar(key.Replace("|", "").Replace("#", ""));
                                if (joSkup.Property("specId") != null && joSkup.Property("specId").ToString() != "")
                                {
                                    smi.specId = joSkup["specId"].ToString();
                                }
                                if (joSkup.Property("price") != null && joSkup.Property("price").ToString() != "")
                                {
                                    smi.price = double.Parse(joSkup["price"].ToString());
                                }
                                else
                                {
                                    smi.price = pMaxPrice;
                                }
                                if (joSkup.Property("saleCount") != null && joSkup.Property("saleCount").ToString() != "")
                                {
                                    smi.saleCount = Int32.Parse(joSkup["saleCount"].ToString());
                                }
                                if (joSkup.Property("discountPrice") != null && joSkup.Property("discountPrice").ToString() != "")
                                {
                                    smi.discountPrice = double.Parse(joSkup["discountPrice"].ToString());
                                }
                                else
                                {
                                    smi.discountPrice = pMaxPrice;
                                }
                                if (joSkup.Property("canBookCount") != null && joSkup.Property("canBookCount").ToString() != "")
                                {
                                    int canbc = Int32.Parse(joSkup["canBookCount"].ToString());
                                    if (canbc > 1000)
                                    {
                                        canbc = 1000;
                                    }
                                    smi.canBookCount = canbc;
                                }
                                if (joSkup.Property("skuId") != null && joSkup.Property("skuId").ToString() != "")
                                {
                                    smi.skuId = joSkup["skuId"].ToString();
                                }
                                skuMap.Add(smi);
                            }
                        }
                    }
                }
                return this;
            }
            return null;
        }

        private bool JsonDataContainsKey(JsonData data, string key)
        {
            bool result = false;
            if (data == null)
                return result;
            if (!data.IsObject)
            {
                return result;
            }
            IDictionary tdictionary = data as IDictionary;
            if (tdictionary == null)
                return result;
            if (tdictionary.Contains(key))
            {
                result = true;
            }
            return result;
        }
    }
}
