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
    public class TianmaoProdFormat : ProdFormat
    {
        public override ProdFormat WebSourceFormat(string webSrc)
        {
            string webSource = webSrc.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(webSource);
            if (null != document)
            {
                wAbbrName = "TIM";
                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                pOneSend = 1;
                pCurrency = "CNY";

                //<input id="J_ShopSearchUrl" type="hidden" value="//tjkfs.tmall.com" />
                HtmlNode nodeShopSearchUrl = document.DocumentNode.SelectSingleNode("//*[@id='J_ShopSearchUrl']");
                if (null != nodeShopSearchUrl)
                {
                    sShopUrl = "https:"+nodeShopSearchUrl.Attributes["value"].Value.ToString();
                }
                HtmlNode nodeProperty = document.DocumentNode.SelectSingleNode("//*[@id='J_Attrs']");
                if (null != nodeProperty)
                {
                    string attributes = nodeProperty.InnerHtml;
                    attributes = Regex.Replace(attributes, "<th(.*?)>", "|");
                    attributes = attributes.Replace("</th><td>", ":").Replace("&nbsp;", "");
                    attributes = Regex.Replace(attributes, "<(.*?)>", "");
                    attributes = Regex.Replace(attributes, "\\s+", "");

                    string[] proPropertys = attributes.Split('|');
                    pProperty = new List<string>();
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
                nodeProperty = document.DocumentNode.SelectSingleNode("//*[@id='J_AttrUL']");
                if (null != nodeProperty)
                {
                    string attributes = nodeProperty.InnerHtml;
                    attributes = attributes.Replace("&nbsp;", "");
                    attributes = attributes.Replace("</li>", "|");
                    attributes = Regex.Replace(attributes, "<(.*?)>", "");
                    attributes = Regex.Replace(attributes, "\\s+", "");

                    string[] proPropertys = attributes.Split('|');
                    pProperty = new List<string>();
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
                //HtmlNode nodeDesc = document.DocumentNode.SelectSingleNode("//*[@id='description']");
                //if (null != nodeDesc)
                //{
                //    pDescription = pDescription + "\r\n" +ProdFormatTextTool.replaceHtmlSpecChar(nodeDesc.InnerText);
                //    HtmlNodeCollection nodesImg = nodeDesc.SelectNodes(".//img");
                //    if (nodesImg != null)  //没有img节点时出错
                //    {
                //        pDescImages = new List<string>();
                //        //处理html字符串中img标签的src属性
                //        foreach (HtmlNode hnImg in nodesImg)
                //        {
                //            if (hnImg.Attributes["src"] != null)
                //            {
                //                string link = hnImg.Attributes["src"].Value.ToString();
                //                if (!pDescImages.Contains(link))
                //                {
                //                    pDescImages.Add(link);
                //                }
                //            }
                //        }
                //    }
                //}
                HtmlNode nodeSku = document.DocumentNode.SelectSingleNode("//*[@id='J_DetailMeta']");
                Dictionary<string, List<string[]>> dictVariNameInfo = new Dictionary<string, List<string[]>>();
                if (null != nodeSku)
                {
                    HtmlNodeCollection nodesUl = nodeSku.SelectNodes(".//ul");
                    pVariImages = new List<string>();
                    if (nodesUl != null)  //没有img节点时出错
                    {
                        foreach (HtmlNode hnUl in nodesUl)
                        {
                            if (hnUl != null)
                            {
                                
                                if(hnUl.Attributes["class"].Value.ToString().Contains("tm-clear")
                                    && hnUl.Attributes["class"].Value.ToString().Contains("J_TSaleProp"))
                                {
                                    string variName = hnUl.Attributes["data-property"].Value.ToString().Trim();//sku类型名称
                                    List<string[]> listVariInfo = new List<string[]>();
                                    for (int j = 1; j < 99; j++)
                                    {
                                        HtmlNode hnLi = hnUl.SelectSingleNode(".//li[" + j + "]");
                                        HtmlNode hnA = hnUl.SelectSingleNode(".//li[" + j + "]/a");
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
                                                if(pThumbnail==null|| pThumbnail.Equals(""))
                                                {
                                                    pThumbnail = variInfo[2];
                                                }
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
                    }
                }
                if (dictVariNameInfo.Count > 0)
                {
                    skuProps = new List<SkuPropsItem>();
                    foreach (var vitem in dictVariNameInfo)
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
                Regex regexTShopSetup = new Regex(@"TShop.Setup\((.*?)\);");
                Match matchTShopSetup = regexTShopSetup.Match(webSource);
                if (matchTShopSetup.Success)
                {
                    string tshopSetup = matchTShopSetup.Value.ToString().Trim();
                    tshopSetup = tshopSetup.Replace("TShop.Setup(", "");
                    tshopSetup = Regex.Replace(tshopSetup, "\\);$", "");
                    tshopSetup = tshopSetup.Trim();
                    JObject joTshopSetup = (JObject)JsonConvert.DeserializeObject(tshopSetup);

                    pTitle = joTshopSetup["itemDO"]["title"].ToString();
                    //refPrice = joTshopSetup["itemDO"]["reservePrice"].ToString();
                    //memberid = joTshopSetup["itemDO"]["userId"].ToString();
                    sProvince = joTshopSetup["itemDO"]["prov"].ToString();
                    //brand = joTshopSetup["itemDO"]["brand"].ToString();
                    //canBookCount = joTshopSetup["itemDO"]["quantity"].ToString();
                    //weight = joTshopSetup["itemDO"]["weight"].ToString();
                    offerid = joTshopSetup["itemDO"]["itemId"].ToString();
                    wLink = "https://detail.tmall.com/item.htm?id=" + offerid;
                    catid = joTshopSetup["itemDO"]["categoryId"].ToString();

                    JArray jaSkuList = (JArray)joTshopSetup["valItemInfo"]["skuList"];
                    JObject joSkuMap = (JObject)joTshopSetup["valItemInfo"]["skuMap"];

                    if (jaSkuList.Count > 0)
                    {
                        skuMap = new List<SkuMapItem>();
                        foreach (JObject sl in jaSkuList)
                        {
                            SkuMapItem smi = new SkuMapItem();


                            string pvs = sl["pvs"].ToString();//20509:28313;1627207:1183159656
                            string skuId = sl["skuId"].ToString();//3149937284934
                            string variNameTemp = pvs.Replace(";", ">");
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

                            string skuPrice = "0";
                            string skuStock = "0";
                            foreach (KeyValuePair<string, JToken> item in joSkuMap)
                            {
                                string key = item.Key;
                                //JArray value = (JArray)item.Value;
                                JObject joSkuMapSub = (JObject)item.Value;
                                if (joSkuMapSub["skuId"].ToString().Equals(skuId))
                                {
                                    skuPrice = joSkuMapSub["price"].ToString();
                                    skuStock = joSkuMapSub["stock"].ToString();
                                    break;
                                }
                            }
                            smi.name = variNameTemp;
                            smi.specId = skuId;
                            smi.price = double.Parse(skuPrice);
                            if (pMinPrice == 0)
                            {
                                pMinPrice = smi.price;
                            }
                            else if (pMinPrice > smi.price)
                            {
                                pMinPrice = smi.price;
                            }
                            if (pMaxPrice == 0)
                            {
                                pMaxPrice = smi.price;
                            }
                            else if (pMaxPrice < smi.price)
                            {
                                pMaxPrice = smi.price;
                            }
                            smi.saleCount = 0;
                            smi.discountPrice = double.Parse(skuPrice);
                            smi.canBookCount = Int32.Parse(skuStock);
                            smi.skuId = skuId;
                            skuMap.Add(smi);
                        }
                    }
                    //解析产品图片和详情
                    pSlideImages = new List<string>();
                    HtmlNode nodeSlideImage = document.DocumentNode.SelectSingleNode("//*[@id='J_UlThumb']");
                    if (null != nodeSlideImage)
                    {
                        HtmlNodeCollection nodesLi = nodeSlideImage.SelectNodes(".//li");                  
                        if (nodesLi != null)  //没有img节点时出错
                        {
                            foreach (HtmlNode hnLi in nodesLi)
                            {
                                if (hnLi != null)
                                {
                                    HtmlNode hn_image = hnLi.SelectSingleNode(".//a/img");
                                    if (hn_image != null)
                                    {
                                        string slideImg = hn_image.Attributes["src"].Value.ToString();
                                        slideImg = Regex.Replace(slideImg, ".jpg_(.*?).jpg", ".jpg");
                                        slideImg = "https:" + slideImg;
                                        if (pSlideImages.Contains(slideImg) == false)
                                        {
                                            pSlideImages.Add(slideImg);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (joTshopSetup.ContainsKey("propertyPics"))
                    {

                        JArray jaSlidePics = (JArray)joTshopSetup["propertyPics"]["default"];
                        if (jaSlidePics.Count > 0)
                        {
                            
                            foreach (var item in jaSlidePics)
                            {
                                string slideImg = "https:" + item.ToString();
                                if (pSlideImages.Contains(slideImg) == false)
                                {
                                    pSlideImages.Add(slideImg);
                                }

                            }
                        }

                        JObject joPropertyPics = (JObject)joTshopSetup["propertyPics"];
                        if (joPropertyPics != null)
                        {
                            pVariImages = new List<string>();
                            foreach (KeyValuePair<string, JToken> item in joPropertyPics)
                            {
                                string key = item.Key;
                                JArray jaVariImages = (JArray)item.Value;
                                if (key.Contains("default") == false)
                                {
                                    foreach (var vimg in jaVariImages)
                                    {
                                        string slideImg = "https:" + vimg.ToString();
                                        if (pVariImages.Contains(slideImg) == false)
                                        {
                                            pVariImages.Add(slideImg);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                }
                return this;
            }
            return null;
        }
    }
}
