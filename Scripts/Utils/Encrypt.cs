using System;
using System.Security.Cryptography;
using System.Text;

namespace YanYeek
{
    public partial class Utils
    {
        /// <summary>
        /// 获取RijndaelManaged对象用于AES加密解密
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static RijndaelManaged GetRijndaelManaged(byte[] key)
        {
            RijndaelManaged m = new RijndaelManaged();
            m.Key = key;
            m.Mode = CipherMode.ECB;
            m.Padding = PaddingMode.PKCS7;
            return m;
        }

        /// <summary>
        /// AES加密字符串
        /// key 必须是 16 位长英文字符数组占一位 中文字符占3位
        /// </summary>
        /// <param name="key">key 必须是 16 位长英文字符数组占一位 中文字符占3位</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] key, string data)
        {
            if (key.Length != 16) throw new ArgumentException("key 必须是 16 位长英文字符数组占一位 中文字符占3位");
            var cryptoTransform = GetRijndaelManaged(key).CreateEncryptor();
            var encryptData = Encoding.UTF8.GetBytes(data);
            var result = cryptoTransform.TransformFinalBlock(encryptData, 0, encryptData.Length);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// AES加密字符串
        /// key 必须是 16 位长英文字符数组占一位 中文字符占3位
        /// </summary>
        /// <param name="key">key 必须是 16 位长英文字符数组占一位 中文字符占3位</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string data)
        {
            var _key = Encoding.UTF8.GetBytes(key);
            return Encrypt(_key, data);
        }

        /// <summary>
        /// AES加密字符串
        /// key 必须是 16 位长英文字符数组占一位 中文字符占3位
        /// </summary>
        /// <param name="key">key 必须是 16 位长英文字符数组占一位 中文字符占3位</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decrypt(byte[] key, string data)
        {
            if (key.Length != 16) throw new ArgumentException("key 必须是 16 位长英文字符数组占一位 中文字符占3位");
            var cryptoTransform = GetRijndaelManaged(key).CreateDecryptor();
            var decryptData = Convert.FromBase64String(data);
            var result = cryptoTransform.TransformFinalBlock(decryptData, 0, decryptData.Length);
            return Encoding.UTF8.GetString(result);
        }

        /// <summary>
        /// AES加密字符串
        /// key 必须是 16 位长英文字符数组占一位 中文字符占3位
        /// </summary>
        /// <param name="key">key 必须是 16 位长英文字符数组占一位 中文字符占3位</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string data)
        {
            var _key = Encoding.UTF8.GetBytes(key);
            return Decrypt(_key, data);
        }

    }

}