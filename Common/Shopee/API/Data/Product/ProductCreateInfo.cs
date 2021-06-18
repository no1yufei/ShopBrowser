using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class ProductCreateInfo
    {
        //按照json格式，定义数据项，数组和类
        //此处定义数据项
        //Post 数据
        //https://seller.xiapi.shopee.cn/api/v3/product/create_product/?SPC_CDS=ee3aa3f7-1f5f-451f-a2d6-8423c5d46978&SPC_CDS_VER=2

        //public string[] add_on_deal = new string[0];//: []
        public AttributeModel attribute_model = new AttributeModel();//: {attribute_model_id: 22695,…}
        public string brand = "自有品牌";
        public int[] category_path;//: [62, 1477, 19569]
        public int[][] category_recommend = new int[0][];//: [[62, 2170, 7696], [62, 1477, 11951], [62, 1477, 19570]]
        public int condition = 1;
        public int days_to_ship = 2;
        public string description;//: "均碼服裝.↵↵款式：套裝↵↵面料：雪紡/彈力緞↵↵↵↵組織：CM釐米↵↵上衣尺碼（均碼）：胸圍104/衣長32↵↵半身裙（均碼）：寬136 /裙長96"
        public ProductDimension dimension = new ProductDimension(10, 2, 20);// {width: 10, height: 2, length: 20}
        public string ds_cat_rcmd_id = "0";
        public long id = 0;
        public string[] images;//: ["b4dce7c13018f29a374c0c13388f3d67", "f00b851955962cf6c53c8cdb360c21f9",…]
        public InstallmentTenures installment_tenures;//: {}
        //这里的ID 是 "New-"+channelid
        public LogisticsChannel[] logistics_channels = new LogisticsChannel[]
                                   { new LogisticsChannel(38011, false, true, "New-38011", "0", "70", 0, 0),
                                      new LogisticsChannel(38012, false, true, "New-38012", "0", "60", 0, 0),
                                    new LogisticsChannel(38013, false, true, "New-38013", "0", "160.00", 0, 0),
                                    new LogisticsChannel(38020, false, true, "New-38020", "0", "60", 0, 0)};//: [{id: "New-38011", channelid: 38011, sizeid: 0, size: 0, price: "70", cover_shipping_fee: false,…},…]
        public ModelInfo[] model_list;//: [{id: 0, name: "", stock: 20, price: "496", sku: "GG-WD-LG-01-001", tier_index: [0, 0]},…]
        public string name;//: "肚皮舞2019新款套肚皮舞練功服新款套夏季思齊同款甘甘家遮肚女"
        public string parent_sku;//: "GG-WD-LG-01"
        public bool pre_order = false;//: false
        public string price;//: "496"
        //public string price_before_discount;//: ""
        public string size_chart="";//: ""
        public int stock;//: 20
        public VariationTheme[] tier_variation;//: [{name: "顏色", options: ["白色", "粉紅"], images: []}, {name: "尺寸", options: ["均碼"]}]
        public bool unlisted = true;// false
        public string weight = "0.45";
        public WholesaleInfo[] wholesale_list;//: []

        public string ToJson()
        {
            string dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
        public static string ArrayToJson(ProductCreateInfo[] products)
        {
            string dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.SerializeObject(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
        public static ProductCreateInfo FromJson(string jsonStr)
        {
            ProductCreateInfo dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<ProductCreateInfo>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
        public static T FromJson<T>(string jsonStr)
        {
            T dataTemplate =default(T);
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
    public class ModelInfo
    {
        public long id = 0;//: 0
        public string name="";//: ""
        public string price;//: "496"
        public bool is_default = true;
        public string item_price= "";
        public string sku;//: "GG-WD-LG-01-001"
        public int stock = 0;
        public int[] tier_index;//: [0, 0]
    }
    public class VariationTheme
    {
        public string name;
        public string[] options = new string[0];
        public string[] images ;
    }
}
