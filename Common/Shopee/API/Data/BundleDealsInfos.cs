using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    class BundleDealsInfos
    {
    }

    public class BundleDeals
    {
        /// <summary>
        /// 
        /// </summary>
        public Meta meta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Bundle_dealsItem> bundle_deals { get; set; }
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
            public int offset { get; set; }
        }

        public class Bundle_deal_rule
        {
            /// <summary>
            /// 
            /// </summary>
            public int rule_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string discount_value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fix_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int min_amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string discount_percentage { get; set; }
        }

        public class LabelsItem
        {
            /// <summary>
            /// 4 件 9.7折
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string language { get; set; }
        }

        public class Extinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> shopid_list { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<LabelsItem> labels { get; set; }
            /// <summary>
            /// 套装2019
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<long> itemid_list { get; set; }
        }

        public class Bundle_dealsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int comm_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int usage_limit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ctime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rebate_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Bundle_deal_rule bundle_deal_rule { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Extinfo extinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
}
}
}
