using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class ProductDetailBaseInfo
    {
        public Bundle_deal bundle_deal { get; set; }
        public long boost_cool_down_seconds;
        public string[] add_on_deal;//: []
        public AttributeModel attribute_model;//: {attribute_model_id: 3774,…}
        public string brand;//: "其他"
        public int[] category_path;//: [1611, 1615, 8137]
        public string[] category_recommend;//: []
        //public int gcondition = 1;//: 1
        public int days_to_ship = 2;//: 2
        public string description;//: "材料:【天然矽膠】↵產品尺寸:1↵適用機型:Gear Fit2↵產品重量:15G↵包裝清單:opp/產品↵顏色:大號：黑色,大號：灰色,大號：藍色,大號：藍青色,大號：天藍色,大號：水鴨綠,大號：白色,大號：紫羅蘭,大號：寶藍,大號：橘紅,大號：紫紅,大號：綠色,小號：黑色,小號：灰色,小號：藍色,小號：藍青色,小號：天藍色,小號：水鴨綠,小號：白色,小號：紫羅蘭,小號：寶藍,小號：橘紅,小號：紫紅,小號：綠色↵型號:fit2分體↵↵❤產品選項如下:[下標時請備註]↵☑[1]: 小號：橘紅↵☑[2]: 小號：藍青色↵☑[3]: 小號：紫紅↵☑[4]: 大號：水鴨綠↵☑[5]: 大號：綠色↵☑[6]: 小號：藍色↵☑[7]: 大號：紫紅↵☑[8]: 小號：紫羅蘭↵☑[9]: 小號：白色↵☑[10]: 大號：寶藍↵☑[11]: 小號：天藍色↵☑[12]: 大號：黑色↵☑[13]: 小號：綠色↵☑[14]: 大號：橘紅↵☑[15]: 大號：天藍色↵☑[16]: 小號：黑色↵☑[17]: 大號：藍青色↵☑[18]: 大號：灰色↵☑[19]: 小號：寶藍↵☑[20]: 小號：灰色↵☑[21]: 大號：藍色↵☑[22]: 大號：紫羅蘭↵☑[23]: 小號：水鴨綠↵☑[24]: 大號：白色↵#智能手環錶帶 #不銹鋼智能錶帶"
        public ProductDimension dimension;//: {width: 0, height: 0, length: 0}
        public long id;//: 560708230
        public string[] images;//: ["0ac677632d8ce320f64a67f9f0929ac9", "506897d6b59d12cbc1171cf2db3a2f83",…]
        public int like_count;//: 6
        public InstallmentTenures installment_tenures;//: {status: 0, enables: []}
        public LogisticsChannel[] logistics_channels;//: [{id: 38011, channelid: 38011, sizeid: 0, size: 0.05, price: "70.00", cover_shipping_fee: false,…},…]
        public ProductModel[] model_list;//: []
        public string name;//: "矽膠錶帶_fit2智能手環矽膠錶帶samsung替換手腕帶現貨【新品】"
        public string parent_sku;//: "AL537688867043"
        public bool in_promotion;//: false
        public bool pre_order = false;//: false
        public string price;//: "169.00"
        public string price_before_discount;//: "0.00"
        public string size_chart;//: ""
        public int sold;//: 0
        public int stock;//: 489
        public TierVariation[] tier_variation;//: "tier_variation": [{"images": [], "name": "Variation", "options": ["E\u6b3e", "F\u6b3e", "G\u6b3e", "D\u6b3e", "A\u6b3e", "C\u6b3e"]}],
        public bool unlisted = false;//: false
        public int view_count = 0;//: 0
        public string weight;//: "0.05"
        public WholesaleInfo[] wholesale_list;//: []wholesale_list
        public long create_time;// : 1526985993
        public long modify_time;//: 1568180442
        public int status;
        public string ToJsonString()
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ProductDetailBaseInfo：" + ex.Message);
                return "";
            }
            
        }
    }

    public class AttributeModel
    {
        public int attribute_model_id = 3774;//: 3774
        public ProductAttriuteItem[] attributes = { new ProductAttriuteItem()};//: [{attribute_id: 1407, prefill: false, status: 1, value: "自有品牌"}]

    }
    public class ProductAttriuteItem
    {
        public int attribute_id = 1407;//: 1407, prefill: false, status: 1, value: "自有品牌"}
                                       //attribute_id: 1407
        //public bool prefill = false;//: false
        public int status = 2;//: 1
        public string value = "自有品牌";//: "自有品牌"
    }
    public class ProductDimension
    {
        public int width = 0;//: 0, 
        public int height = 0;//: 0, 
        public int length = 0;//: 0

        public ProductDimension(int width,int height,int length)
        {
            this.width = width;
            this.height = height;
            this.length = length;
            
        }
    }
    public class InstallmentTenures
    {
        public int status = 0;//: 0, 
        public string[] enables;//: []
        public int tip_type = 0;
    }
    public class LogisticsChannelList
    {
        public LogisticsChannel[] list;
    }
    public class LogisticsChannel
    {
        public long channelid;//: 38011//通过产品id获得时，这个字段是channel_id
        public bool cover_shipping_fee = false;//: false
        public bool enabled = true;//: true
        
        //public string id;//: 38011 
        public string item_flag = "0";//
        public string price;//: "70.00"
        public double size;//: 0.05
        public int sizeid;//: 0//通过产品id获得时，size_id
        public LogisticsChannel(long channelId,bool coverShippingFee,bool enabled,string id,string flag,string price,double size,int sizeid)
        {
            this.channelid = channelId;
            this.cover_shipping_fee = coverShippingFee;
            this.enabled = enabled;
            //this.id = id;
            this.item_flag = flag;
            this.price = price;
            this.size = size;
            this.sizeid = sizeid;
        }
    }

    public class ProductModel
    {
        public string[] add_on_deal;//: []
        public string flash_sale_status;//: "empty"
        public long id;//: 434451773
        public bool in_promotion = false;//: false
        public string name;//: "S"
        public string price;//: "169.00"
        public string price_before_discount;//: "0.00"
public int promotion_id;//: 0
        public int promotion_source;//: 0
        public int promotion_stock;//: 0
        public string sku;//: "AL532623266005_0"
        public int stock;//: 500
public int[] tier_index;//: [0]
    }
    public class WholesaleInfo
    {

        public int max_count;//: 20
        public int min_count;//: 3
        public string price;//: "149.00"
        public WholesaleInfo(int minCount ,int maxCount,double curPrice)
        {
            this.max_count = maxCount;
            this.min_count = minCount;
            this.price = Math.Round(curPrice,2).ToString();
        }
    }
    public class TierVariation : VariationTheme
    {

        //public string[] images;//": [], 
        //public string name;// "Variation", ;
        //public string[] options;//;: ["E\u6b3e", "F\u6b3e", "G\u6b3e", "D\u6b3e", "A\u6b3e", "C\u6b3e"]}],
    }

    public class Bundle_deal
    {
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Label_listItem> label_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
    }
    public class Label_listItem
    {
        /// <summary>
        /// 4 件 9.7折
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
    }
}
