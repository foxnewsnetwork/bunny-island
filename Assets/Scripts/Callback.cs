using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate bool ConditionalCallback2<T>(T prev, T next);
public class Callback
{
    public static Action<T> DebounceWhen<T>(Action<T> callback, ConditionalCallback2<T> checker)
    {
        bool hasPrev = false;
        T prev = default(T);
        return (T x) =>
        {
            if (!hasPrev || checker(prev, x))
            {
                callback(x);
            }
            prev = x;
            hasPrev = true;
        };
    }
}
