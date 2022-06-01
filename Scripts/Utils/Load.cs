using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace YanYeek
{
    public partial class Utils
    {
        public static T LoadJsonFromFile<T>(string path) where T : class
        {
            if (!File.Exists(Application.dataPath + path))
            {
#if UNITY_EDITOR
                Debug.LogError("Don't Find");
#endif
                return null;
            }

            StreamReader sr = new StreamReader(Application.dataPath + path);
            if (sr == null)
            {
                return null;
            }
            string json = sr.ReadToEnd();

            if (json.Length > 0)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return null;
        }

        /// <summary>
        /// 加载Resources目录下的文件，通过JsonUtility反序列化为初始化的实例
        /// path以Resources为根路径 例如："configs/UIPanel"
        /// </summary>
        /// <returns></returns>
        public static T LoadJson<T>(string path) where T : class
        {
            TextAsset json = Resources.Load<TextAsset>(path);
            return JsonConvert.DeserializeObject<T>(json.text);
        }

        /// <summary>
        /// "C:\a" + "b" = "C:\a\b"
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns>"C:\a\b"</returns>
        public static string CombinePath(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public static void WriteEncryptJson(string fileName, object obj, string key)
        {
            var json = JsonConvert.SerializeObject(obj);
            json = Encrypt(key, json);
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(json);
                }
            }
        }

        public static T ReadEncryptJson<T>(string fileName, string key) where T : new()
        {

            T result = default(T);
            if (File.Exists(fileName))
            {
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        string value = Decrypt(key, reader.ReadString());
                        result = JsonConvert.DeserializeObject<T>(value);
                    }
                }
            }
            return result;
        }
    }
}