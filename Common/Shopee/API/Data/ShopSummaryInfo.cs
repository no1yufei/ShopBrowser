using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee
{
    public class ShopSummaryInfo
    {
        //頁面瀏覽數:在統計時間區間內，透過電腦網站，手機瀏覽器和APP來瀏覽你賣場內商品的總次數
        public SummaryData product_pv;
        //不重複訪客數:在統計時間區間內，同一用戶透過同一個瀏覽器不論瀏覽多少次您的賣場網頁，都只會被系統紀錄為一筆不重複訪客
        public SummaryData product_uv;
        public SummaryData paid_gmv;
        //銷售額:在統計時間區間內，買家下單的商品銷售額，包含全部已付款和未付款的訂單
        public SummaryData place_gmv;
        //訂單:在統計時間區間內，買家下單的訂單數。包含取消和退貨的訂單
        public SummaryData place_orders;
        public SummaryData paid_orders;
        public SummaryData paid_sales_per_order;
        //平均訂單金額:在統計時間區間內，買家付款總金額除以訂單數
        public SummaryData place_sales_per_order;
        public SummaryData uv_to_paid_order_rate;
        //下單轉換率:在所選的期間內，下單買家數除以不重複訪客數
        public SummaryData uv_to_place_order_rate;
        public SummaryData uv_to_paid_buyers_rate;
        public SummaryData uv_to_placed_buyers_rate;
        public SummaryData paid_buyers;
        public SummaryData placed_buyers;

        public static ShopSummaryInfo FromJson(String json)
        {
            ShopSummaryInfo customers = new ShopSummaryInfo();
            try
            {
                customers = JsonConvert.DeserializeObject<ShopSummaryInfo>(json);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return null;
            }
            return customers;
        }
    }
    public class SummaryData
{
        public float value;
        public float chain_ratio;
        public TimePointData[] points;
    }
    public class TimePointData
    {
        public int timestamp;
        public float value;
    }
}
