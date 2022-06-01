using UnityEngine;

namespace YanYeek {


    /// <summary>
    /// 面板功能基类 继承于MonoBehaviour，需要挂载到UI面板上
    /// </summary>
    public class BasePanel : MonoBehaviour {

        /// <summary>
        /// 面板打开时的生命周期函数
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// 面板关闭时的生命周期函数
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 面板暂停时的生命周期函数
        /// </summary>
        public virtual void OnPause() { }

        /// <summary>
        /// 面板恢复时的生命周期函数
        /// </summary>
        public virtual void OnResume() { }
    }
}