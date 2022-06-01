using UnityEngine;

namespace YanYeek
{
    /// <summary>
    /// UnityEngine在Awake生命周期赋值的单例 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
    /// </summary>
    public class UnityAwakeSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance => _instance;
        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }
}