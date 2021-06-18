using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductInfoFeatureConfigs
    {
        public ProudctDTS dts;//: {pre_order_min_dts: 7, pre_order_max_dts: 30, in_stock_dts: 2}

        public int list_limit = 1000;
        public string mass_upload_file_path = "Shopee_mass_upload_template_sku_sg.zip";
        public int product_image_limit = 9;//: 9
        public VariationLimit variation_limit = new VariationLimit();//: {one_tier_limit: 20, total_limit: 50}
 
    }
    public class ProudctDTS
    {
        public int pre_order_min_dts;//: 7, 
        public int pre_order_max_dts;//: 30, 
        public int in_stock_dts;//: 2
    }
    public class VariationLimit
    {
        public int one_tier_limit = 20;//: 20
        public int total_limit = 50;//: 50
    }
}
