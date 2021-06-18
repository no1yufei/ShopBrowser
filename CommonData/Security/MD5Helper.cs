using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonData.Security
{
    public class HashHelper
    {
        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }
        public static string GetSHA256(string myString)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            fromData = System.Text.Encoding.UTF8.GetBytes(byte2String);
    
            SHA256CryptoServiceProvider sHA256 = new SHA256CryptoServiceProvider();
            targetData = sHA256.ComputeHash(fromData);
            byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String += targetData[i].ToString("x2");
                }
            return byte2String;
        }
       
    }
}
