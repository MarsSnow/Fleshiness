using UnityEngine;
using System;
using System.Threading;
using System.Collections;
public class LtSingleton<T> where T : class, new()
{
    public static readonly object padlock = new object();
    private static T _instance = Activator.CreateInstance<T>();
    public static T Instance
    {
        get
        {
            object obj = LtSingleton<T>.padlock;
            Monitor.Enter(obj);
            try
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                }
            }
            finally
            {
                Monitor.Exit(obj);
            }
            return _instance;
        }
    }
    public virtual void Clear()
    {
        _instance = (T)((object)null);
    }
}