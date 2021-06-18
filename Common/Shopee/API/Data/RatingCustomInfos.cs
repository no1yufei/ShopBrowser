using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class RatingCustomInfos
    {
        public String version;//":"7d9557bd61a63e05c4412e213a13eac3",
        public Ratings data = new Ratings();//":{ 

        public static RatingCustomInfos DataJson(String json)
        {
            RatingCustomInfos customers = new RatingCustomInfos();
            try
            {
                customers = JsonConvert.DeserializeObject<RatingCustomInfos>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
    }
    public class Ratings
    {
        public RatingItem[] ratings = new RatingItem[0];
    }
    public class RatingItem
    {
        public long itemid;//":2197837749,"rating":1,"liked":null,
        public long shopid;//":30032531,"show_reply":false,
                           //"product_items":[{"itemid":2197837749,"welcome_package_info":null,"liked":null,"recommendation_info":null,"bundle_deal_info":null,"price_max_before_discount":35000000,"image":"7db43feede0854dd1fe51f975c485f11","is_cc_installment_payment_eligible":false,"shopid":30032531,"can_use_wholesale":false,"group_buy_info":null,"currency":"TWD","raw_discount":57,"show_free_shipping":null,"video_info_list":null,"images":["7db43feede0854dd1fe51f975c485f11","eeb1e4041fffeb195c68bddbad667538","8f2d31d382ee5758924dad4b9f4df1ff","5594391d956cfa98d1b28c6ea91a3251","9bee920de38012bee5a270e5b4f1455c","5dac22e2527719ef5b893ad51beec0ee","9df7e5f084885f196ae8f336cf3be49d","2f5569c8432a38dad62ed1c3ce3cd6ec","495b8f992b8e5592fcec5626aaec9a3f","df16e8ccac0e249fb5206aded4640316","fc73393a396f7dcc60c40884dc4fc123","944c164a4bc6213b44ba36f3a58b4e4d"],"installment_plans":null,"price_before_discount":35000000,"is_category_failed":false,"show_discount":57,"cmt_count":1,"view_count":null,"catid":70,"upcoming_flash_sale":null,"is_official_shop":true,"brand":"\u81ea\u6709\u54c1\u724c","price_min":14900000,"liked_count":3,"can_use_bundle_deal":false,"show_official_shop_label":true,"coin_earn_label":null,"is_snapshot":1,"price_min_before_discount":35000000,"cb_option":0,"sold":null,"stock":499,"status":1,"price_max":14900000,"add_on_deal_info":null,"is_group_buy_item":null,"flash_sale":null,"modelid":4126068440,"price":14900000,"shop_location":null,"item_rating":null,"show_official_shop_label_in_title":true,"tier_variations":[],"is_adult":null,"discount":"4.3","flag":196608,"is_non_cc_installment_payment_eligible":false,"has_lowest_price_guarantee":false,"snapshotid":454917436,"has_group_buy_stock":null,"preview_info":null,"welcome_package_type":0,"name":"\u6df7\u8272\u78e8\u7802\u624b\u6a5f\u6bbc \u9632\u649e\u9632\u6454 \u819a\u8cea\u611f \u4e0d\u5361\u819c \u860b\u679c\u4fdd\u8b77\u6bbc \u9632\u6454\u6bbc \u9069\u7528iPhone7/8Plus/X/Xs/XsMAX/XR","ctime":1557305721,"wholesale_tier_list":[],"show_shopee_verified_label":false,"show_official_shop_label_in_normal_position":null,"item_status":"normal","shopee_verified":null,"hidden_price_display":null,"size_chart":null,"shipping_icon_type":null,"label_ids":[],"service_by_shopee_flag":0,"badge_icon_type":0,"historical_sold":null,"model_name":"iPhoneXR\u3010\u9ed1\u8272\u3011"}],
                           //"like_count":null,
        public long mtime;//:1558682219,
                          //"mentioned":[],
                          //"ItemRatingReply":{"orderid":null,"itemid":null,"cmtid":null,"ctime":null,"mentioned":null,"rating":null,"editable":null,"userid":null,"shopid":null,"comment":null,"filter":null,"rating_star":null,"status":null,"mtime":null,"opt":null,"is_hidden":null},
                          //"is_hidden":false,
        public string author_portrait;//":"", imagePath =  https://cf.shopee.tw/ + author_portrait
        public  string orderid;//":1336936887,
                                      //"cmtid":1261010042,
                                      //"editable_date":1561274219,
                                      // "opt":2,"status":2,
        public string author_username;//":"e*****1",
                                      //"tags":null,
                                      //"images":null,
                                      //"delete_operator":null,
                                      //"editable":1,
                                      //"anonymous":true,
                                      //"ctime":1558682219,
                                      //"rating_star":5,
        public long author_shopid;//":6394895,
        public long userid;//":6396208,
                           // "comment":"",
                           // "filter":0,
                           //"delete_reason":null},
    }
}
