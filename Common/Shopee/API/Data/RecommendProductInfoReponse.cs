using Common.Shopee.API.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class RecommendProductInfoReponse
    {
        //按照json格式，定义数据项，数组和类
        //此处定义数据项
        public string version;//
        public RecommendProductInfoReponseData data;

        //这是每个数据类规定的静态函数，将json转换成这里定义的类
        public static RecommendProductInfoReponse FromJson(String jsonStr)
        {
            //实现是将DataTemplate 换成具体的类名
            RecommendProductInfoReponse dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<RecommendProductInfoReponse>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
    public class RecommendProductInfoReponseData
    {
        public RecommendProductInfo[] items;
        public int total;
        public long update_time;//: 1565793887
    }
}
