using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData
{
    public class SCPResponseData<T>
    {
        public int code;//: 200
        public T data;//: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImRsdCIsInVzZXJJbmRleCI6IjI1Iiwid2hJbmRleCI6IjAiLCJndWlkIjpbImY1ZDQ4MTQ4LTlmN2YtNDQ4YS04NWE3LTdkZmQ3MGJkYjFmNiIsImY1ZDQ4MTQ4LTlmN2YtNDQ4YS04NWE3LTdkZmQ3MGJkYjFmNiJdLCJhdmF0YXIiOiIiLCJkaXNwbGF5TmFtZSI6IuW6l-iBiumAmui0puWPtyIsImxvZ2luTmFtZSI6ImRsdCIsImVtYWlsQWRkcmVzcyI6IiIsInVzZXJUeXBlIjoiMiIsIm5iZiI6MTU5MjA0MjE2MiwiZXhwIjoxNTkyNjQ2OTYyLCJpYXQiOjE1OTIwNDIxNjJ9.YmZiFvIpxQIkgXpGGOVxJWpqZXX5nLB-eODfMGS7yps"
        public string message;//: "操作成功"
       
        public bool success { get { return code == 200; } }
        static public SCPResponseData<T> FromJson(string jsonStr)
        {
            SCPResponseData<T> dataTemplate = default(SCPResponseData<T>);
            try
            {
                dataTemplate = JsonConvert.DeserializeObject<SCPResponseData<T>>(jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTemplate;
        }
        static public string ToJson(SCPResponseData<T> obj)
        {
            string dataTemplate = "";
            try
            {
                dataTemplate = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTemplate;
        }
    }
}
