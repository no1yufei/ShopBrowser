using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class SearchHintData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<KeywordsItem> keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }


        public class KeywordsItem
        {
            /// <summary>
            /// 女生衣著--类目名称
            /// </summary>
            public string cat_display_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string catid { get; set; }
            /// <summary>
            /// 联想的关键词
            /// </summary>
            public string keyword { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int record_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string trackingid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int keyword_source_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int rnum { get; set; }
        }
    }
}
