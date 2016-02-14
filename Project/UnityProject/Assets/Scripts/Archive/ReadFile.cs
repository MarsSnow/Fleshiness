using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
/// 读文件
/// </summary>
public class ReadFile
{

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="name">文件名</param>
    /// <returns></returns>
    public static string LoadFile(string path, string name)
    {
        string filePath = path + "/" + name;
        try
        {
            return File.ReadAllText(filePath, System.Text.Encoding.UTF8);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
