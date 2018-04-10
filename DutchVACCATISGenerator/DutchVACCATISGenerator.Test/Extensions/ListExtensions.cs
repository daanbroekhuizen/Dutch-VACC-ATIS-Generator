using System;
using System.Collections.Generic;

namespace DutchVACCATISGenerator.Test.Extensions
{
    public static class ListExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> list, int seed)
        {
            var random = new Random(seed);
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
