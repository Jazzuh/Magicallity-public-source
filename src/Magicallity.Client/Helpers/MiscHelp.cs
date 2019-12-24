using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Drawing;
using CitizenFX.Core;
using System.Collections;

namespace Magicallity.Client.Helpers
{
    /*public static class MiscHelp
    {
        /// <summary>
        /// Changes e.g. BANANA to Banana 
        /// useful for e.g. vehicle model names which are returned in caps
        /// from default native
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string str)
        {
            var tokens = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }

            return string.Join(" ", tokens);
        }

        public static T Clamp<T>(T value, T min, T max)
        where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }

        /*public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }

        public static PointF Add(this PointF c1, PointF c2)
        {
            return new PointF(c1.X + c2.X, c1.Y + c2.Y);
        }

        public static PointF Subtract(this PointF c1, PointF c2)
        {
            return new PointF(c1.X - c2.X, c1.Y - c2.Y);
        }

        public static List<T> Slice<T>(this List<T> list, int start, int end)
        {
            return list.Skip(start).Take(end - start + 1).ToList();
        }

        public static bool IsBetween<T>(this T value, T start, T end) where T : IComparable
        {
            return value.CompareTo(start) >= 0 && value.CompareTo(end) <= 0;
        }

        public static Color ToColor(this string color)
        {
            try
            {
                return Color.FromArgb(int.Parse(color.Replace("#", ""),
                             System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            catch (Exception e)
            {
                //Log.Error($"ToColor exception: {e}");
            }
            return Color.FromArgb(255, 255, 255, 255);
        }

        public static float[] ToArray(this Vector3 vector)
        {
            try
            {
                return new float[] { vector.X, vector.Y, vector.Z };
            }
            catch (Exception e)
            {
                //Log.Error($"ToArray exception: {e}");
            }
            return null;
        }

        public static Vector3 ToVector3(this float[] xyzArray)
        {
            try
            {
                return new Vector3(xyzArray[0], xyzArray[1], xyzArray[2]);
            }
            catch (Exception e)
            {
                //Log.Error($"ToVector3 exception: {e}");
            }
            return Vector3.Zero;
        }

        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// </summary>
        /*public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            // go through the items in the dictionary and copy over the key value pairs)
            foreach (var kvp in dictionary)
            {
                // if the value can also be turned into an ExpandoObject, then do it!
                if (kvp.Value is IDictionary<string, object>)
                {
                    var expandoValue = ((IDictionary<string, object>)kvp.Value).ToExpando();
                    expandoDic.Add(kvp.Key, expandoValue);
                }
                else if (kvp.Value is ICollection)
                {
                    // iterate through the collection and convert any strin-object dictionaries
                    // along the way into expando objects
                    var itemList = new List<object>();
                    foreach (var item in (ICollection)kvp.Value)
                    {
                        if (item is IDictionary<string, object>)
                        {
                            var expandoItem = ((IDictionary<string, object>)item).ToExpando();
                            itemList.Add(expandoItem);
                        }
                        else
                        {
                            itemList.Add(item);
                        }
                    }

                    expandoDic.Add(kvp.Key, itemList);
                }
                else
                {
                    expandoDic.Add(kvp);
                }
            }

            return expando;
        }

        public static void Print(this ExpandoObject dynamicObject)
        {
            var dynamicDictionary = dynamicObject as IDictionary<string, object>;

            foreach (KeyValuePair<string, object> property in dynamicDictionary)
            {
                CitizenFX.Core.Debug.WriteLine("{0}: {1}", property.Key, property.Value.ToString());
            }
            CitizenFX.Core.Debug.WriteLine();
        }
    }*/
}
