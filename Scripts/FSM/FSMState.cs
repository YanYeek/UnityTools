namespace YanYeek
{
    public abstract class FSMState
    {

        /// <summary>
        /// 状态退出
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 状态进入
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// 执行当前状态逻辑
        /// </summary>
        public abstract void Action();

        /// <summary>
        /// 判断是否要切换到其他状态
        /// </summary>
        public abstract void Reason(FSM fsm);
    }
}