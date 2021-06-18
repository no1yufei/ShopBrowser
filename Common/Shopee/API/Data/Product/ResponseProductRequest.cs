using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API.Data.Product
{
    public class ResponseProductRequest<T>
    {
        public int code;//: 0
        public T data;//: {banned_count: 1, unlisted_count: 0, count_for_limit: 5000, product_count_for_limit: 3182,…}
        public string message;//: "success"
        public string user_message;//: "success"

        static public ResponseProductRequest<T> FromJson(string str)
        {
            ResponseProductRequest<T> info = default(ResponseProductRequest<T>);
            try
            {
                info = JsonConvert.DeserializeObject<ResponseProductRequest<T>>(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(typeof(T).GetType().Name+" Json转换:" + ex.Message);
            }
            return info;
        }
    }

    public class JsonConvert<T>
    {
        static public T FromJson(string str)
        {
            T info = default(T);
            try
            {
                info = JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(typeof(T).GetType().Name + " Json转换:" + ex.Message);
            }
            return info;
        }
        static public string ToJson(object value)
        {
            string dataTemplate = null;
            try
            {
                dataTemplate = JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
}
