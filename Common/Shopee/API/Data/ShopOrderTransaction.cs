using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class ShopOrderTransaction
    {


        public static ShopOrderTransaction FromJson(String jsonStr)
        {
            ShopOrderTransaction orderTransaction = null;
            try
            {
                orderTransaction = JsonConvert.DeserializeObject<ShopOrderTransaction>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderTransaction;
        }

        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_message { get; set; }

        public class Shipping_subtotal
        {
            /// <summary>
            /// 
            /// </summary>
            public string shipping_fee_discount_3pl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shipping_fee_paid_by_buyer { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shipping_fee_paid_by_shopee_on_your_behalf { get; set; }
        }

        public class Rebate_and_voucher
        {
            /// <summary>
            /// 
            /// </summary>
            public string product_discount_rebate_from_shopee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string voucher_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string seller_absorbed_coin_cash_back { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string estimated_shipping_rebate_from_shopee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shipping_rebate_from_shopee { get; set; }
        }

        public class Fees_and_charges
        {
            /// <summary>
            /// 
            /// </summary>
            public string commission_fee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string credit_card_charge { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string transaction_fee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string service_fee { get; set; }
        }

        public class Merchant_subtotal
        {
            /// <summary>
            /// 
            /// </summary>
            public string product_price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refund_amount { get; set; }
        }

        public class Payment_info
        {
            /// <summary>
            /// 
            /// </summary>
            public Shipping_subtotal shipping_subtotal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Rebate_and_voucher rebate_and_voucher { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Fees_and_charges fees_and_charges { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Merchant_subtotal merchant_subtotal { get; set; }
        }

        public class Buyer_payment_info
        {
            /// <summary>
            /// 
            /// </summary>
            public string shopee_voucher { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string credit_card_promotion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string seller_voucher { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopee_coins_redeemed { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string import_tax { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shipping_fee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string merchant_subtotal { get; set; }
        }

        public class Order_logsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string new_status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shop_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mtime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string release_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string old_status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ctime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Payment_info payment_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string order_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Buyer_payment_info buyer_payment_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string seller_absorbed_discount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string source { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string release_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string price_before_discount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Order_logsItem> order_logs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string using_wallet { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string rebate_price { get; set; }
        }
    }
}
