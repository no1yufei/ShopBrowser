using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{

    public class ShopOrderLogistic
    {
        /// <summary>
        /// 
        /// </summary>
        public string driver_phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string driver_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tracking_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Tracking_infoItem> tracking_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forder_id { get; set; }

        public class Tracking_infoItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ctime { get; set; }
            /// <summary>
            /// [深圳国际转运中心]已發出
            /// </summary>
            public string description { get; set; }
        }
    }

    /// <summary>
    /// 物流订单号，发货状态，时间等。
    /// </summary>
    public class ShopOrderLogisticslogsStatus
    {
        //   actual_deliver_time: 0
        //actual_pickup_time: 0
        public long channel_id;//: 28016
                               //channel_status: "DUMMY_FORDER_CREATED"
        public string consignment_no;//: "MY1931341400938"
                                     //ctime: 1574347840
                                     //deliver_time: 0
                                     //extra_data: "{"remark":"","cod":false,"waybill_params":{"first_mile_name":"SKYWIN","last_mile_name":"SPX","declare_goods":0,"item_declare_names":{"2825717570":"Women's Clothing-Others"},"service_code":"M03","lane_code":"S-MY06","warehouse_id":"TWS01","warehouse_address":"1/F, Building 8, Tangtou industrial District, Shiyan street, baoan district"},"sls_info":{"first_mile_name":"MYF3","last_mile_name":"MYL4","declare_goods":0,"item_declare_names":{"2825717570":"Women's Clothing-Others"},"service_code":"M03","lane_code":"S-MY06","warehouse_id":"TWS01","warehouse_address":"1/F, Building 8, Tangtou industrial District, Shiyan street, baoan district"},"sort_code":"","origin_code":"","destination_code":"","extra_flag":0,"channelid":28016,"sku_special":false,"discount_shipping_fees":null}"
        public string forder_id;//: "488306128936896588"
                                //logid: 1
                                //logistical_flag: 536870922
                                //mtime: 1574347840
        public long order_id;//: 2182044579
                             //pickup_attempts: 0
                             //pickup_cutoff_time: 2147483647
                             //pickup_time: 0
                             //pickup_timeslot: ""
                             //ref1: ""
                             //shipping_proof: ""
                             //shipping_proof_status: 0
                             //status: 3
                             //thirdparty_tracking_number: "MY1931341400938"
                             //type: 0
        public long user_id;//: 176119092
    }

}
