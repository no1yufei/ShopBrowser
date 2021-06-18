using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public class OrderSummaryInfo
    {
        public int refund =0;
        public int refund_processed;
        public int refund_unprocessed;
        public int request_cancellation;
        public int shipping;
        //public int shipping_method;
        public int toship = 0;
        public int toship_processed;
        public int toship_unprocessed;
        public int unpaid_offline;
    }
    public class OrderInfos
    {
        public ItemModel[] itemmodels;//item-models
        public OrderItem[] orderitems;//order-items
        public Order[] orders;
        public Product[] products;
        public OrderUser[] users;


        public static OrderInfos FromJson(String jsonStr)
        {
            OrderInfos orderInfos = null;
            try
            {
                orderInfos = JsonConvert.DeserializeObject<OrderInfos>(jsonStr.Replace("item-models", "itemmodels").Replace("order-items", "orderitems"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderInfos;
        }
    }

    public class OrderInfo
    {
        public ItemModel[] itemmodels;//item-models
        public OrderItem[] orderitems;//order-items
        public Order order;
        public Product[] products;

        public static OrderInfo FromJson(String jsonStr)
        {
            OrderInfo orderInfos = null;
            try
            {
                orderInfos = JsonConvert.DeserializeObject<OrderInfo>(jsonStr.Replace("item-models", "itemmodels").Replace("order-items", "orderitems"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderInfos;
        }
    }
    public class OrderBaseInfo
    {
        public ItemModel[] itemmodels;//item-models
        public OrderItem[] orderitems;//order-items
        public Order[] orders;
        public Product[] products;

        public static OrderInfos FromJson(String jsonStr)
        {
            OrderInfos orderInfos = null;
            try
            {
                orderInfos = JsonConvert.DeserializeObject<OrderInfos>(jsonStr.Replace("item-models", "itemmodels").Replace("order-items", "orderitems"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderInfos;
        }
    }
    public class ItemModel
    {
        public string currency;//: "TWD"
                               //flash_sale: ""
                               //flash_sale_status: "empty"
        public string id;//: "1408830135-3961787011"
        public long itemid;//: 2141652806
        public string modelid;//: 3961787011
        public string name;//: "黑色,均碼"
        public float price;//: "299.00"
                           //price_before_discount: "0.00"
                           //promo_source: 0
                           //promo_stock: 0
                           //promotion_refresh_time: 0
                           //promotionid: 0
                           //rebate_price: "0.00"
                           //sip_stock: 0
        public string sku;//: "569662233968"
                          //sold: 0
                          //status: 1
                          //stock: 100
    }
    public class OrderItem
    {
        public string add_on_deal_id;//: 0
        public int amount;//: 2
                          //bundle_deal_id: null
                          //comm_fee_rate: "0.0"
        public string groupid;//: "0"
        public string id;//: "order-1411621647-2055093014-3685364325-0"
        public bool is_add_on_sub_item;//: false
        public bool is_wholesale;//: false
        //item_list: []
        public float item_price;//: "199.00"
        public string modelid;//: "1411621647-3685364325"
        public float order_price;//: "199.00"
                                 //price_before_bundle: 0
        public string snapshotid;//: 472688518
        public int status;//: 1
    }
    public class Order
    {
        public String actual_carrier;//: "蝦皮店配-全家"
        public float actual_price;//: "657.00"
        public float actual_shipping_fee;//: "0.00"
                                         //add_on_deal_id: 0
        public long arrange_pickup_by_date;//: 1560480130
        public long auto_cancel_3pl_ack_date;//: 1560912130
        public long auto_cancel_arrange_ship_date;//: 1560739330
                                                  //buyer_address_name: "鄭*芳"
                                                  //buyer_address_phone: "886932***531"
                                                  //buyer_address_zipcode: ""
                                                  //buyer_cancel_reason: 0
                                                  //buyer_is_rated: 0
                                                  //buyer_last_change_address_time: 0
        public float buyer_paid_amount;//: "597.00"
                                       //buyer_rate_cmtid: 0
                                       //cancel_reason: -1
                                       //cancel_reason_ext: 0
                                       //cancel_userid: 0
                                       //cancellation_end_date: ""
                                       //card_txn_fee_info: {card_txn_fee: "13.00", rule_id: 136}
                                       //    checkoutid: 1269111965
        public float coin_offset;//: "0.00"
                                                  //coin_used: 0
        public float comm_fee;//: "30.00"
                                                  //complete_time: 0
        public long create_time;//: 1560307256
                                //credit_card_number: ""
        public float credit_card_promotion;//: "0.00"
        public string currency;//: "TWD"
        public int days_to_ship;//: 2
        public long delivery_time;//: 0
                                //dropshipping_info: {phone_number: "", enabled: 0, name: ""}
                                //end_of_pickup_window_time: 0
                                //escrow_release_time: 1561862530
        public float estimated_shipping_rebate;//: "0.00"
                                //first_item_count: 2
                                //first_item_is_wholesale: false
        public string ordersn;
        public string first_item_model;//: "38黑色或棕色請備註"
                               public string first_item_name;//: "雨鞋 時尚防滑雨鞋 交換禮物 生日禮物切爾西 英倫女式雨鞋 防滑雨鞋 鬆緊帶韓版低筒"
                                //first_item_return: false
        public long id;//: 1411621647
                       //instant_buyercancel_toship: false
                       //is_buyercancel_toship: false
                       //is_request_cancellation: false
        public int item_count;//: 2
                              //list_type: 7
                              //logid: 0
                              //logistics_channel: 38020
                              //logistics_extra_data: "{"discounted_shipping_fees":{"shopee":6000000,"seller":6000000},"extra_flag":0}"
                              //logistics_flag: 536870922
        public long logistics_status;//: 9
        public string note;//: ""
                              //note_mtime: "0"
                              //order_command: ""
        public string[] order_items;//: ["order-1411621647-2055093014-3685364325-0", "order-1411621647-2055093014-3685364326-0"]
                                    //order_ratable: true
                                    //order_type: 2
                                    //ordersn: "19061210402JY2F"
        public float origin_shipping_fee;//: "60.00"
        public float paid_amount;//: "0.00"
                                    //pay_by_credit_card: false
                                    //pay_by_wallet: false
                                    //payby_date: 1560566455
                                    //payment_channel_name: "Cash On Delivery"
                                    //payment_method: 6
                                    //pickup_attempts: 0
                                    //pickup_cutoff_time: 0
                                    //pickup_date_description: ""
                                    //pickup_time: 0
        public float price_before_discount;//: "657.00"
                                           //rate_by_date: 0
                                           //rate_comment: ""
                                           //rate_star: 0
                                           //ratecancel_by_date: 0
        public string remark;//: ""
                             //return_id: 0
                             //seller_address: {status: 1, orderid: 0, name: "劉遠志", district: "宝安区", city: "深圳市", country: "CN", town: "",…}
                             //seller_address_id: 14184910
                             //seller_due_date: 0
                             //seller_is_rated: 0
                             //seller_rate_cmtid: 0
                             //seller_userid: 34797586
                             //ship_by_date: 1560480130
        public string shipping_address;//: "全家豐原綠山店 台中市豐原區北陽里南陽路６０號一樓 店號F003791"
                                       //shipping_confirm_time: 1560307330
        public float shipping_fee;//: "60.00"
        public float shipping_fee_discount;//: 0
                                    //shipping_method: 38020
                                    //shipping_proof: ""
                                    //shipping_proof_status: 0
                                    //shipping_rebate: "0.00"
                                    //shipping_remark: ""
        public string shipping_traceno;//: ""
        public long shopid;//: 34796202
        public int status;//: 1
        public long status_ext;//: 1
        public float tax_amount;//: "0.00"
        public float total_price;//: "657.00"
        public int used_voucher;//: 0
        public long userid;//: 95399
                           //voucher_absorbed_by_seller: false
        public string voucher_code;//: ""
        public float voucher_price;//: "0.00"
        public float wallet_discount;//: "0.00"
    }
    public class Product
    {
        //        brand: "0"
        //catid: 65
        //cmt_count: 0
        //condition: 1
        //ctime: 1553333254
        //currency: "TWD"
        //description: "♚型號:猩猩掛飾↵♚材質:尼龍↵♚尺寸:高4cm雙手臂長度12cm↵♚顏色:中號檸檬黃,中號古典紫,中號天藍,中號紅咖啡,中號土黃,中號大紅,中號漿果紫,中號藏青,中號純白,中號皇家紅,中號寶藍,中號深藍,中號淺紫,中號粉刷綠,中號青綠,中號葡萄籽,中號紫藍,中號淡天藍,（迷你猴）黑色,（迷你猴）藏青,（迷你猴）天藍,（迷你猴）皇家紅,（迷你猴）深藍,（迷你猴）桃紅,（迷你猴）紫藍,（迷你猴）灰色,（迷你猴）浪漫紫,中號桃紅,中號咖啡,（迷你猴）粉刷綠,（迷你猴）大紅,中號西瓜紅,（迷你猴）白色,（迷你猴）土豪金,中號土豪金,長毛猴白色（光面）,長毛猴白色（磨砂面）,中號粉藍,中號粉紫,（迷你猴）粉藍,迷你猴）粉紫,中號黑色,（迷你猴）日落黃,中號日落黃,長毛猴白色（銀面）,迷你猴古典紫,中號銀灰↵↵"
        //estimated_days: 2
        public long id;//: 454078466
        public string[] images;//: ["fc5c5623e6450804bd2cbe2cddbfe036"]
        //is_pre_order: false
        public long itemid;//: 2034067303
        //liked_count: 0
        public string name;//: "包配飾♚⚽供應2019春季大中號迷你毛絨猴子猩猩掛飾⚽包配飾⚽鑰匙扣"
        public string parent_sku;//: "AL586836152287"
        public string price;//: "279.00"
                            //price_before_discount: "0.00"
        public long shopid;//: 34796202
                           //sold: 0
                           //status: 1
                           //stock: 2000
    }
    public class OrderUser
    {
        //ba_check_status: 0
        //birth_timestamp: 0
        //ctime: 0
        //delivery_address_id: 0
        //delivery_order_count: 17
        //delivery_succ_count: 16
        //disable_new_device_login_otp: false
        //email: ""
        public string fbid;//: "1949097132003258"
        //feed_private: false
        public bool followed;//: false
                             //following_count: 3
                             //gender: 1
                             //hide_likes: 0
                             //holiday_mode: false
                             //holiday_mode_mtime: 0
        public long id;//: 39250033
                       //id_address_limit_info: "{}"
                       //kyc_consent: false
                       //language: "zh-Hant"
                       //machine_code: "android_gcm"
                       //phone: ""
                       //phone_public: false
                       //pn_option: 4294967295
                       //portrait: ""
                       //products: -1
                       //rating_count: [0, 0, 0, 0, 0, 5]
                       //    rating_star: 5
                       //score: -1
        public long shopid;//: 39248647
                           //status: 1
        public string username;//: "cj552"
//wallet_setting: 0
//wholesale_setting: 1
    }

}
