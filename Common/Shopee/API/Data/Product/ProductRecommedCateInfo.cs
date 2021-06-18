using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductRecommedCateInfoV2
    {
        public int[][] cats_recommend;

        public static ProductRecommedCateInfoV2 FromJson(string str)
        {
            ProductRecommedCateInfoV2 ret = new ProductRecommedCateInfoV2();
            try
            {
                ret = JsonConvert.DeserializeObject<ProductRecommedCateInfoV2>(str);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ProductRecommedCateInfo:" + ex.Message);
            }
            return ret;
        }
    }
    public class ProductRecommedCateInfo
    {
        public bool IsInvlidCate
        {
            get { return cats != null && cats.Length > 0; }
        }
        //public string msg;//": "success", 
        public int[][] cats;//": [[62, 1483, 9301], [75, 1587, 8111], [62, 1479, 7654]], 
      // public double[] scores;//": [0.9998725652694702,      */                    0.00013322725135367364, 1.8917602574219927e-05], 
        public long ds_cat_rcmd_id;//": 1021572004870943755
    }
}
