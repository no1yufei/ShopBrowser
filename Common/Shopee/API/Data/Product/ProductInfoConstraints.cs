using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductInfoConstraints
    {
        public int[] category_blacklist;//: []
        public CategoryDts[] category_dts_setting;//: [{dts_max: 30,…}]
        public int description_length_max = 1500;//: 3000
        public int description_length_min = 3;//: 3
        public int image_height_min = 1;//: 1
        public int image_num_min = 1;//: 1
        public int image_width_min = 1;//: 1
        public string price_max= "499999.00";//: "499999.00"
        public string price_min = "1.00";//: "1.00"
        public long stock_max = 9999;//: 999999
        public int stock_min = 1;//: 1
        public string[] title_character_blacklist;//: []
        public int title_length_max = 40;//: 60
public int title_length_min = 10;//: 10
    }
    public class CategoryDts
    {
        public int[] category_id_list;//: [1611, 67, 2580, 1837, 62, 63, 64, 65, 66, 1859, 68, 69, 70, 71, 72, 73, 74, 75, 76, 10076, 100, 1657]
        public int dts_max;//: 30
        public int dts_min;//: 7
    }
}
