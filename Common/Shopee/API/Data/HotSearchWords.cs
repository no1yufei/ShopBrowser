using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{

    public class HotSearchWords
    {

        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string error_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int error { get; set; }


        /// <summary>
        /// Request URL: https://xiapi.xiapibuy.com/api/v2/recommendation/hot_search_words?limit=8&offset=0
        /// Request Method: GET
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static HotSearchWords FromJson(String jsonStr)
        {
            //实现是将DataTemplate 换成具体的类名
            HotSearchWords dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<HotSearchWords>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
        public class ItemsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string @from { get; set; }
            /// <summary>
            /// gucci 短夾
            /// </summary>
            public string keyword { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string data_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string intentionid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> images { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public List<ItemsItem> items { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int update_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int total { get; set; }
        }
    }
}
