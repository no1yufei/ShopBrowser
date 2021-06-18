using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class ProductShowInfo
    {
        public static ProductShowInfo FromJson(String json)
        {
            ProductShowInfo productShowInfo = new ProductShowInfo();
            try
            {
                productShowInfo = JsonConvert.DeserializeObject<ProductShowInfo>(json);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return productShowInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ShopsItem> shops { get; set; }

        public class DetailsItem
        {
            /// <summary>
            /// 产品id
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 訪客數
            /// </summary>
            public int uv { get; set; }
            /// <summary>
            /// 商品頁面瀏覽數
            /// </summary>
            public int pv { get; set; }
            /// <summary>
            /// 商品按讚數
            /// </summary>
            public int likes { get; set; }
            /// <summary>
            /// 商品頁面跳出率
            /// </summary>
            public double bounce_rate { get; set; }
            /// <summary>
            /// 加入購物車(件數)
            /// </summary>
            public int add_to_cart_units { get; set; }
            /// <summary>
            /// 加入購物車(人數)
            /// </summary>
            public int add_to_cart_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double placed_sales { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int placed_units { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int placed_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double paid_sales { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int paid_units { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int paid_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double placed_to_paid_buyers_rate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double paid_sales_per_buyers { get; set; }
            /// <summary>
            /// 加入購物車轉換率
            /// </summary>
            public double uv_to_add_to_cart_rate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int placed_order_per_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int paid_order_per_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double uv_to_placed_buyers_rate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double uv_to_paid_buyers_rate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double confirmed_sales { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int confirmed_units { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int confirmed_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int confirmed_order_per_buyers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double uv_to_confirmed_buyers_rate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double placed_to_confirmed_buyers_rate { get; set; }
        }

        public class Items
        {
            /// <summary>
            /// 
            /// </summary>
            public string total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DetailsItem> details { get; set; }
        }

        public class ShopsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Items items { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int total_items { get; set; }
        }
    }
}
