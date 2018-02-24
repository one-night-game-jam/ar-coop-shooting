using System;
using System.Collections.Generic;

public static class IEnumeratorExtensions
{
    private static readonly Random rand = new Random();

    public static T Sample<T>(this IEnumerable<T> arg) {
        T selected = default(T);
        var n = 0;
        foreach (var item in arg) {
            n += 1;
            if (rand.Next(n) == 0) {
                selected = item;
            }
        }
        return selected;
    }
}
