using System.Collections;
using UnityEngine;

namespace YanYeek
{
    public class CoroutineMgr
    {
        private MonoBehaviour mono;

        public CoroutineMgr(MonoBehaviour mono)
        {
            this.mono = mono;
        }

        /// <summary>
        /// 统一使用CoroutineMgr开启协程 方便管理
        /// </summary>
        public Coroutine LaunchCoroutine(IEnumerator coroutine)
        {
            return mono.StartCoroutine(coroutine);
        }

        /// <summary>
        /// 统一使用CoroutineMgr关闭协程 方便管理
        /// </summary>
        public void DropCoroutine(IEnumerator coroutine)
        {
            mono.StopCoroutine(coroutine);
        }

        /// <summary>
        /// 统一使用CoroutineMgr关闭协程 方便管理
        /// </summary>
        public void DropCoroutine(Coroutine coroutine)
        {
            mono.StopCoroutine(coroutine);
        }

        /// <summary>
        /// 统一使用CoroutineMgr关闭所有协程 方便管理
        /// </summary>
        public void DropAllCoroutine()
        {
            mono.StopAllCoroutines();
        }
    }
}