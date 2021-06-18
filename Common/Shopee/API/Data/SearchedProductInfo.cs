using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class SearchedProductInfo
    {
        public ProductItem[] items = new ProductItem[0];
        public static SearchedProductInfo DataJson(String json)
        {
            SearchedProductInfo customers = new SearchedProductInfo();
            try
            {
                customers = JsonConvert.DeserializeObject<SearchedProductInfo>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
    }
    public class ProductItem
    {
        //add_on_deal_info: null
        //ads_keyword: "iphone"
        //adsid: 1198980
        //badge_icon_type: 0
        //brand: "自有品牌"
        //bundle_deal_info: null
        //campaign_stock: null
        //campaignid: 1199152
        //can_use_bundle_deal: false
        //can_use_wholesale: false
        //catid: 70
        //cb_option: 0
        //cmt_count: 5
        //coin_earn_label: null
        //collection_id: null
        //ctime: 1557831213
        //currency: "TWD"
        //deduction_info: "dzFuc2pndnZxZXU4ejJyaI96tDtWFR1y05E3dAm7za212g2iY2LKAwwPKdZ03Gh0g21AsWVNbpk02ANjxbuj7UG43cXndgHQVqcb4ykltxnB5+CrznM++S1PQBlhr61ZQQzdoVz6GT6sU7Mpq2UjTi2Qbie96zd5sOiNuGKVX1AH8A8g0ZJgF5O0ize8o5P+PJWYj/WIqrtI1OrMu2dlKsar/d9pxs6TA9aYU3dryRsx788IygMkxjaxA+bWyD+Nza9e+rs9fqywLS9MCM1x8nIVvnMBuSL+l+XYo4R9I9AMOjCIC5sQQ4nPoYt4yK7l/zM6ogoFv+Vl/siQcjxn733hC492+rtR486fZOOhrMHTnrbru0rLk9tF3OepV0xoOgVIkBxOTEPHVVfF0u+chKa3pt/SD2rTG4qyfGNxt3kj98uQdH5k4DwsOQ6HWmVL7vNybbqb6bR5aP5VrkHZUDjx/M3nwxRKrV502Q8ntFS39QqaWtWNSvyYkD/6ZHWhNopjHYUe1o46JJJrPmQ0oXFop8MqunZfdpcz+A4daAhb8jzqwYrCPMA/LbJEP8li"
        //discount: "8.9"
        //display_name: null
        //distance: null
        //flag: 196608
        //flash_sale: null
        //group_buy_info: null
        //has_group_buy_stock: false
        //has_lowest_price_guarantee: false
        //hidden_price_display: null
        //historical_sold: 7
        public string image;//: "668cacb3d670a45af204c511a166817b"
                            //images: ["668cacb3d670a45af204c511a166817b", "e35dfe6cfd1a966c05a5f1e4dfb69fb5",…]
                            //installment_plans: null
                            //is_adult: null
                            //is_category_failed: false
                            //is_cc_installment_payment_eligible: false
                            //is_group_buy_item: null
                            //is_non_cc_installment_payment_eligible: false
                            //is_official_shop: true
        public ItemRateInfo item_rating;//: {rating_star: 4.6, rating_count: [5, 0, 0, 1, 0, 4], rcount_with_image: 0, rcount_with_context: 0}
                                        //item_status: "normal"
        public long itemid;//: 2223260229
                           //label_ids: [16]
                           //  liked: false
                           //liked_count: 18
                           //match_type: 1
                           //name: "iPhone 軍事十倍手機殼 防撞防摔保護殼 XS Max i7/8 X XS XR 保護殼 防摔殼【K12】"
                           //preview_info: null
                           //price: 49000000
                           //price_before_discount: 55000000
                           //price_max: 49000000
                           //price_max_before_discount: 55000000
                           //price_min: 49000000
                           //price_min_before_discount: 55000000
                           //raw_discount: 11
                           //recommendation_info: null
                           //service_by_shopee_flag: 0
                           //shipping_icon_type: null
                           //shop_location: "台南市北區"
                           //shopee_verified: false
        public long shopid;//: 1982863
                           //show_discount: 11
                           //show_free_shipping: false
                           //show_official_shop_label: true
                           //show_official_shop_label_in_normal_position: null
                           //show_official_shop_label_in_title: true
                           //show_shopee_verified_label: false
                           //size_chart: null
                           //sold: 7
                           //status: 1
                           //stock: 982
                           //tier_variations: []
                           //    upcoming_flash_sale: null
                           //video_info_list: []
        public string view_count;//: 1565
                              //welcome_package_info: null
                              //welcome_package_type: 0
                              //wholesale_tier_list: []
    }
    public class ItemRateInfo
    {
        public float rating_star;//: 4.6, 
        public int[] rating_count;//; [5, 0, 0, 1, 0, 4], rcount_with_image: 0, rcount_with_context: 0}
    }

    public class SearcheShopInfos
    {
        public string json_data;//: ""
        public int total_count;//: 17
        public SearcheShopInfo[] users;//: [{username: "amyh7091", followed: false, shopname: "《時尚寶盒》", following_count: 4, shopid: 7211549,…},…]
    }
    public class SearcheShopInfo
{
    public long adsid;//: 0
    public string country;//: "TW"
                          //data: {total_count: 0, items: []}
    public bool followed;//: false
    public int follower_count;//: 457
    public int following_count;//: 4
                               //is_official_shop: false
                               //json_data: ""
                               //last_login_time: 1575806444
                               //nickname: "《時尚寶盒》"
                               //pickup_address_id: 2322892
                               //portrait: ""
                               //products: 1310
                               //response_rate: 100
                               //response_time: 10726
                               //score: 3440
                               //shop_rating: 4.906535
                               //shopee_verified_flag: 0
    public long shopid;//: 7211549
                       //shopname: "《時尚寶盒》"
                       //show_official_shop_label: false
                       //show_shopee_verified_label: false
                       //status: 1
    public long userid;//: 7212842
    public string username;//: "amyh7091"
}
}
