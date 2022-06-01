using System.Collections.Generic;
using UnityEngine;

namespace YanYeek
{
    public partial class Utils
    {
        public static bool RandomNum(int rand)
        {
            return rand > UnityEngine.Random.Range(0, 100);
        }

        public static List<T> RandomList<T>(List<T> originList)
        {
            int k;
            T temp;

            for (int i = 0; i < originList.Count; i++)
            {
                k = UnityEngine.Random.Range(0, i + 1);
                temp = originList[i];
                originList[i] = originList[k];
                originList[k] = temp;
            }

            return originList;
        }

        /// <summary>
        /// 中文字符转为英文字符
        /// </summary>
        /// <param name="text">转换的中文字符串</param>
        /// <returns></returns>
        public static string ZhSymbolToEn(string text)
        {
            const string ch = "。；，？！、“”‘’（）—【】￥";//中文字符
            const string en = @".;,?!\""""''()-[]$";//英文字符
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int n = ch.IndexOf(c[i]);
                if (n != -1) c[i] = en[n];
            }
            return new string(c);
        }

        /// <summary>
        /// 一个点绕另一个点旋转
        /// </summary>
        /// <param name="point">要旋转的点</param>
        /// <param name="pivot">中心点</param>
        /// <param name="euler">旋转的角度</param>
        /// <returns></returns>
        public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 euler)
        {
            Vector3 direction = point - pivot;
            Vector3 rotatedDirection = Quaternion.Euler(euler) * direction;
            Vector3 rotatedPoint = rotatedDirection + pivot;
            return rotatedPoint;
        }
    }
}