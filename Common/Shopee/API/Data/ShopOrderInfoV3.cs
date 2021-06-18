using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
   
    public class OrderListInfo
    {
        public OrderListMeta meta;//: {from_seller_data: false, total: 2, limit: 40, offset: 0}
        public OrderInfoV3[] orders;//: [{comm_fee: "0.00", shipping_method: 10006, payment_method: 6, wallet_discount: "0.00",…},…]
    }
public class OrderListMeta
{
    public bool from_seller_data;//: false
    public int limit;//: 40
    public int offset;//: 0
    public int total;//: 2
}

public class OrderInfoV3
    {
        public string Region = "tw";
        public string actual_carrier;//: "蝦皮店配-全家"
        public string actual_price;//: "528.00"
        public double actual_shipping_fee;//: "0.00"
                                          //add_on_deal_id: 0
                                          //arrange_pickup_by_date: 1573832080
                                          //auto_cancel_3pl_ack_date: 1574264080
                                          //auto_cancel_arrange_ship_date: 1574091280
                                          //buyer_address_name: "張*蓁"
                                          //buyer_address_phone: "886981***733"
                                          //buyer_cancel_reason: 0
                                          //buyer_is_rated: 0
                                          //buyer_last_change_address_time: 0
        public double buyer_paid_amount;//: "468.00"
                                   //buyer_txn_fee: "0.00"
        public BuyerUserInfo buyer_user;//: {phone_public: false, buyer_rating: 0, user_id: 10853869, language: "zh-Hant", hide_likes: 0,…}
        public int cancel_reason_ext;//: 0
                                        //cancel_userid: 0
                                        //cancellation_end_date: null
                                        //card_txn_fee_info: {card_txn_fee: "11.00", rule_id: 136}
                                        //carrier_shipping_fee: 0
                                        //checkout_id: 1864105618
                                        //coin_offset: "0.00"
                                        //coin_used: "0.00"
                                        //coins_by_voucher: 0
                                        //coins_cash_by_voucher: "0.00"
        public double comm_fee;//: "28.00"
                                 //complete_time: 0
        public long create_time;//: 1573481966
                                //credit_card_number: ""
                                //credit_card_promotion_discount: "0.00"
        public string currency;//: "TWD"
                               //delivery_time: 0
                               //dropshipping_info: {phone_number: 0, enabled: 0, name: ""}
                               //escrow_release_time: 1575214480
                               //estimated_shipping_rebate: "0.00"
                               //express_channel: 0
                               //first_item_count: 1
                               //first_item_is_wholesale: false
                               //first_item_model: "XXL,不加绒"
                               //first_item_name: "韓劇鬼怪李棟旭同款背後黑白綁帶衛衣時尚寬鬆男女情侶款上衣"
                               //first_item_return: false
                               //instant_buyercancel_toship: false
                               //is_buyercancel_toship: false
                               //is_request_cancellation: false
        public int item_count;//: 1
        public int list_type;//: 7
                             //logid: 0
        public long logistics_channel;//: 38020
        public string logistics_extra_data;//: "{"discounted_shipping_fees":{"shopee":6000000,"seller":6000000},"discount_shipping_fees":{"shopee":0,"seller":0},"extra_flag":0}"
                                           //logistics_flag: 536870922
        public int logistics_status;//: 9
        public string note;//: ""
                           //note_mtime: 0
        public long order_id;//: 2138392711
        public OrderItemV3[] order_items;//: [{status: 1, is_add_on_sub_item: false,…}]

        //order_ratable: true
        public string order_sn;//: "19111122194BYG7"
                               //order_type: 2
        public double origin_shipping_fee;//: "60.00"
                               //paid_amount: "0.00"
                               //pay_by_credit_card: false
                               //pay_by_wallet: false
                               //payby_date: 1573741164
                               //payment_method: 6
                               //pickup_attempts: 0
                               //pickup_cutoff_time: 0
                               //pickup_time: 0
                               //price_before_discount: "528.00"
                               //rate_by_date: 0
                               //rate_comment: ""
                               //rate_star: 0
                               //ratecancel_by_date: 0
        public string remark;//: ""
        public long return_id;//: 0
                             //seller_address: {status: 2, city: "深圳市", address_id: 9158860, user_id: 11437734, name: "劉遠志", district: "宝安区",…}
                             //seller_address_id: 9158860
                             //seller_due_date: 0
                             //seller_service_fee: "0.00"
        public long seller_userid;//: 11437734
                                  //ship_by_date: 1573832080
                                  //shipment_config: true
                                  //shipping_address: "全家高雄延慶店 高雄市三民區第二安發里延慶街１１３號一樓 店號F007972"
                                  //shipping_confirm_time: 1573486480
                                  //shipping_fee: "60.00"
                                  //shipping_fee_discount: 0
                                  //shipping_method: 38020
                                  //shipping_proof: ""
                                  //shipping_proof_status: 0
                                  //shipping_rebate: "0.00"
        public string shipping_traceno="";//: ""
        public long shop_id;//: 11436434
        public int status;//: 1
        public int status_ext;//: 1
                            //tax_amount: "0.00"
        public string total_price;//: "528.00"
                                  //trans_detail_shipping_fee: ""
                                  //used_voucher: 0
        public long user_id;//: 10853869
                            //voucher_absorbed_by_seller: false
        public string voucher_code = "";
//voucher_price: "0.00"
//wallet_discount: "0.00"
        public override string ToString()
        {
            return buyer_user==null?"NoName":buyer_user.user_name;
        }
    }
   
    public class BuyerUserInfo
    {
        public long user_id;//: 39250033
        public long shop_id;//: 39248647
        public string user_name= "NoName1";//: "cj552"
        public string phone = "";//:: "886987866847"
        public string delivery_succ_count = "";//:: 46
        public string delivery_order_count = "";//:: 46
        public string email = "";//:: "a0987866848@gmail.com"
        public string rating_star = "";//:: 4.77777777777778
    }
