#if UNITY_EDITOR
namespace YanYeek
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// ScriptableObject 工具
    /// </summary>
    public static class SOUtils
    {


        /// <summary>
        /// 根据itemArray批量创建SO
        /// e:assetPath = "Assets/Resources/ScriptObjects/Effect" 
        /// 不会立刻刷新Refresh  可以结束后添加刷新语句UnityEditor.AssetDatabase.Refresh();
        /// </summary>
        public static void CreateSO<T>(string[] itemArray, string assetPath, Action<T> initDataCB = null) where T : ScriptableObject
        {
            //检查保存路径
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }
            foreach (string name in itemArray)
            {
                //创建数据
                T so = ScriptableObject.CreateInstance<T>();
                // 赋值数据
                initDataCB?.Invoke(so);
                //删除原有文件，生成新文件
                string fullPath = $"{assetPath}/{name}.asset";
                UnityEditor.AssetDatabase.DeleteAsset(fullPath);
                UnityEditor.AssetDatabase.CreateAsset(so, fullPath);
            }

            UnityEditor.AssetDatabase.Refresh();
        }

        /// <summary>
        /// 创建单个SO
        /// e:assetPath = "Assets/Resources/ScriptObjects/Effect" 
        /// 不会立刻刷新Refresh  可以结束后添加刷新语句UnityEditor.AssetDatabase.Refresh();
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assetPath"></param>
        /// <param name="initDataCB"></param>
        /// <typeparam name="T"></typeparam>
        public static void CreateSO<T>(string name, string assetPath, Action<T> initDataCB = null) where T : ScriptableObject
        {
            //检查保存路径
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }

            //创建数据
            T so = ScriptableObject.CreateInstance<T>();
            // 赋值数据
            initDataCB?.Invoke(so);
            //删除原有文件，生成新文件
            string fullPath = $"{assetPath}/{name}.asset";
            UnityEditor.AssetDatabase.DeleteAsset(fullPath);
            UnityEditor.AssetDatabase.CreateAsset(so, fullPath);
        }

        /// <summary>
        /// 创建单个SO T so = ScriptableObject.CreateInstance(typeof(T)); 
        /// e:assetPath = "Assets/Resources/ScriptObjects/Effect" 
        /// 不会立刻刷新Refresh  可以结束后添加刷新语句UnityEditor.AssetDatabase.Refresh();
        /// </summary>
        public static void CreateSO(ScriptableObject so, string name, string assetPath)
        {
            //检查保存路径
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }
            //删除原有文件，生成新文件
            string fullPath = $"{assetPath}/{name}.asset";
            UnityEditor.AssetDatabase.DeleteAsset(fullPath);
            UnityEditor.AssetDatabase.CreateAsset(so, fullPath);
        }

        /// <summary>
        /// 创建单个SO T so = ScriptableObject.CreateInstance(typeof(T)); 
        /// e:assetPath = "Assets/Resources/ScriptObjects/Effect" 
        /// 不会立刻刷新Refresh  可以结束后添加刷新语句UnityEditor.AssetDatabase.Refresh();
        /// </summary>
        public static T CreateSO<T>(string name, string path) where T : ScriptableObject
        {
            //删除原有文件，生成新文件
            string fullPath = $"{path}/{name}.asset";
            var so = ScriptableObject.CreateInstance<T>();
            UnityEditor.AssetDatabase.CreateAsset(so, fullPath);
            return so;
        }

        public static void Rename<T>(string newName, T so) where T : UnityEngine.Object
        {
            var s = AssetDatabase.GetAssetPath(so.GetInstanceID());
            AssetDatabase.RenameAsset(s, newName);
            AssetDatabase.SaveAssets();
        }


    }
}
#endif