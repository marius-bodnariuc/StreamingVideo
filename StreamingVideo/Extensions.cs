using System;
using System.Collections.Generic;
using System.IO;

namespace StreamingVideo
{
    public static class Extensions
    {
        public static string[] TokenizeNextLine(this StreamReader reader)
        {
            var line = reader.ReadLine();
            var tokens = line.Split(new char[] { ' ' });

            return tokens;
        }

        public static void Times(this int n, Action action)
        {
            for (var i=0; i<n; i++)
            {
                action.Invoke();
            }
        }

        public static void TimesWithIndex(this int n, Action<int> action)
        {
            for (var i=0; i<n; i++)
            {
                action.Invoke(i);
            }
        }
    }
}
