using Newtonsoft.Json;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Shopee.API
{
    public class ShopLogDataInfo
    {
        public string username;
        public string password;
        public string device_id = Guid.NewGuid().ToString();

        public ShopLogDataInfo(Store store)
        {
            username = store.UserName;
            password = store.Password;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ShopLogDataWithCaptchaInfo
    {
        public Captcha captcha = new Captcha();
        public string username;
        public string password;
        public ShopLogDataWithCaptchaInfo(Store store)
        {
            username = store.UserName;
            password = store.Password;
        }
        public override string ToString()
        {
            JsonSerializerSettings seting;
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            });
        }

    }
public class Captcha
{
        Guid ikey = Guid.NewGuid();
    public string key { get { return ikey.ToString(""); } }
    public string value;
        public Guid GetGuid()
        {
            return ikey;
        }
}
}
