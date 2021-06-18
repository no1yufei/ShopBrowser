using Common.Tools.ProdFormater;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Common.Collector.ProdFormater.ProsItem;

namespace Common.Collector.ProdFormater
{
    public class WxwProdFormat : ProdFormat
    {

        public override ProdFormat WebSourceFormat(string wxwJsonProsStr)
        {
            ProsItem pi = ProsItem.FromJson(wxwJsonProsStr);
            if (pi != null)
            {
                this.pTitle = pi.infor.name;
                this.pMaxPrice = pi.price;
                this.pMinPrice = pi.price;
                this.pDescription = pi.infor.description;
                //string[] pDescs = pi.infor.description.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                //for(int i = 0; i < pDescs.Length; i++)
                //{
                //    this.pDescription = this.pDescription + pDescs[i] + "\r\n";
                //}
                string[] keywords = this.pDescription.Split('#');


                List<string> tempKW = new List<string>();
                foreach (string item in keywords)
                {
                    if (item.Trim().Equals("") == false && tempKW.Contains(item.Trim()) == false&& item.Trim().Length<20)
                    {
                        tempKW.Add(item.Trim());
                    }
                }
                keywords = tempKW.ToArray();
                //按长度开始排序，最短的排前面
                for (int i = 0; i < keywords.Count(); i++) //每个字符串都要参与比较
                {
                    for (int j = 1; j < keywords.Count(); j++) //字符串长度较长的排在前面
                    {
                        if (keywords[j - 1].Length > keywords[j].Length)
                        {
                            string temp = keywords[j - 1];
                            keywords[j - 1] = keywords[j];
                            keywords[j] = temp;
                        }
                    }
                }
                this.wKeywords = keywords;
                this.pSlideImages = new List<string>();// pi.infor.images;
                if (pi.infor.images != null)
                {
                    foreach (string imageUrlItem in pi.infor.images)
                    {
                        string imageUrl = imageUrlItem;
                        if (imageUrl.Contains("/uploads/user/"))
                        {
                            imageUrl = "https://www.wxwerp.com" + imageUrl;
                        }
                        pSlideImages.Add(imageUrl);
                    }
                }
                this.pDescImages = new List<string>();//pi.option_imgs;
                if (pi.option_imgs != null)
                {
                    foreach (string imageUrlItem in pi.option_imgs)
                    {
                        string imageUrl = imageUrlItem;
                        if (imageUrl.Contains("/uploads/user/"))
                        {
                            imageUrl = "https://www.wxwerp.com" + imageUrl;
                        }
                        pDescImages.Add(imageUrl);
                    }
                }
                this.pVariImages = new List<string>();
                if (pi.infor.tier_variation != null)
                {
                    this.skuProps = new List<SkuPropsItem>();
                    foreach (Tier_variationItem tItem in pi.infor.tier_variation)
                    {
                        SkuPropsItem spi = new SkuPropsItem();
                        string name = tItem.name;
                        spi.prop = name;
                        spi.value = new List<SkuPropsItemValue>();
                        List<string> options = tItem.options;
                        List<string> images_url = tItem.images_url;
                        for (int i = 0; i < options.Count; i++)
                        {
                            SkuPropsItemValue spiv = new SkuPropsItemValue();
                            string optionName = options[i];
                            spiv.name = optionName;
                            if (tItem.images_url != null)
                            {
                                if (options.Count == images_url.Count)
                                {
                                    string imageUrl = images_url[i];
                                    spiv.imageUrl = imageUrl;
                                    if (imageUrl.Contains("/uploads/user/"))
                                    {
                                        imageUrl = "https://www.wxwerp.com" + imageUrl;
                                    }
                                    pVariImages.Add(imageUrl);
                                }
                            }

                            spi.value.Add(spiv);
                        }
                        this.skuProps.Add(spi);
                    }
                }
                
                
                if (pi.infor.variations != null)
                {
                    this.skuMap = new List<SkuMapItem>();
                    foreach (VariationsItem vItem in pi.infor.variations)
                    {
                        SkuMapItem smi = new SkuMapItem();
                        smi.name = vItem.name.Replace(",", ">");
                        smi.price = vItem.price;
                        smi.discountPrice = vItem.price;
                        smi.saleCount = 0;
                        smi.specId = vItem.variation_sku;
                        smi.canBookCount = vItem.stock>500 ? 500 : vItem.stock;
                        smi.skuId = vItem.variation_sku;
                        this.skuMap.Add(smi);
                    }
                }
                
                if (pi.src.Contains("detail.1688.com/offer"))//550626633694.html
                {
                    this.wAbbrName = "ALI";
                    this.wLink = pi.src;
                    this.offerid = pi.src.Replace("https://detail.1688.com/offer/", "").Replace(".html", "");//573918517023
                }
                //https://item.taobao.com/item.htm?spm=2013.1.0.0.133711d9ERHudl&scm=1007.11855.31966.100200300000007&id=521133979730&pvid=84bf9a22-85c4-42f3-8040-a5344db52958
                if (pi.src.Contains("item.taobao.com/item.htm"))
                {
                    this.wAbbrName = "TAB";
                    this.wLink = pi.src;
                    this.offerid = Regex.Replace(pi.src, "^(.*?)id=", "");
                    this.offerid = Regex.Replace(this.offerid, "&(.*?)$", "");
                }
                //https://detail.tmall.com/item.htm?spm=a230r.1.14.24.3ecc359fJ4ZHQj&id=590800777731&ns=1&abbucket=5
                if (pi.src.Contains("detail.tmall.com/item.htm"))
                {
                    this.wAbbrName = "TIM";
                    this.wLink = pi.src;
                    this.offerid = Regex.Replace(pi.src, "^(.*?)id=", "");
                    this.offerid = Regex.Replace(this.offerid, "&(.*?)$", "");
                }
                return this;
            }
            return null;
        }
    }
    public class ProsItem
    {
        public static ProsItem FromJson(string json)
        {
            ProsItem pi = null;
            try
            {
                pi = JsonConvert.DeserializeObject<ProsItem>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pi;
        }


        /// <summary>
        /// 
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Infor infor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> option_imgs { get; set; }

        public class Infor
        {
            /// <summary>
            /// Lb珍珠鎖骨鏈女choker頸帶簡約短款項鏈女脖子飾品項圈頸帶
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<VariationsItem> variations { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Tier_variationItem> tier_variation { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long stock { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> images { get; set; }
            /// <summary>
            /// 材質 : 合金 
            /// </summary>
            public string description { get; set; }
        }
        public class Tier_variationItem
        {
            /// <summary>
            /// 顏色
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> options { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> images_url { get; set; }
        }
        public class VariationsItem
        {
            ///// <summary>
            ///// 
            ///// </summary>
            //public string status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double original_price { get; set; }
            ///// <summary>
            ///// 
            ///// </summary>
            //public long update_time { get; set; }
            ///// <summary>
            ///// 
            ///// </summary>
            //public long create_time { get; set; }
            ///// <summary>
            ///// 
            ///// </summary>
            //public long discount_id { get; set; }
            /// <summary>
            /// A 金色
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double price { get; set; }
            /// <summary>
            /// 5vsw-1-a金色
            /// </summary>
            public string variation_sku { get; set; }
            ///// <summary>
            ///// 
            ///// </summary>
            //public long variation_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int stock { get; set; }
        }

    }

    
}
