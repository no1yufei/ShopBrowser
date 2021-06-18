using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class ProductItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Item item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string error_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string error { get; set; }
    }
    //public class Coin_info
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int spend_cash_unit { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<string> coin_earn_items { get; set; }
    //}

    public class Video_info_listItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string video_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumb_url { get; set; }
    }

    public class Extinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string seller_promotion_limit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_shopee_promo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string group_buy_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string holiday_mode_old_stock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> tier_index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seller_promotion_refresh_time { get; set; }
    }

    public class ModelsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string itemid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// #1 蜜粉刷
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Extinfo extinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long modelid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_before_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int promotionid { get; set; }
    }

    public class Item_rating
    {
        /// <summary>
        /// 
        /// </summary>
        public double rating_star { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> rating_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rcount_with_image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rcount_with_context { get; set; }
    }

    public class Tier_variationsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> images { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> options { get; set; }
    }

    public class AttributesItem
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int idx { get; set; }
        /// <summary>
        /// 自有品牌
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_pending_qc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_timestamp { get; set; }
    }

    public class CategoriesItem
    {
        /// <summary>
        /// 美妝保健
        /// </summary>
        public string display_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string catid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string no_sub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_default_subcat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string block_buyer_platform { get; set; }
    }

    public class Item
    {
        /// <summary>
        /// 
        /// </summary>
        public string itemid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string welcome_package_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recommendation_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string bundle_deal_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_max_before_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public Coin_info coin_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_cc_installment_payment_eligible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string can_use_wholesale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string raw_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_free_shipping { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Video_info_listItem> video_info_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int service_by_shopee_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> images { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string installment_plans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_before_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_category_failed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int estimated_days { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cmt_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string view_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cod_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string catid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipping_icon_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string upcoming_flash_sale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_official_shop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slash_lowest_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_min { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int liked_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string can_use_bundle_deal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_official_shop_label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coin_earn_label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long price_min_before_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cb_option { get; set; }
        /// <summary>
        /// 自有品牌
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_hot_sales { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int stock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int bundle_deal_id { get; set; }
        /// <summary>
        /// 新北市新莊區
        /// </summary>
        public string shop_location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_group_buy_item { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flash_sale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_slash_price_item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ModelsItem> models { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int show_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string add_on_deal_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Item_rating item_rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_official_shop_label_in_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Tier_variationsItem> tier_variations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_adult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_non_cc_installment_payment_eligible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_lowest_price_guarantee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AttributesItem> attributes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string has_group_buy_stock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preview_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int welcome_package_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CategoriesItem> categories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ctime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int condition { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<string> wholesale_tier_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_shopee_verified_label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string show_official_shop_label_in_normal_position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string group_buy_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopee_verified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int badge_icon_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string size_chart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_pre_order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> label_ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> hashtag_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hidden_price_display { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int historical_sold { get; set; }
    }

}
