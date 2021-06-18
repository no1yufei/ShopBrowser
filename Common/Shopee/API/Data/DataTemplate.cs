using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data
{
    public class DataTemplate
    {
        //按照json格式，定义数据项，数组和类
        //此处定义数据项



            //这是每个数据类规定的静态函数，将json转换成这里定义的类
        public static DataTemplate FromJson(String jsonStr)
        {
            //实现是将DataTemplate 换成具体的类名
            DataTemplate dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<DataTemplate>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
}
