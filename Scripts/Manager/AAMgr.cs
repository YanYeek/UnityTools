/* using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace YanYeek
{
    public static class AAMgr 
    {
        /// <summary>
        /// 可寻址资源 信息
        /// </summary>
        public class ResHandleInfo
        {
            /// <summary>
            /// 记录 异步操作句柄
            /// </summary>
            public AsyncOperationHandle handle;
            /// <summary>
            /// 记录 引用计数
            /// </summary>
            public uint count = 1;
            public ResHandleInfo(AsyncOperationHandle handle)
            {
                this.handle = handle;
            }
        }
        public static readonly Dictionary<string, ResHandleInfo> resDict = new Dictionary<string, ResHandleInfo>();
        public static AsyncOperationHandle<T> LoadResAsync<T>(string path, Action<AsyncOperationHandle<T>> callBack)
        {
            AsyncOperationHandle<T> handle;
            // 如果TryGetValue返回true就是已存在这个handle
            if (resDict.TryGetValue(path, out ResHandleInfo info))
            {
                handle = info.handle.Convert<T>();
                info.count += 1;
                //判断 这个异步加载是否结束
                if (handle.IsDone)
                {
                    //如果成功 就不需要异步了 直接相当于同步调用了 这个委托函数 传入对应的返回值
                    if (callBack != null)
                    {
                        callBack(handle);
                    }
                }
                //还没有加载完成
                else
                {
                    //如果这个时候 还没有异步加载完成 那么我们只需要 告诉它 完成时做什么就行了
                    handle.Completed += (obj) =>
                    {
                        if (obj.Status == AsyncOperationStatus.Succeeded)
                        {
                            if (callBack != null)
                            {
                                callBack(handle);
                            }
                        }
                    };
                }
            }
            else
            {
                //如果没有加载过该资源 直接进行异步加载 并且记录
                handle = Addressables.LoadAssetAsync<T>(path);
                handle.Completed += (obj) =>
                    {
                        if (obj.Status == AsyncOperationStatus.Succeeded)
                        {
                            if (callBack != null)
                            {
                                callBack(handle);
                            }
                        }
                    };
                info = new ResHandleInfo(handle);
                resDict.Add(path, info);
            }
            return handle;
        }

        public static void Release<T>(string path)
        {
            if (resDict.TryGetValue(path, out ResHandleInfo info))
            {
                //释放时 引用计数-1
                info.count -= 1;
                //如果引用计数为0  才真正的释放
                if (info.count == 0)
                {
                    //取出对象 移除资源 并且从字典里面移除
                    var handle = info.handle.Convert<T>();
                    Addressables.Release(handle);
                    resDict.Remove(path);
                }
            }
        }

        /// <summary>
        /// 清空资源
        /// </summary>
        public static void Clear()
        {
            foreach (var item in resDict.Values)
            {
                Addressables.Release(item.handle);
            }
            resDict.Clear();
            AssetBundle.UnloadAllAssetBundles(true);
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }

} */