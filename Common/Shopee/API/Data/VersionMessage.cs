using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shopee.API.Data
{
    public class VersionMessage<T>
    {
        public T data;
        public int error;//: 0
        public string error_msg;//: null
        public string version;//: "31cbf95f4442d0d21404379c0aaf69f4"

       static  public VersionMessage<T> FromJson(String str)
        {
            VersionMessage<T> ret = default(VersionMessage<T>);
            try
            {
                ret = JsonConvert.DeserializeObject<VersionMessage<T>>(str);

            }
            catch(Exception xe)
            {
                Console.WriteLine(typeof(T) + "转换Json失败：" +str);
            }
            return ret;
        }
    }
}
