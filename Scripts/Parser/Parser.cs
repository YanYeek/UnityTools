using System;
using System.Collections.Generic;
using UnityEngine;

namespace YanYeek
{

    public static class Parser
    {
        private static Dictionary<Type, Action<string>> _parserDict = new Dictionary<Type, Action<string>>();

        public static void AddParser<T>(Action<string> parser)
        {
#if UNITY_EDITOR
            if (_parserDict.ContainsKey(typeof(T)))
            {
                Debug.LogError("zh:解析器重复添加 en:Parsers are added repeatedly");
                return;
            }
#endif
            _parserDict.Add(typeof(T), parser);
        }
    }
}