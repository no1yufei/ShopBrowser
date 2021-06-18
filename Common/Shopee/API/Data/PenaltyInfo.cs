using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class OngoingPenalty
    {
        public static OngoingPenalty FromJson(String json)
        {
            OngoingPenalty op = new OngoingPenalty();
            try
            {
                op = JsonConvert.DeserializeObject<OngoingPenalty>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return op;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Ongoing_penaltyItem> ongoing_penalty { get; set; }

        public class Ongoing_penaltyItem
        {
            /// <summary>
            /// 
            /// </summary>
            public long start_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int punishment_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long end_time { get; set; }
            /// <summary>
            /// 商品不显示在类别搜索
            /// </summary>
            public string penalty { get; set; }
        }
    }
    public class PenaltyHistory
    {
        public static PenaltyHistory FromJson(String json)
        {
            PenaltyHistory ph = new PenaltyHistory();
            try
            {
                ph = JsonConvert.DeserializeObject<PenaltyHistory>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return ph;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Penalty_historyItem> penalty_history { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }

        public class Penalty_historyItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int punishment_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long start_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long end_time { get; set; }
            /// <summary>
            /// 商品不显示在类别搜索
            /// </summary>
            public string penalty { get; set; }
        }

        public class Meta
        {
            /// <summary>
            /// 
            /// </summary>
            public int offset { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int limit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int total { get; set; }
        }
    }
    public class PenaltyPoints
    {
        public static PenaltyPoints FromJson(String json)
        {
            PenaltyPoints pp = new PenaltyPoints();
            try
            {
                pp = JsonConvert.DeserializeObject<PenaltyPoints>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return pp;
        }
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Penalty_pointsItem> penalty_points { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Transify_infoItem> transify_info { get; set; }

        public class Meta
        {
            /// <summary>
            /// 
            /// </summary>
            public int total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int limit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int sum_points { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int offset { get; set; }
        }

        public class Penalty_pointsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int offence_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int penalty_point { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 订单表现
            /// </summary>
            public string violation_type { get; set; }
            /// <summary>
            /// 订单未完成率过高
            /// </summary>
            public string violation_reason { get; set; }
            /// <summary>
            /// 您有过多取消 / 退货订单。为了改善您的订单未完成率，请尽量避免缺货，提早出货，并妥善包装您的商品。
            /// </summary>
            public string explanation { get; set; }
        }

        public class Transify_contentItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string language { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string value { get; set; }
        }

        public class Transify_infoItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int offence_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Transify_contentItem> transify_content { get; set; }
        }
    }
    public class PenaltyScoreList
    {
        public static PenaltyScoreList FromJson(String json)
        {
            PenaltyScoreList psl = new PenaltyScoreList();
            try
            {
                psl = JsonConvert.DeserializeObject<PenaltyScoreList>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return psl;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Scoring_listItem> scoring_list { get; set; }

        public class Scoring_listItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int penalty_score { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ctime { get; set; }
        }
    }
    public class PenaltyOrderDetail
    {
        public static PenaltyOrderDetail FromJson(String json)
        {
            PenaltyOrderDetail pod = new PenaltyOrderDetail();
            try
            {
                pod = JsonConvert.DeserializeObject<PenaltyOrderDetail>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return pod;
        }
        /// <summary>
        /// 
        /// </summary>
        public string reference_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int violation_type { get; set; }
        /// <summary>
        /// 您有过多取消 / 退货订单。为了改善您的订单未完成率，
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PenaltyOrderDetailItem> penalty_detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Violation_info violation_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }

        public class Order_detail
        {
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int late_by_days { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string total_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status_ext { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int order_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int cancel_userid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int payment_method { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ship_on_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int cancel_reason_ext { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long days_to_ship { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long buyerid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string buyer { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long create_time { get; set; }
            /// <summary>
            /// 蝦皮店配-全家
            /// </summary>
            public string shipped_by { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long pay_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ordersn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int cancel_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shipping_confirm_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int shipping_deadline { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int shipping_method { get; set; }

            public long cancellation_date { get; set; }
        }

        public class Return_detail
        {
            /// <summary>
            /// 
            /// </summary>
            public string return_sn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long refundpaid_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string text_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long requested_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long accepted_time { get; set; }
        }

        public class PenaltyOrderDetailItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long orderid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int order_ship_by_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long order_pickup_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Order_detail order_detail { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Return_detail return_detail { get; set; }
        }

        public class Violation_info
        {
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int offence_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int source_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int penalty_point { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
        }

        public class Meta
        {
            /// <summary>
            /// 
            /// </summary>
            public int total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int offset { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int limit { get; set; }
        }
    }
    public class PenaltyProductDetail
    {
        public static PenaltyProductDetail FromJson(String json)
        {
            PenaltyProductDetail ppd = new PenaltyProductDetail();
            try
            {
                ppd = JsonConvert.DeserializeObject<PenaltyProductDetail>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return ppd;
        }
        /// <summary>
        /// 
        /// </summary>
        public string reference_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int violation_type { get; set; }
        /// <summary>
        /// 您的商品违反了上架规则 - 违禁品。请参阅Shopee商品上架说明以了解更多关于违禁品的资讯。
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PenaltyProductDetailItem> penalty_detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Violation_info violation_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }

        public class Product
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> images { get; set; }
            /// <summary>
            /// 圣誕黑紅格子冰箱手柄套 布藝冰箱手柄套 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long date_deleted { get; set; }
            /// <summary>
            /// 包裝 : opp袋 
            /// </summary>
            public string description { get; set; }
        }

        public class PenaltyProductDetailItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Product product { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int violation_reason { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long itemid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int audit_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long mtime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string country { get; set; }
        }

        public class Violation_info
        {
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int offence_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int source_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int penalty_point { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
        }

        public class Meta
        {
            /// <summary>
            /// 
            /// </summary>
            public int total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int offset { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int limit { get; set; }
        }
    }
}
