using UnityEngine;

namespace YanYeek
{
    public class ResMgr
    {
        public static T LoadRes<T>(string path) where T : Object
        {
            // TODO 暂定资源加载方式
            return Resources.Load<T>(path);
        }
        public static T[] LoadAllRes<T>(string path) where T : Object
        {
            // TODO 暂定资源加载方式
            return Resources.LoadAll<T>(path);
        }
    }
}