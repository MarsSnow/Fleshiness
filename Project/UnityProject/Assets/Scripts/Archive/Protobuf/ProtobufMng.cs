using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ProtobufMng : MonoBehaviour
{
    private ProtobufSerializer m_serializer = new ProtobufSerializer();
    private static ProtobufMng s_instance = null;

    void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(this);
    }

    public static ProtobufMng instance
    {
        get { return s_instance; }
    }

    public ProtobufSerializer Serializer
    {
        get { return m_serializer; }
    }
}
