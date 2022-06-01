using System.Collections.Generic;

namespace YanYeek
{
    public static class ListExtension
    {
        /// <summary>
        /// 删除List最后一个元素并返回 为空或者元素个数为0时返回元素类型默认值
        ///  空List需要在函数外判断处理
        /// </summary>
        public static T Pop<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default(T);

            int lastIndex = list.Count - 1;

            var last = list[lastIndex];
            list.RemoveAt(lastIndex);
            return last;
        }

        public static T Random<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            var newList = new List<T>();
            foreach (var e in list)
            {
                newList.Add(e);
            }
            return newList;
        }

        /// <summary>
        /// 方法可向数组的末尾添加一个元素，并返回新的长度。
        /// </summary>
        public static int Push<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list.Count;
        }

        /// <summary>
        /// 方法可向数组的末尾添加多个元素，并返回新的长度。
        /// </summary>
        public static int Push<T>(this List<T> list, List<T> items)
        {
            list.AddRange(items);
            return list.Count;
        }

        /// <summary>
        /// 方法可向数组的开头添加一个元素，并返回新的长度。
        /// </summary>
        public static int UnShift<T>(this List<T> list, T item)
        {
            list.Insert(0, item);
            return list.Count;
        }

        /// <summary>
        /// 方法可向数组的开头添加更多元素，并返回新的长度。
        /// </summary>
        public static int UnShift<T>(this List<T> list, List<T> items)
        {
            list.InsertRange(0, items);
            return list.Count;
        }
    }
}
