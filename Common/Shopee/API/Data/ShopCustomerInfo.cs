using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class ShopCustomerInfo
    {
        public string id;//:"230875266985134735"
        public DateTime last_message_time;//"2018-11-16T08:31:55+07:00"
        public String last_read_message_id;//"1240515291542454425"
        public TextMessage latest_message_content;//{text: "Honey. This one is available, if you like, you can place an order directly."}
        public string latest_message_id;//"1240515291542454425"
        public string latest_message_type;//"text"
        public long next_timestamp;//1536736332
        public long shop_id;//53753447
        public string status;//null
        public string to_avatar;//"https://cf.shopee.co.id/file/6ce4899ef1dd435a43bfd735141151f4"
        public long to_id;//68323983
        public string to_name;//"putrisuryo27"
        public long unread_count;//0

        public static ShopCustomerInfo FromJson(String json)
        {
           return JsonConvert.DeserializeObject<ShopCustomerInfo>(json);
        }
        public static List<ShopCustomerInfo> ListFromJson(String json)
        {
            List<ShopCustomerInfo> customers = new List<ShopCustomerInfo>();
            try
            {
                customers =JsonConvert.DeserializeObject<List<ShopCustomerInfo>>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
        public override string ToString()
        {
            return to_name;
        }
    }
    public class TextMessage
    {
        public string text="";
    }
}
