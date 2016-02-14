using UnityEngine;
using System.Collections;

public class Singleton<T> where T : new() {
    private static T s_instance = new T();
    protected Singleton() { }

    public static T instance
    {
        get
        {
            return s_instance;
        }
    }
}
