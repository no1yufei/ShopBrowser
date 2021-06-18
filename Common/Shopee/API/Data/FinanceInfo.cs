using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    class FinanceInfo
    {
    }


    public class IncomeMeta
    {

        public static IncomeMeta FromJson(String json)
        {
            IncomeMeta im = new IncomeMeta();
            try
            {
                im = JsonConvert.DeserializeObject<IncomeMeta>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return im;
        }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_message { get; set; }
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public double available { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double frozen { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double last_month_income { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double last_week_income { get; set; }
        }

    }
}
