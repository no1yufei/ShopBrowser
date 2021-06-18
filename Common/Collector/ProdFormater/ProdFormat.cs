using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Tools.ProdFormater
{
    public abstract class ProdFormat
    {
        public bool NetError = false;
        /// <summary>
        /// 产品前缀代号简称
        /// </summary>
        public string wAbbrName { get; set; }
        /// <summary>
        /// 采集网页链接
        /// </summary>
        public string wLink { get; set; }
        List<string> wkeywords = new List<string>();
        /// <summary>
        /// 采集的网页里的关键词，一般在网页的头部
        /// </summary>
        public string[] wKeywords
        {
            get { return wkeywords.ToArray(); }
            set
            {
                foreach (string str in value)
                {
                    wkeywords.Add(wordFilter(str, titleFilter));
                }

            }
        }
/// <summary>
/// 是否一键代发，我们自己的产品帮人代发。1代表可以。0代表未知不可以，1以上是最低发的件数
/// </summary>
public int pOneKey { get; set; }
        /// <summary>
        /// 供应商是否一件代发，单件代发，有货源信息决定。1代表可以。0代表未知不可以，1以上是最低发的件数
        /// </summary>
        public int pOneSend { get; set; }

        string title = "";
        /// <summary>
        /// 产品详情正文标题
        /// </summary>
        /// 
        public string pTitle
        {
            get { return title; }
            set
            {
                title = wordFilter(value, titleFilter);
            }
        }

        string descripttion ="";
        /// <summary>
        /// 产品详情文字
        /// </summary>
        public string pDescription { get { return descripttion; }
            set
            {
                descripttion = removeStringKeyValue(filterString,value);
                descripttion = wordFilter(descripttion, filterString);
                descripttion = wordFilter(descripttion, titleFilter);
            }
        }
        /// <summary>
        /// 产品在该平台的唯一编号，可组成产品唯一的访问地址
        /// </summary>
        public string offerid { get; set; }
        /// <summary>
        /// 三级类目编号
        /// </summary>
        public string catid { get; set; }
        /// <summary>
        /// 二级类目编号
        /// </summary>
        public string dcatid { get; set; }
        /// <summary>
        /// 一级类目编号
        /// </summary>
        public string parentdcatid { get; set; }
        /// <summary>
        /// 起批数量
        /// </summary>
        public int beginAmount { get; set; }
        /// <summary>
        /// 访问此产品数据时的时间,要统一格式化成yyyy-MM-dd HH:mm:ss，一般是unixtime 13位字符串
        /// </summary>
        public string currentTime { get; set; }
        /// <summary>
        /// 最大价格。
        /// </summary>
        public double pMaxPrice { get; set; }
        /// <summary>
        /// 最小价格。
        /// </summary>
        public double pMinPrice { get; set; }
        /// <summary>
        /// 产品价格的货币单位
        /// </summary>
        public string pCurrency { get; set; }

        /// <summary>
        /// 产品缩略图，如果没有，就用产品首图替代
        /// </summary>
        public string pThumbnail { get; set; }
        /// <summary>
        /// 供应商店铺地址
        /// </summary>
        public string sShopUrl { get; set; }
        /// <summary>
        /// 发货地的省份
        /// </summary>
        public string sProvince { get; set; }
        /// <summary>
        /// 发货地城市
        /// </summary>
        public string sCity { get; set; }
        /// <summary>
        /// 普通批发区间和价格
        /// </summary>
        public List<List<double>> priceRange { get; set; }
        /// <summary>
        /// 会员批发区间和价格
        /// </summary>
        public List<List<double>> priceRangeOriginal { get; set; }
        /// <summary>
        /// 单一变体的描述，只有属性名，具体名称和变体图片链接
        /// </summary>
        public List<SkuPropsItem> skuProps { get; set; }
        /// <summary>
        /// 复合变体的描述，包含复合变体名称，唯一标识，价格，销量，打折价，备货数量，编号
        /// </summary>
        public List<SkuMapItem> skuMap = new List<SkuMapItem>();
        
        /// <summary>
        /// 产品属性上货时一般会作为详情
        /// </summary>
        public List<string> pProperty { get; set; }
        /// <summary>
        /// 产品视频地址
        /// </summary>
        public List<string> pVideos { get; set; }
        /// <summary>
        /// 产品轮播图地址
        /// </summary>
        public List<string> pSlideImages { get; set; }
        /// <summary>
        /// 产品变体图片地址，这个图片在变体图包含。如果轮播图不足，可以用此补充
        /// </summary>
        public List<string> pVariImages { get; set; }
        /// <summary>
        /// 产品详情中的图片地址
        /// </summary>
        public List<string> pDescImages { get; set; }
        /// <summary>
        /// 相关产品的产品链接
        /// </summary>
        public List<string> pRelatedProductUrl { get; set; }
        /// <summary>
        /// 相关产品的简称
        /// </summary>
        public List<string> pRelatedProductShortName { get; set; }
        

        public abstract ProdFormat WebSourceFormat(string webSrc);

        public string ToJson()
        {
            string json = "";
            try
            {
                json = JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return json;
        }

        public ProdFormat FromJson(string json)
        {
            ProdFormat prodFormat = null;
            try
            {
                prodFormat = JsonConvert.DeserializeObject<ProdFormat>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return prodFormat;
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

        public string[] filterString = { "货源类别","品牌","贴牌","代工","改版","条形码","建议零售价", "价格段", "价格", "包装价格多少", "大陆联邦特效价要求", "定做的价格", "价格是否含包装", "全国统一零售价", "淘宝限价",
        "营销价格", "加工定制", "加工方式", "加工工艺", "深圳加工生产厂家", "是否加工定制", "加工地址", "加工级别", "加工制作", "可加工", "可加工范围", "年剩余加工能力",
        "外贸订单加工", "印后加工", "产地", "原产地", "产地（国内）", "机芯产地", "厂家(产地)", "生产地址", "主面料产地是否进口", "品牌产地", "生产地", "是否支持一件代发",
        "OEM 一件代发", "可否代发", "是否一件代发", "是否支持代发", "一件代发", "支持一件代发", "是否外贸", "是否专供外贸", "外贸类型", "外贸", "外贸爆款平台",
        "外贸礼品定制", "外贸品质", "是否专利货源", "是否有专利", "专利号", "专利类型", "专利", "专利及著作权", "专利及著作权申请时间", "专利号或版权登记证号", "外观专利号",
        "产品专利号", "设计专利", "专利产品", "加印LOGO", "LOGO印刷", "丝印国外LOGO", "OEM", "OEM定制", "是否支持OEM", "可OEM", "可否OEM", "是否进口",
        "进口地", "进口", "进口芯片", "是否支持混批", "混批起批量", "批号", "深圳华强北批发实体店", "支持混批", "现货批发", "销售批发",  "货源",
        "是否跨境电商货源", "货源类型", "发票", "售后服务", "物流服务", "特色服务", "服务", "免费服务", "增值服务", "FBA服务", "服务类型", "服务内容", "服务项目",
        "服务信息", "立方速贴心服务", "商家服务", "同城服务", "上市时间", "最快出货时间", "产品上市时间", "最晚发货时间", "交货时间", "打样时间", "货号", "3C证书编号",
        "产品编号", "贸易属性", "贸易类型", "发货物流公司", "快递物流", "是否支持代理加盟", "加盟分销门槛", "质保期", "质保", "保质期", "货期", "交货期", "质保年限",
        "商品类型", "开模定制", "是否库存", "库存类型", "是否支持分销", "是否出口", "销售序列号", "销售单位", "销售方式", "热销爆款平台", "主要销售渠道", "厂家直销",
        "工厂直销", "经销性质", "热销平台", "销售性质", "一件代销", "营销方式", "是否出口专用","主要销售地区","主要下游平台","适用送礼场合","有可授权的自有","适合年龄"
        };

        public string[] titleFilter = { "新品","现货","批发", "厂家","直销","新款",  "一件代发",  "批发零售", "批发代理", "广州", "定做",
                                        "大量现货",  "中性", "新款", "国产", "包邮","亚马逊","wish","amazon","ebay","速卖通","lazada" ,
                                        "跨境", "外贸" , "抖音爆款","工厂","广场舞","六一","中国风","中国","民族风","新疆","专供","2015","2016","2017","2018","2019"
         };

        private string wordFilter(string source,string[] words)
        {
            if(null == source || "" == source)
            {
                return "";
            }
            foreach(string word in words)
            {
                source = source.ToLower().Replace(word.ToLower(), "");
            }
            return source;
        }

        protected string getHeadMetaValue(HtmlDocument doucment, string attriKey,string keyValue,string attriValue="content")
        {
            string ret = "";
            HtmlNode node = doucment.DocumentNode.SelectSingleNode("//html/head/meta[@"+ attriKey + "='"+ keyValue + "']");
            if(node != null)
            {
                try
                {
                    ret = node.GetAttributeValue(attriValue, "");

                }
                catch(Exception xe)
                {
                    Console.WriteLine("Meta节点：" + attriKey + "=" + keyValue+"未能找到属性"+ attriValue);
                }
            }
            else
            {
                Console.WriteLine("未能寻找Meta节点：" + attriKey + "=" + keyValue);
            }
            return ret;
        }
        protected string getHeadLinkValue(HtmlDocument doucment, string attriKey, string keyValue, string attriValue)
        {
            string ret = "";
            HtmlNode node = doucment.DocumentNode.SelectSingleNode("//html/head/link[@" + attriKey + "='" + keyValue + "']");
            if (node != null)
            {
                try
                {
                    ret = node.GetAttributeValue(attriValue, "");

                }
                catch (Exception xe)
                {
                    Console.WriteLine("Meta节点：" + attriKey + "=" + keyValue + "未能找到属性" + attriValue);
                }
            }
            else
            {
                Console.WriteLine("未能寻找Meta节点：" + attriKey + "=" + keyValue);
            }
            return ret;
        }

        /// <summary>
        /// 删除描述中，指定的属性对值。   属性：属性值  
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string removeStringKeyValue(string[] keys, string source)
        {
            if(null == source || source == "")
            {
                return "";
            }
            foreach(string key in keys)
            {
                Regex regex_keyvalueData = new Regex(key + ":(.*?)[，。]");
                Match match_keyvalueData = regex_keyvalueData.Match(source);
                if (match_keyvalueData.Success)
                {
                    string keyvalue = match_keyvalueData.Value.ToString();
                    source = source.Replace(keyvalue, "");
                }
                //手机侧不适宜换行 观看。
                //source = source.Replace('，', '\n');
            }
            return source;
        }

        protected string getStringKeyValue(string key, string source)
        {

            string value = "";
                Regex regex_keyvalueData = new Regex(key + ":(.*?)，");
                Match match_keyvalueData = regex_keyvalueData.Match(source);
            if (match_keyvalueData.Success)
            {
                string keyvalue = match_keyvalueData.Value.ToString();
                value = keyvalue.Replace(key + ":", "").Replace("，", "").Trim();
            }
            return value;
        }
    }

    public class SkuPropsItem
    {
        /// <summary>
        /// 变体类型名称，例如：颜色
        /// </summary>
        public string prop { get; set; }
        /// <summary>
        /// 具体的变体值，包括图片等
        /// </summary>
        public List<SkuPropsItemValue> value { get; set; }


    }

    public class SkuPropsItemValue
    {
        /// <summary>
        /// 变体具体的名称例如：白色
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 变体图片，如果为空则无
        /// </summary>
        public string imageUrl { get; set; }
        
    }
    public class SkuMapItem
    {
        /// <summary>
        /// 组合变体类型名称，例如：黑色>XL
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string specId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int saleCount { get; set; }
        /// <summary>
        /// 打折价格
        /// </summary>
        public double discountPrice { get; set; }
        /// <summary>
        /// 备货数量
        /// </summary>
        public int canBookCount { get; set; }
        /// <summary>
        /// sku编号
        /// </summary>
        public string skuId { get; set; }


    }
}
