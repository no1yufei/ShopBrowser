using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductCateAttributes
    {
        public CateAttributeModel[] list;
    }
    public class CateAttributeModel
    {
        public int attribute_model_id;//: 2576
        public CateAttribute[] attributes;//: [{status: 1, display_name: "品牌", name: "Brand: L2 Sportswear [2170]", attribute_type: 2,…}]
    }
    public class CateAttribute
    {
        public int attribute_id;//: 956
        public int attribute_type;//: 2
        public CateAttributeItems data;//: {description: "", multi_lang: [{lang: "zh-Hant", placeholder: "", display_name: "品牌",…}], value: [],…}
        public string display_name;//: "品牌"
        public int input_type;//: 3
        public bool mandatory;//: true
        public bool mandatory_mall;//: true
        public string name;//: "Brand: L2 Sportswear [2170]"
        public int status;//: 1
        public int validate_type;//: 2
    }
    public class CateAttributeItems
    {
        public int brand_option;//: 1
        public string description;//: ""
        public bool is_filter;//: true
        public CateAttributeItemValue[] multi_lang;//: [{lang: "zh-Hant", placeholder: "", display_name: "品牌",…}]
        public string placeholder;//: ""
        public string tooltip;//: "請選取商品的品牌。　　　　　　　　　　　提醒您，您可以先利用搜尋確認品牌是否有在選單內，若選單內沒有符合的品牌，請點選「自行設定」新增品牌名稱，建立格式為"英文+半型空格+中文"［註：英文大小寫請以商標為主；請輸入品牌名稱而非商店名稱］　　　　若品牌被拒絕可能是因格式不符或未達建立標準，請您改選擇「自有品牌」；您也可提供專利或是商標登記證明並來信蝦皮客服信箱，我們將協助新增品牌。"
                              //public string value;//: []
    }
    public class CateAttributeItemValue
    {
        public string display_name;//: "品牌"
        public string lang;//: "zh-Hant"
        public string placeholder;//: ""
        public string tooltip;//: "請選取商品的品牌。　　　　　　　　　　　提醒您，您可以先利用搜尋確認品牌是否有在選單內，若選單內沒有符合的品牌，請點選「自行設定」新增品牌名稱，建立格式為"英文+半型空格+中文"［註：英文大小寫請以商標為主；請輸入品牌名稱而非商店名稱］　　　　若品牌被拒絕可能是因格式不符或未達建立標準，請您改選擇「自有品牌」；您也可提供專利或是商標登記證明並來信蝦皮客服信箱，我們將協助新增品牌。"
        public string[] value;//: ["自有品牌", "2％", "46％", "47", "0918", "2143", "#FR2", "#nude", "& byP&D", "& Other Stories",…]
    }
}
