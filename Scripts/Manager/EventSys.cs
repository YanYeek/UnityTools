using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanYeek
{
    /// <summary>
    /// 静态单例事件中心，用与于解耦event的注册与触发
    /// （如果需要多个参数或者多个返回值可以用Tuple包装）
    /// </summary>
    public static class EventSys
    {
        private interface IEventInfo { }

        private class ActionInfo : IEventInfo
        {
            public Action action = delegate { };
        }

        private class ActionInfo<TArg> : IEventInfo
        {
            public Action<TArg> action = delegate { };
        }

        private class FuncInfo<TResult> : IEventInfo
        {
            public Func<TResult> func = delegate { return default(TResult); };
        }

        private class FuncInfo<TArg, TResult> : IEventInfo
        {
            public Func<TArg, TResult> func = delegate { return default(TResult); };
        }

        /// <summary>
        /// Key:事件名 Value:事件 多对多 但Action或者Func类型必须一致
        /// </summary>
        private static readonly Dictionary<int, IEventInfo> eventDict = new Dictionary<int, IEventInfo>();

        /// <summary>
        /// 注册无参无返Action 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void AddEvent(int id, Action action)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
                eventInfo = new ActionInfo();
                eventDict.Add(id, eventInfo);
            }
            (eventInfo as ActionInfo).action += action;
        }

        /// <summary>
        /// 取消注册无参无返事件 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void RemoveEvent(int id, Action action)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as ActionInfo).action -= action;
        }

        /// <summary>
        /// 调用无参无返事件 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void CallEvent(int id)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as ActionInfo).action.Invoke();
        }

        /// <summary>
        /// 注册有参无返Action 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void AddEvent<TArg>(int id, Action<TArg> action)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
                eventInfo = new ActionInfo<TArg>();
                eventDict.Add(id, eventInfo);
            }
            (eventInfo as ActionInfo<TArg>).action += action;
        }

        /// <summary>
        /// 取消注册有参无返Action 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void RemoveEvent<TArg>(int id, Action<TArg> action)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as ActionInfo<TArg>).action -= action;
        }

        /// <summary>
        /// 调用有参无返Action 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void CallEvent<TArg>(int id, TArg arg)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as ActionInfo<TArg>).action.Invoke(arg);
        }

        /// <summary>
        /// 注册无参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void AddEvent<TResult>(int id, Func<TResult> func)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
                eventInfo = new FuncInfo<TResult>();
                eventDict.Add(id, eventInfo);
            }
            (eventInfo as FuncInfo<TResult>).func += func;
        }

        /// <summary>
        /// 取消注册注册无参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void RemoveEvent<TResult>(int id, Func<TResult> func)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as FuncInfo<TResult>).func -= func;
        }

        /// <summary>
        /// 调用注册无参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static TResult CallEvent<TResult>(int id)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return default(TResult);
            }
            return (eventInfo as FuncInfo<TResult>).func.Invoke();
        }

        /// <summary>
        /// 注册有参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void AddEvent<TArg, TResult>(int id, Func<TArg, TResult> func)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
                eventInfo = new FuncInfo<TArg, TResult>();
                eventDict.Add(id, eventInfo);
            }
            (eventInfo as FuncInfo<TArg, TResult>).func += func;
        }

        /// <summary>
        /// 取消注册注册无参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static void RemoveEvent<TArg, TResult>(int id, Func<TArg, TResult> func)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return;
            }
            (eventInfo as FuncInfo<TArg, TResult>).func -= func;
        }

        /// <summary>
        /// 调用注册无参有返Func 建议用enum强转关联 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行
        /// </summary>
        public static TResult CallEvent<TArg, TResult>(int id, TArg arg)
        {
            eventDict.TryGetValue(id, out IEventInfo eventInfo);
            if (eventInfo == null)
            {
#if UNITY_EDITOR
                Debug.LogError($" {id} 事件不存在 注意可能空指针异常，在Edit>Project Settings>Script Execution Order设置顺序，从小到大依次执行");
#endif
                return default(TResult);
            }
            return (eventInfo as FuncInfo<TArg, TResult>).func.Invoke(arg);
        }
    }
}