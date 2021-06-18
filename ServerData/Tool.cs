using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    public class Tool
    {
        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStampSeconds()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        public static long GetTimeStampMilseconds()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        public static DateTime GetUTCDateTime(long stamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(stamp);
        }
        static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string cacheDirectory = "\\cache";
        static public void DownloadImage(String name, String url)
        {

            string cacheFileDir = basePath + cacheDirectory + "\\customer\\" + name + "\\";
            string avatorFileName = (new Uri(url)).Segments.Last();
            string avatorFilePath = cacheFileDir + avatorFileName;
            if (!File.Exists(avatorFilePath))
            {
                try
                {
                    if (!Directory.Exists(cacheFileDir))
                    {
                        Directory.CreateDirectory(cacheFileDir);
                    }
                    using (Stream imgStream = System.Net.WebRequest.Create(url).GetResponse().GetResponseStream())
                    {
                        using (FileStream fs = File.OpenWrite(cacheFileDir + avatorFileName))
                        {
                            int i = 0;
                            byte[] bytes = new byte[1024];
                            while ((i = imgStream.Read(bytes, 0, 1024)) > 0)
                            {
                                fs.Write(bytes, 0, i);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
      
       
    }
}
