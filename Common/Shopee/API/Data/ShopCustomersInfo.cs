using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class ShopCustomersInfo
    {
        /// <summary>
        /// 全部買家:在選定的時間內下單的唯一買家的總數
        /// </summary>
        public int buyers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double buyers_change { get; set; }
        /// <summary>
        /// 新買家:在選定的時間內，不重複的新買家總數。如果買方在選定的時間之前的12個月內沒有從您的賣場下過訂單，那麼他就是新買家。
        /// </summary>
        public int new_buyers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double new_buyers_rate { get; set; }
        /// <summary>
        /// 舊買家:在選定的時間內，現有不重複買家的總數。現有買家在指定時間之前的12個月內至少向您的賣場下過一次訂單。
        /// </summary>
        public int existing_buyers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double existing_buyers_rate { get; set; }
        /// <summary>
        /// 潛在買家:在選定的時間內，現有不重複買家的總數。現有買家在指定時間之前的12個月內至少向您的賣場下過一次訂單。
        /// </summary>
        public int potential_buyers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double potential_buyers_change { get; set; }
        /// <summary>
        /// 回購率：訂購超過一次的買家總數，除以在選定時間內訂購的買家總數。
        /// </summary>
        public double repeat_purchase_rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double repeat_purchase_rate_change { get; set; }


        public static ShopCustomersInfo FromJson(String json)
        {
            ShopCustomersInfo customers = new ShopCustomersInfo();
            try
            {
                customers = JsonConvert.DeserializeObject<ShopCustomersInfo>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return customers;
        }
    }
}
