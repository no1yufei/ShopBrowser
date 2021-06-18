/******************************************************************
 * 创建人：HTL
 * 创建时间：2015-04-17 17:36:35
 * 说明：C# AES加密解密
 * Email：huangyuan413026@163.com
 *******************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CommonData.Security
{
    public class AES
    {
        /// <summary>
        /// AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string password, string iv)
        {
            password = EncodeBase64(password);
            iv = EncodeBase64(iv);
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            System.Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;


            byte[] ivBytes = new byte[16];
            int ivLen = iv.Length;
            if (ivLen > ivBytes.Length) ivLen = keyBytes.Length;
            System.Array.Copy(System.Text.Encoding.UTF8.GetBytes(iv), ivBytes, ivLen);
            rijndaelCipher.IV = ivBytes;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.UTF8.GetBytes(text);

            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            return Convert.ToBase64String(cipherBytes);

        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv)
        {
            Console.WriteLine("信息加密："+password + "//" + iv);
            password = EncodeBase64(password);
            iv = EncodeBase64(iv);
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] encryptedData = Convert.FromBase64String(text);

            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            System.Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            byte[] ivBytes = new byte[16];
            int ivLen = iv.Length;
            if (ivLen > ivBytes.Length) ivLen = keyBytes.Length;
            System.Array.Copy(System.Text.Encoding.UTF8.GetBytes(iv), ivBytes, ivLen);
            rijndaelCipher.IV = ivBytes;

            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);

        }
        ///编码
        public static string EncodeBase64(string code,string code_type="UTF-8")
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码
        public static string DecodeBase64(string code, string code_type = "UTF-8")
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
    }
}

//    public static void Main()
//    {
//        //密码
//        string password="1234567890123456";
//        //加密初始化向量
//        string iv="                ";     
//        string message=AESEncrypt("abcdefghigklmnopqrstuvwxyz0123456789",password,iv);
//        Console.WriteLine(message);

//        message=AESDecrypt("8Z3dZzqn05FmiuBLowExK0CAbs4TY2GorC2dDPVlsn/tP+VuJGePqIMv1uSaVErr",password,iv);

//        Console.WriteLine(message);
//    }