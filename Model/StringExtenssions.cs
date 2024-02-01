using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeluxeEdit.Extensions
{
    public static class StringExtenssions
    {
        public static PluginItem CreatePluginItem(this string path, Type item)
        {
            var result = new PluginItem();
            result.MyType = item;
            result.DerivedSourcePath = path;
            result.Id = item.ToString();
            result.Version = item.Assembly.GetName().Version;
            return result;
        }

        public static string SubstringPos(this string s, int startIndex, int lastIndex)
        {
            var result = new StringBuilder();

            for (int i = startIndex; i<= lastIndex && i<s.Length; i++)
            {
                result.Append(s[i]);
            }

              
            return result.ToString();
        } 
          
        public static bool SameFileName(this string a, string b)
        {
            var filea = new FileInfo(a);    
            var fileb = new FileInfo(b);
            var result = String.Equals(filea.Name, fileb.Name, StringComparison.CurrentCultureIgnoreCase);

            return result;
        }
        public static bool IsEmpty(this string? item)
        {
            return String.IsNullOrEmpty(item);
        }
        public static bool HasContent(this string? item)
        {
            return !String.IsNullOrEmpty(item);
        }
        public static int ToInt(this string item)
        {
            return int.Parse(item);
        }
        public static long ToLong(this string item)
        {
            return long.Parse(item);
        }
        public static int? IndexOfDigit(this string item, int pos = 0)
        {
            int? result = null;
            var input = item.Substring(pos);
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public static int? LastIndexOfDigit(this string item, int pos = 0)
        {
            int? result = null;
            var input = item.Substring(pos);
             for (int i = input.Length-1; i > 0 ; i--)
            {
                if (Char.IsDigit(input[i]))
               {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }


    }
}
