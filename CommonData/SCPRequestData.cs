using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData
{
    public class SCPRequestData<T>
    {
        public T Data;
        public String Id = "";
        public DateTime UpdateTime = DateTime.Now;

        public SCPRequestData(T data)
        {
            Data = data;
        }
        public SCPRequestData() { }
        static public SCPRequestData<T> FromJson(String str)
        {
            SCPRequestData<T> ret = default(SCPRequestData<T>);
            try
            {
                ret = JsonConvert.DeserializeObject<SCPRequestData<T>>(str);
            }
            catch (Exception xe)
            {
                Console.WriteLine(typeof(SCPRequestData<T>) + "转换Json失败：" + str+xe.Message);
            }
            return ret;
        }
        public string ToJson()
        {
            string dataTemplate = "";
            try
            {
                dataTemplate = JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(typeof(SCPRequestData<T>) + "转换Json失败：" + ex.Message);
            }
            return dataTemplate;
        }
    }
}
