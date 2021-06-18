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
    public class PddProdFormat : ProdFormat
    {
        public override ProdFormat WebSourceFormat(string webSrc)
        {
            string webSource = webSrc.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            HtmlAgilityPack.HtmlDocument document = getHtmlDocument(webSource);

            if (null != document)
            {
                wAbbrName = "PDD";
                Regex regexProductInfo = new Regex("window.rawData=(.*?)</script>");
                Match matchProductInfo = regexProductInfo.Match(webSource);
                if (matchProductInfo.Success)
                {
                    webSource = matchProductInfo.Value.ToString().Trim();
                    webSource = webSource.Replace("window.rawData=", "").Trim();
                    webSource = webSource.Replace("</script>", "").Trim();
                    webSource = Regex.Replace(webSource, ";$", "");
                    JObject jo = (JObject)JsonConvert.DeserializeObject(webSource);
                    string pddProductInfo = jo["store"]["initDataObj"]["goods"].ToString();
                    PddGoods pg = JsonConvert.DeserializeObject<PddGoods>(pddProductInfo);

                    offerid = pg.goodsID;
                    catid = pg.catID3;
                    dcatid = pg.catID2;
                    parentdcatid = pg.catID1;


                    beginAmount = 1;
                    pMaxPrice = pg.maxGroupPrice;
                    pMinPrice = pg.minGroupPrice;

                    long time = pg.serverTime*1000;
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime date = start.AddMilliseconds(time).ToLocalTime();
                    currentTime = date.ToString("yyyy-MM-dd HH:mm:ss");

                    wLink = "https://mobile.yangkeduo.com/goods.html?goods_id=" + offerid;
                    pTitle = pg.goodsName;
                    pDescription = pg.goodsDesc;
                    pOneSend = 1;
                    pCurrency = "CNY";
                    pThumbnail = pg.hdThumbUrl.Contains("http")? pg.hdThumbUrl: "https:" + pg.hdThumbUrl;

                    //sNickName = jo["store"]["initDataObj"]["mall"]["mallName"].ToString();
                    sShopUrl = "https://mobile.yangkeduo.com/mall_page.html?mall_id=" + pg.mallID;
                    //topGallery   轮播图
                    if (pg.topGallery != null)
                    {
                        pSlideImages = new List<string>();
                        foreach (TopGalleryItem item in pg.topGallery)
                        {
                            string slideImage = Regex.Replace(item.url, "\\?imageMogr(.*?)$", "");
                            slideImage = slideImage.Contains("http")? slideImage: "https:" + slideImage;
                            pSlideImages.Add(slideImage);
                        }
                    }
                    //detailGallery  详情图
                    if (pg.detailGallery != null)
                    {
                        pDescImages = new List<string>();
                        foreach (DetailGalleryItem dgitem in pg.detailGallery)
                        {
                            string detailImage =  Regex.Replace(dgitem.url, "\\?imageMogr(.*?)$", "");
                            detailImage = detailImage.Contains("http") ? detailImage : "https:" + detailImage;
                            pDescImages.Add(detailImage);
                        }
                    }
                    //specs  变体 skuMap//skuProps  
                    if (pg.skus != null)
                    {
                        skuMap = new List<SkuMapItem>();//复合变体
                        pVariImages = new List<string>();

                        skuProps = new List<SkuPropsItem>();//单一变体

                        SkuPropsItem spi1 = null;
                        SkuPropsItem spi2 = null;
                        List<string> list_vari1name = new List<string>();
                        List<string> list_vari2name = new List<string>();
                        for (int i = 0; i < pg.skus.Count; i++)
                        {
                            SkusItem skuitem = pg.skus[i];

                            SkuMapItem smi = new SkuMapItem();
                            
                            smi.specId = skuitem.skuID.ToString();
                            smi.price = skuitem.oldGroupPrice==0? skuitem.groupPrice: skuitem.oldGroupPrice;
                            smi.saleCount = skuitem.quantity;
                            smi.discountPrice = skuitem.groupPrice;
                            smi.canBookCount = skuitem.quantity;
                            smi.skuId = skuitem.skuID.ToString();
                            //thumbUrl : "//t00img.yangkeduo.com/goods/images/2019-03-05/81bb0712-3f09-4ea7-9521-b6a4c9b2fdd0.jpg"
                            string vImg = skuitem.thumbUrl;
                            vImg = vImg.Contains("http") ? vImg : "https:" + vImg;
                            if (pVariImages.Contains(vImg) == false)
                            {
                                pVariImages.Add(vImg);
                            }
                            string vari1name = "";
                            string vari2name = "";
                            
                            if (skuitem.specs != null && skuitem.specs.Count > 0)
                            {
                                SpecsItem siitem = skuitem.specs[0];
                                if (spi1 == null)
                                {
                                    spi1 = new SkuPropsItem();
                                    spi1.prop = siitem.spec_key;//变体名
                                    spi1.value = new List<SkuPropsItemValue>();
                                    skuProps.Add(spi1);
                                }
                                vari1name = siitem.spec_value;
                                if(list_vari1name.Contains(vari1name) == false)
                                {
                                    list_vari1name.Add(vari1name);
                                    SkuPropsItemValue siv = new SkuPropsItemValue();
                                    siv.name = vari1name;
                                    siv.imageUrl = vImg;
                                    spi1.value.Add(siv);
                                }
                                
                                
                            }

                            if (skuitem.specs != null && skuitem.specs.Count > 1)
                            { 
                                SpecsItem siitem = skuitem.specs[1];
                                if (spi2 == null)
                                {
                                    spi2 = new SkuPropsItem();
                                    spi2.prop = siitem.spec_key;
                                    spi2.value = new List<SkuPropsItemValue>();
                                    skuProps.Add(spi2);
                                }
                                vari2name = siitem.spec_value;
                                if (list_vari2name.Contains(vari2name) == false)
                                {
                                    list_vari2name.Add(vari2name);
                                    SkuPropsItemValue siv = new SkuPropsItemValue();
                                    siv.name = vari2name;
                                    siv.imageUrl = "";
                                    spi2.value.Add(siv);
                                }
                            }
                            if(vari1name.Equals("")==false&& vari2name.Equals("") == false)
                            {
                                smi.name = vari1name + ">" +vari2name;
                            }
                            else if (vari1name.Equals("") == false && vari2name.Equals("") == true)
                            {
                                smi.name = vari1name;
                            }
                            skuMap.Add(smi);
                        }
                    }
                    //goodsProperty 产品属性pProperty
                    if (pg.goodsProperty != null)
                    {
                        pProperty = new List<string>();
                        foreach (GoodsPropertyItem gpiitem in pg.goodsProperty)
                        {
                            string proName = gpiitem.key;
                            string proDetail = proName + ": ";
                            foreach (string item in gpiitem.values)
                            {
                                proDetail = proDetail + item + ",";
                            }
                            pProperty.Add(proDetail);
                        }
                    }
                          
                    

                    ////skus

                    ////string skus = jo["store"]["initDataObj"]["goods"]["skus"].ToString();
                    ////string goodsProperty = jo["store"]["initDataObj"]["goods"]["goodsProperty"].ToString();

                    ////pi4ee.imgSlidColumnName = slideImage;
                    ////详情图

                    ////变体图
                    //string variImage = "";
                    //List<string> listVariImage = new List<string>();
                    //string vari1Name = "";
                    //Dictionary<string, string> dictVari1Detail = new Dictionary<string, string>();
                    //string vari1Detail = "";

                    //string vari2Name = "";
                    //Dictionary<string, string> dictVari2Detail = new Dictionary<string, string>();
                    //string vari2Detail = "";


                    //string vari3Detail = "";


                    //foreach (SkusItem skuitem in pg.skus)
                    //{
                    //    string skuprice = skuitem.normalPrice;
                    //    string variMerge = "";
                    //    if (skuitem.specs != null && skuitem.specs.Count > 0)
                    //    {
                    //        SpecsItem siitem = skuitem.specs[0];
                    //        vari1Name = siitem.spec_key;
                    //        if (siitem.spec_value.Length < 20 && dictVari1Detail.ContainsKey(siitem.spec_value) == false)
                    //        {
                    //            dictVari1Detail.Add(siitem.spec_value, skuprice);
                    //        }

                    //        //vari1Detail = vari1Detail + siitem.spec_value + "|" + skuprice + "\r\n";
                    //        variMerge = skuitem.specs[0].spec_value;

                    //    }
                    //    if (skuitem.specs != null && skuitem.specs.Count > 1)
                    //    {
                    //        SpecsItem siitem = skuitem.specs[1];
                    //        vari2Name = siitem.spec_key;
                    //        if (siitem.spec_value.Length < 20 && dictVari2Detail.ContainsKey(siitem.spec_value) == false)
                    //        {
                    //            dictVari2Detail.Add(siitem.spec_value, skuprice);
                    //        }


                    //        //vari2Detail = vari2Detail + siitem.spec_value + "|" + skuprice + "\r\n";


                    //        //vari3Detail = vari3Detail + skuitem.specs[0].spec_value +">"+ skuitem.specs[1].spec_value + "|" + skuprice + "\r\n";

                    //        variMerge = skuitem.specs[0].spec_value + ">" + skuitem.specs[1].spec_value;
                    //    }

                    //    vari3Detail = vari3Detail + variMerge + "|" + skuprice + "\r\n";

                    //    //foreach (SpecsItem siitem in skuitem.specs)
                    //    //{
                    //    //    Console.WriteLine(siitem.spec_key + "|" + siitem.spec_value);
                    //    //}
                    //    //Console.WriteLine(skuitem.thumbUrl);
                    //    //
                    //    string imageTemp = "https:" + Regex.Replace(skuitem.thumbUrl, "\\?imageMogr(.*?)$", "");
                    //    if (listVariImage.Contains(imageTemp) == false)
                    //    {
                    //        listVariImage.Add(imageTemp);
                    //    }


                    //    Console.WriteLine();
                    //}
                    //int var1index = 0;
                    //foreach (var item in dictVari1Detail)
                    //{
                    //    vari1Detail = vari1Detail + item.Key + "|" + item.Value + "\r\n";
                    //    var1index++;
                    //    if (var1index == 50)
                    //    {
                    //        break;
                    //    }
                    //}
                    //int var2index = 0;
                    //foreach (var item in dictVari2Detail)
                    //{
                    //    vari2Detail = vari2Detail + item.Key + "|" + item.Value + "\r\n";
                    //    var2index++;
                    //    if (var2index == 50)
                    //    {
                    //        break;
                    //    }
                    //}
                    //foreach (string item in listVariImage)
                    //{
                    //    variImage = variImage + item + "|";
                    //}
                }
                return this;
            }
            return null;
        }

        public class PddGoods
        {
            /// <summary>
            /// 
            /// </summary>
            public string catID { get; set; }
            public string catID1 { get; set; }
            public string catID2 { get; set; }
            public string catID3 { get; set; }
            public string catID4 { get; set; }
            public string mallID { get; set; }
            public string hdThumbUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string goodsID { get; set; }
            /// <summary>
            /// 新2019春款妈妈鞋老北京软底软皮单鞋防滑舒适孕妇鞋中老年女皮鞋
            /// </summary>
            public string goodsName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<TopGalleryItem> topGallery { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> viewImageData { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DetailGalleryItem> detailGallery { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<SkusItem> skus { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<GoodsPropertyItem> goodsProperty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string maxNormalPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string minNormalPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double maxGroupPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double minGroupPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string maxOnSaleGroupPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string minOnSaleGroupPrice { get; set; }
            public long serverTime { get; set; }

            public string goodsDesc { get; set; }


        }
        public class TopGalleryItem
        {
            public string url;
            public long id;
        }
       
        public class DetailGalleryItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long width { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long height { get; set; }
        }

        public class SkusItem
        {
            /// <summary>
            /// 
            /// </summary>
            public long skuID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int quantity { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long initQuantity { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long isOnSale { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long soldQuantity { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<SpecsItem> specs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string thumbUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long limitQuantity { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double normalPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double groupPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double oldGroupPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double skuExpansionPrice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double unselectGroupPrice { get; set; }
            /// <summary>
            /// 再售1580件后恢复¥39.9
            /// </summary>
            public string yellowLabel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long defaultQuantity { get; set; }
        }
        public class OrderSpecsItem
        {
            /// <summary>
            /// 颜色分类
            /// </summary>
            public string key { get; set; }
            /// <summary>
            /// 黑色加绒款1139
            /// </summary>
            public string value { get; set; }
        }
        public class SpecsItem
        {
            /// <summary>
            /// 颜色分类
            /// </summary>
            public string spec_key { get; set; }
            /// <summary>
            /// 黑色单鞋1139
            /// </summary>
            public string spec_value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long spec_key_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long spec_value_id { get; set; }
        }
        public class GoodsPropertyItem
        {
            /// <summary>
            /// 鞋面材质
            /// </summary>
            public string key { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> values { get; set; }
        }
    }
}
