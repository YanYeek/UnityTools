using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

namespace YanYeek
{
    public partial class Utils
    {
        /// <summary>
        /// 利用Json序列化和反序列化实现深拷贝
        /// </summary>
        /// <typeparam name="T">深拷贝的类类型</typeparam>
        /// <param name="obj">深拷贝的类对象</param>
        /// <returns></returns>
        public static T DeepCopyByJson<T>(T obj) where T : new()
        {
            // 序列化
            // string json = JsonConvert.SerializeObject(obj);
            // 反序列化
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 深拷贝，通过反射实现
        /// </summary>
        /// <typeparam name="T">深拷贝的类类型</typeparam>
        /// <param name="obj">深拷贝的类对象</param>
        /// <returns></returns>
        public static T DeepCopyByReflect<T>(T obj)
        {
            // 如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;
            // 如果是数组类型
            if (obj.GetType().IsArray)
            {
                var array = obj as Array;
                var newArray = Array.CreateInstance(array.GetType().GetElementType(), array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    newArray.SetValue(DeepCopyByReflect(array.GetValue(i)), i);
                }
                return (T)(object)newArray;
            }
            if (obj.GetType().FullName.IndexOf("System.Collections.Generic.List") == 0)
            {
                var list = Activator.CreateInstance(obj.GetType());
                int count = Convert.ToInt32(obj.GetType().GetProperty("Count").GetValue(obj));
                for (int i = 0; i < count; i++)
                {
                    obj.GetType().GetMethod("Add").Invoke(list, new object[] { DeepCopyByReflect(obj.GetType().GetProperty("Item").GetValue(obj, new object[] { i })) });
                }
                return (T)list;
            }
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj)));
                }
                catch (Exception ex)
                {
#if UNITY_EDITOR
                    Debug.LogError(ex);
#endif
                }
            }
            return (T)retval;
        }

        /// <summary>
        /// 深拷贝，通过反射实现
        /// </summary>
        /// <typeparam name="T">深拷贝的类类型</typeparam>
        /// <param name="obj">深拷贝的类对象</param>
        /// <returns></returns>
        public static List<T> DeepCopyListByReflect<T>(List<T> obj)
        {
            return obj.Select(m => DeepCopyByReflect(m)).ToList();
        }
    }
}