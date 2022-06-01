using UnityEngine;

namespace YanYeek
{
    public static class UnityExtension
    {
        /// <summary>
        /// 简化GameObject获取子节点组件扩展
        /// </summary>
        public static T Find<T>(this GameObject gameObject, string path)
        {
            return gameObject.transform.Find(path).GetComponent<T>();
        }

        /// <summary>
        /// 删除GameObject所有的子节点扩展
        /// </summary>
        /// <param name="gameObject"></param>
        public static void DestroyAllChildren(this GameObject gameObject)
        {
            foreach (Transform e in gameObject.transform)
            {
                GameObject.Destroy(e.gameObject);
            }
            /* for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
            } */
        }

        /// <summary>
        /// GO自己销毁扩展
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(this GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}