public class OrderItemV3
{

    //add_on_deal_id: 0
    public int amount;//: 1
                      //bundle_deal: null
        public int bundle_deal_id;//: 0
        public List<OrderItemModel> bundle_deal_model;//: []
        public List<OrderItemProduct> bundle_deal_product;//: []
        public List<Item_listItem> item_list;
        //comm_fee_rate: "0.0"
        //group_id: 0
        //is_add_on_sub_item: false
        //is_wholesale: false
        public long item_id;//: 159993479
                        //item_list: []
    public OrderItemModel item_model;//: {status: 1, model_id: 8049410732, name: "XXL,加绒", price: "468.00", ctime: 1571108988, currency: "TWD",…}
                                     //item_price: "468.00"
        public long model_id;//: 8049410732
                                     //order_price: "468.00"
                                     //price_before_bundle: "0.00"
    public OrderItemProduct product;//: {cmt_count: 21, cat_id: 62, currency: "TWD", shop_id: 11436434, snapshot_id: 1053099773,…}
        public long snapshot_id;
        public string ItemImgUrl = "";
    }
    public class OrderItemModel
{
    // ctime: 1571108988
    //currency: "TWD"
    public long item_id;//: 159993479
    public long model_id;//: 8049410732
    public long mtime;//: 1572316302
    public string name;//: "XXL,加绒"
        public string price;//: "468.00"
        public string price_before_discount;//: "585.00"
                       //promotion_id: 1039915536
                       //rebate_price: "0.00"
    public string sku;//: "543915927042"
//sold: 0
//status: 1
//stock: 188
}

    public class Item_listItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long item_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long model_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long snapshot_id { get; set; }
    }
    public class OrderItemProduct
{
    //branch: ""
    //brand: "0"
    //cat_id: 62
    //cmt_count: 21
    //condition: 1
    //ctime: 1488181635
    //currency: "TWD"
    //description: "是否套裝:否↵風格:韓版↵淘貨類別:青春流行（18-24歲）↵款式:套頭↵有無內膽:無內膽↵領型:無領↵領口形狀:圓領↵袖長:長袖↵是否連帽:不連帽↵厚薄:普通↵主面料成分:棉↵主面料成分的含量:100（%）↵面料名稱:棉↵圖案:純色↵顏色:黑色不加絨綁帶↵尺碼:S,M,L,XL,XXL↵#情侶款上衣 #綁帶大學T #韓劇大學T"
    //estimated_days: 2
    public string[] images;//: ["155ed594ff616070b170a3aba033784a", "1c22dba50cf4286eb6e50b1dedab6744",…]
                           //is_pre_order: false
    public long item_id;//: 159993479
                        //liked_count: 91
    public string name;//: "韓劇鬼怪李棟旭同款背後黑白綁帶衛衣時尚寬鬆男女情侶款上衣"
                       //price: "468.00"
                       //price_before_discount: "585.00"
                       //shop_id: 11436434
    public string sku;//: "543915927042"
    public long snapshot_id;//: 1053099773
//sold: 38
//status: 1
//stock: 1878
}

    public class OrderIdList
    {
        // from_seller_data: true
        public long[] order_ids;//: [40556651896412, 40567870946712, 40721980886046, 40736418419221]
        public int page_number;//: 1
        public int page_size;//: 40
        public int total;//: 4
    }
    public class Orders
    {
        public OrderInfoV3[] orders;
    }

    public class ForderList
    {
        // from_seller_data: true
        public ForderInfo[] forders;//: [40556651896412, 40567870946712, 40721980886046, 40736418419221]
        public int page_number;//: 1
        public int page_size;//: 40
        public int total;//: 4
    }
    public class ForderInfo
    {
        public long order_id;
        public long shop_id;// : 58583742
    }
}
