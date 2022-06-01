#if UNITY_EDITOR
namespace YanYeek
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using UnityEngine;

    public abstract class SOParser
    {
        protected static Regex regex = new Regex("[^0-9:,-]+");
        protected readonly Dictionary<Type, Func<object, object>> actionDIct = new Dictionary<Type, Func<object, object>>()
        {
            {typeof(int), (object data) => Convert.ToInt32(data.ToString()=="无" ? 0 : data)},
            {typeof(float), (object data) => Convert.ToSingle(data)},
            {typeof(string), (object data) => Convert.ToString(data)},
            {typeof(uint), (object data) => Convert.ToUInt32(data)},
            {typeof(bool), (object data) => Convert.ToString(data)=="1"},
            {typeof(Dictionary<int,int>), (object data) =>{
                    var dict = new Dictionary<int, int>();
                    if (data == null) { return dict; }
                    // 将中文字符转换为英文字符，提高容错率
                    string v = Utils.ZhSymbolToEn(data.ToString());
                    var match = regex.Match(v).Value;
                    if (match.Length > 0)
                    {
                        throw new Exception($"转换成字典的字符串包含 {match} 非法字符");
                    }
                    try
                    {
                        var strArr = v.Split(',');
                        foreach (var e in strArr)
                        {
                            var item = e.Split(':');
                            dict.Add(Convert.ToInt32(item[0]), Convert.ToInt32(item[1]));
                        }
                    }
                    catch (System.Exception)
                    {
                        Debug.LogError("字典格式有误，无法解析，应该为 101:1,102:1");
                        throw;
                    }
                    return dict;
                }
            },
        };

        public void Parser(object target, object data, FieldInfo field)
        {
            Type T = field.FieldType;
            if (!actionDIct.ContainsKey(T))
            {
                Debug.LogError($"{T} 的解析委托不存在");
                return;
            }
            var result = actionDIct[T].Invoke(data);
            field.SetValue(target, result);
        }
    }
}
#endif