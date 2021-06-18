using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public class ShopFollowerInfo
    {
        //campaign_id: 0
        //cancellation_rate: 0
        //cb_option: 0
        //chat_disabled: false
        //cover: "81204dca948940866742de0e7141b0ab"
        //ctime: 1462805894
        //description: "我們經營手機模型已長達十年，專注於品質，速度，售後服務。↵與大陸及台灣國內上下游廠商皆有相當的默契合作！↵主要客戶在，通訊行，包膜店，週邊設計公司，學生，軍人。↵若是有任何相關問題都歡迎在線諮詢😊↵另外新開了手機分期業務，專為學生，軍人，上班族開發的專案，一樣歡迎線上諮詢唷🤩"
        //disable_make_offer: 1
        //enable_display_unitno: false
        //followed: null
        public int follower_count;//: 1520
        public int following_count;//: 211
                                   //free_shipping_min_total: null
                                   //has_decoration: null
                                   //holiday_mode_on: false
                                   //is_blocking_owner: null
                                   //is_free_shipping: false
                                   //is_semi_inactive: false
                                   //is_shopee_verified: true
                                   //item_count: 45
                                   //last_active_time: 1560434812
                                   //name: "達米先生 手機分期 手機模型"
                                   //place: "新北市中和區"
                                   //portrait: "247ad9dc87562462bacd82cc1d3d259b"
                                   //preparation_time: 58184
                                   //rating_bad: 11
                                   //rating_good: 1563
                                   //rating_normal: 16
                                   //rating_star: 4.90522
                                   //response_rate: "95%"
                                   //response_time: 4571
                                   //shop_covers: [{image_url: "6d2f55209075f8b23358e1fea3946ca7", type: 0, video_url: ""},…]
                                   //shop_location: "新北市中和區"
                                   //shopid: 5510071
                                   //show_low_fulfillment_warning: false
                                   //show_official_shop_label: false
                                   //status: 1
                                   //total_avg_star: 4.90682
                                   //userid: 5511365
                                   //username: "peterdemo"
        public static ShopFollowerInfo FromJson(String jsonStr)
        {
            ShopFollowerInfo storeOrderInfo = null;
            try
            {
                storeOrderInfo = JsonConvert.DeserializeObject<ShopFollowerInfo>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return storeOrderInfo;
        }
    }
}
