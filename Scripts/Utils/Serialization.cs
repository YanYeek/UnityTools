using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
    原著：https://kou-yeung.hatenablog.com/entry/2015/12/31/014611
    中文翻译：https://gameinstitute.qq.com/community/detail/128931
    用于解决JsonUtility 不能序列化list Dictionary BitArray 使用类包装
 */

namespace YanYeek
{
    /// <summary>
    /// List<T> 
    /// </summary>
    [Serializable]
    public class Serialization<T>
    {
        [SerializeField]
        List<T> target;
        public List<T> ToList() { return target; }
        public Serialization(List<T> target)
        {
            this.target = target;
        }
    }

    /// <summary>
    /// Dictionary<TKey, TValue>
    /// </summary>
    [Serializable]
    public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        List<TKey> keys;
        [SerializeField]
        List<TValue> values;

        Dictionary<TKey, TValue> target;
        public Dictionary<TKey, TValue> ToDictionary() { return target; }

        public Serialization(Dictionary<TKey, TValue> target)
        {
            this.target = target;
        }

        public TValue this[TKey key]
        {
            get
            {
                return target[key];
            }
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(target.Keys);
            values = new List<TValue>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            var count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (var i = 0; i < count; ++i)
            {
                target.Add(keys[i], values[i]);
            }
        }
    }

    /// <summary>
    /// BitArray
    /// </summary>
    [Serializable]
    public class SerializationBitArray : ISerializationCallbackReceiver
    {
        [SerializeField]
        string flags;

        BitArray target;
        public BitArray ToBitArray() { return target; }

        public SerializationBitArray(BitArray target)
        {
            this.target = target;
        }

        public void OnBeforeSerialize()
        {
            var ss = new System.Text.StringBuilder(target.Length);
            for (var i = 0; i < target.Length; ++i)
            {
                ss.Insert(0, target[i] ? '1' : '0');
            }
            flags = ss.ToString();
        }

        public void OnAfterDeserialize()
        {
            target = new BitArray(flags.Length);
            for (var i = 0; i < flags.Length; ++i)
            {
                target.Set(flags.Length - i - 1, flags[i] == '1');
            }
        }
    }
}