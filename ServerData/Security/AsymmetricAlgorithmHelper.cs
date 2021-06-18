using System;
using System.Security.Cryptography;
namespace ShopeeChat
{
    public class AsymmetricAlgorithmHelper<T>
        where T : AsymmetricAlgorithm, new()
    {
        protected static TResult Execute<TResult>(string key, Func<T, TResult> func)
        {
            using (T algorithm = new T())
            {
                algorithm.FromXmlString(key);
                return func(algorithm);
            }
        }
        /// <summary>
        /// 按默认规则生成公钥、私钥
        /// </summary>
        /// <param name="publicKey">公钥（Xml格式）</param>
        /// <param name="privateKey">私钥（Xml格式）</param>
        public static void Create(out string publicKey, out string privateKey)
        {
            //KeyGenerator.CreateAsymmetricAlgorithmKey<T>(out publicKey, out privateKey);
            privateKey = "";
            publicKey = "";
        }
    }
}
    
