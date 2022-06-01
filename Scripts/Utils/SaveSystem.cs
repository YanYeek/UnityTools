using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

namespace YanYeek
{
    public static class SaveSystem
    {
        #region Json
        /* 
        Application.dataPath 安装目录的文件夹下
        Application.persistentDataPath 各平台默认存数据的路径
         */
        /// <summary>
        /// if (rootPath == "") rootPath = Application.dataPath;
        /// </summary>
        public static void SaveByJson(string saveFileName, object data, string rootPath = "")
        {
            string json = JsonConvert.SerializeObject(data);

            if (rootPath == "") rootPath = Application.dataPath;
            string path = Path.Combine(rootPath, saveFileName);

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                File.WriteAllText(path, json);
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Failed save data to {path}. \n{e}");
#endif
            }
#if UNITY_EDITOR
            Debug.Log($"Successfully saved data to {path}.");
#endif
        }

        /// <summary>
        /// if (rootPath != "") rootPath = Application.dataPath;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveFileName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static T LoadFromJson<T>(string saveFileName, string rootPath = "")
        {
            if (rootPath == "") rootPath = Application.dataPath;
            string path = Path.Combine(rootPath, saveFileName);
            try
            {
                string json = File.ReadAllText(path);
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Failed to load data from {path}. \n{e}");
#endif

                return default(T);
            }
        }

        public static void DeleteSaveFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Failed to delete data from {path}. \n{e}");
#endif
            }
        }

        private static string combinePath(string saveFileName)
        {
            return Path.Combine(Application.dataPath, saveFileName);
        }
        #endregion

        #region 二进制存储
        /// <summary>
        /// 读取二进制文件内容并反序列化为类实例
        /// ！！！=注意1：T的类成员只能由简单数据类型和数组组成，例如Vector3 pos要变成float[] pos=new float[]{pos.x,pos.y,pos.z}      ！！！=注意2：
        /// if (folder == "") path = Path.Combine(Application.persistentDataPath, path);
        /// else path = Path.Combine(folder, path);
        /// ！！！=注意3：当存储的文件夹不存在是会报错
        /// </summary>
        /// <typeparam name="T">T的类成员只能由简单数据类型和数组组成，例如Vector3 pos要变成float[] pos=new float[]{pos.x,pos.y,pos.z}</typeparam>
        public static void SaveByBinary<T>(string path, T data, string folder = "") where T : class
        {
            try
            {
                if (folder == "")
                    path = Path.Combine(Application.persistentDataPath, path);
                else
                    path = Path.Combine(folder, path);
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, data);
                }
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning(e);
                Debug.LogWarning($"路径:{path}加载的文件不存在，检查路径？？？");
#endif
            }

        }
        /// <summary>
        /// 读取二进制文件内容并反序列化为类实例
        /// ！！！=注意1==》：T的类成员只能由简单数据类型和数组组成，例如Vector3 pos要变成float[] pos=new float[]{pos.x,pos.y,pos.z}
        /// 
        /// ！！！=注意2==》：if (folder == "") path = Path.Combine(Application.persistentDataPath, path);
        /// else path = Path.Combine(folder, path);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="folder">默认是 Application.persistentDataPath</param>
        /// <returns>！！！=注意3==》：当路径文件不存在时返回null</returns>
        public static T LoadByBinary<T>(string path, string folder = "") where T : class
        {
            if (folder == "")
                path = Path.Combine(Application.persistentDataPath, path);
            else
                path = Path.Combine(folder, path);

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    T data = formatter.Deserialize(stream) as T;
                    return data;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"路径:{path}加载的文件不存在，检查路径？？？");
#endif
                return null;
            }
        }

        #endregion

    }
}