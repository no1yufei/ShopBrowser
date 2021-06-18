using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data.Product
{
    public class ProductCreateResult
    {
      public  MessageWithCode<ProductCreateID>[] result;
    }
    public class ProductCreateID
    {
        public long product_id;
    }
    //{
    //"message": "success", 
    //"code": 0, 
    //"data": {
    //"result": [
    //{"message": "success", "code": 0, "data": {"product_id": 6129259012}, "user_message": "success"}
    //]
    //}, "user_message": "success"}
}
