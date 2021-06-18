using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    class ReturnInfo
    {
    }
    public class ReturnList
    {
        static public ReturnList FromJson(String str)
        {
            ReturnList ret = null;
            try
            {
                ret = JsonConvert.DeserializeObject<ReturnList>(str);
            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(ReturnList) + "\r\n转换Json失败：" + xe.Message.ToString() + "\r\n" + str);
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_message { get; set; }


        public class Page_info
        {
            /// <summary>
            /// 
            /// </summary>
            public long total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long page_number { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long page_size { get; set; }
        }

        public class Product
        {
            /// <summary>
            /// 
            /// </summary>
            public string sku { get; set; }
            /// <summary>
            /// 春秋季休閒可外穿家居服 莫代爾舒適睡衣
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_pre_order { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> images { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
        }

        public class Model
        {
            /// <summary>
            /// 
            /// </summary>
            public string sku { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// M/粉色套装
            /// </summary>
            public string name { get; set; }
        }

        public class Bundle_deal_product_listItem
        {
            /// <summary>
            /// 
            /// </summary>
            public Product product { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Model model { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string product_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long snapshot_id { get; set; }
        }

        public class Bundle_deal_labelsItem
        {
            /// <summary>
            /// 2 件折$ $50
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string language { get; set; }
        }

        public class Bundle_deal
        {
            /// <summary>
            /// 
            /// </summary>
            public List<Bundle_deal_product_listItem> bundle_deal_product_list { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long bundle_deal_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string total_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string total_price_before_bundle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Bundle_deal_labelsItem> bundle_deal_labels { get; set; }
        }


        public class Order_product_listItem
        {
            /// <summary>
            /// 
            /// </summary>
            public long status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_add_on_sub_item { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Product product { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long bundle_deal_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_wholesale { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string product_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Bundle_deal bundle_deal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string order_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long add_on_deal_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Model model { get; set; }
        }

        public class Buyer
        {
            /// <summary>
            /// 
            /// </summary>
            public string portrait { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shop_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string followed { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
        }

        public class ListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public long judging_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refund_amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_sn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long logistics_status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shop_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long seller_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Order_product_listItem> order_product_list { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopee_handle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_ship_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_seller_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string with_resolution_center { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string official_shop { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long order_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long actual_deliver_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long request_return_response_end_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Buyer buyer { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long dispute_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string amount_before_discount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string need_logistics { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public Page_info page_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ListItem> list { get; set; }
        }

    }
    public class ReturnDetail
    {
        static public ReturnDetail FromJson(String str)
        {
            ReturnDetail ret = null;
            try
            {
                ret = JsonConvert.DeserializeObject<ReturnDetail>(str);
            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(ReturnDetail) + "\r\n转换Json失败："+ xe.Message.ToString()+"\r\n" + str);
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_message { get; set; }

        public class Product
        {
            /// <summary>
            /// 
            /// </summary>
            public string sku { get; set; }
            /// <summary>
            /// 2019年男女童側拉鏈保暖中小童防滑加棉童靴棉鞋
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_pre_order { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> images { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
        }

        public class Bundle_deal
        {
        }

        public class Model
        {
            /// <summary>
            /// cb9g-48-酒紅色-36碼內長21.5cm
            /// </summary>
            public string sku { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 酒紅色,36碼內長21.5cm
            /// </summary>
            public string name { get; set; }
        }

        public class Order_product_listItem
        {
            /// <summary>
            /// 
            /// </summary>
            public long status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_add_on_sub_item { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Product product { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long total_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long total_price_before_discount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string is_wholesale { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string product_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Bundle_deal bundle_deal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string order_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long add_on_deal_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Model model { get; set; }
        }

        public class Return_cvs_address
        {
            /// <summary>
            /// 
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long address_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string area { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string store_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string phone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ic_number { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string store_id { get; set; }
        }

        public class Buyer
        {
            /// <summary>
            /// 
            /// </summary>
            public string portrait { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shop_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string followed { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
        }

        public class Return_address
        {
            /// <summary>
            /// 
            /// </summary>
            public long status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long address_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long user_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string phone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string geo_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string full_address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string town { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string state { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zip_code { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string seller_email { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long logistics_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long judging_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refund_amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long closed_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long logistics_status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shop_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long cancelled_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long seller_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_channel_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Order_product_listItem> order_product_list { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopee_handle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> seller_images { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string return_pickup_address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> buyer_images { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string official_shop { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tracking_number { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long requested_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_ship_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long accepted_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_seller_due_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string with_resolution_center { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Return_cvs_address return_cvs_address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string logistics_channel_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long order_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string buyer_email { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long actual_deliver_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long request_return_response_end_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Buyer buyer { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long return_sn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long dispute_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string amount_before_discount { get; set; }
            /// <summary>
            /// 訂兩雙只送一雙來
            /// </summary>
            public string text_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Return_address return_address { get; set; }
            /// <summary>
            /// 少發1雙鞋子，請買家只退1雙，因為封店問題和客戶溝通不了
            /// </summary>
            public string dispute_text_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string need_logistics { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long refund_paid_time { get; set; }
        }



    }
}
