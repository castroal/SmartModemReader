using System;
using System.Linq;

namespace SmartModemReader
{
    static class Extensions
    {
        public static float ToFloat(this string str)
        {
            var cleanStr = new string(str.Where(c=> char.IsDigit(c) || char.IsPunctuation(c)).ToArray());

            return Convert.ToSingle(cleanStr);
        }

        public static float[] ToFloatArray(this string str)
        {
            var splitStr = str.Split(',');
            var values = new float[splitStr.Length];
            for (int i = 0; i < splitStr.Length; i++)
            {
                values[i] = splitStr[i].ToFloat();
            }
            return values;
        }
    }
}