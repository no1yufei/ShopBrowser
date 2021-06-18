using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.Tools
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
        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStampSeconds(DateTime time)
        {
            TimeSpan ts = time - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        public static long GetUTCTimeStampSeconds(DateTime time)
        {
            TimeSpan ts = time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
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

        public static string GetGBKHexString(string str,char splitchat =' ')
        {
            //汉字转成GBK十六进制码：
            string hz = str;
            byte[] gbk = Encoding.GetEncoding("GBK").GetBytes(hz);
            string s1 = ""; string s1d = "";
            foreach (byte b in gbk)
            {
                s1 += splitchat +string.Format("{0:X2}", b);
            }
            return s1.TrimEnd(splitchat);
        }
        static public string DownloadImage(String name, String url)
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
                    return null;
                }
            }
            return avatorFilePath;
        }
        static public void ClearImage(String name)
        {

            string cacheFileDir = basePath + cacheDirectory + "\\customer\\" + name + "\\";
         
            if (Directory.Exists(cacheFileDir))
            {
                
                try
                {
                    Directory.Delete(cacheFileDir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("清除图片文件夹失败："+ex.Message);
                }
            }
        }
        static public void SaveSerializationToFile<T>(string file, T obj)
        {
            try
            {
                string filePath = file;
                using (FileStream fsstream = File.Open(filePath, FileMode.Create))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    xs.Serialize(fsstream, obj);
                }
                Console.WriteLine("配置文件已经写入：" + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加载本地配置：" + ex.Message);
            }
        }
        static public T LoadSerializationFromFile<T>(string file)
        {
            T ret = default(T);
            try
            {
                string filePath = file;
                using (FileStream fs = File.OpenRead(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    ret = (T)xs.Deserialize(fs);
                }
                Console.WriteLine("序列化文件已经载入成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("序列化载入错误:" + ex.Message);
            }
            return ret;
        }
        static public Image getPicture(String name, String url)
        {
            string cacheFileDir = basePath + cacheDirectory + "\\customer\\" + name + "\\";
            string avatorFileName = (new Uri(url)).Segments.Last();
            string avatorFilePath = cacheFileDir + avatorFileName;
            System.Drawing.Image result = null;
            if (File.Exists(avatorFilePath))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(avatorFilePath, System.IO.FileMode.Open))
                {
                    result = System.Drawing.Image.FromStream(fs);
                }
                return result;
            }
            return result;
        }
    }
}
