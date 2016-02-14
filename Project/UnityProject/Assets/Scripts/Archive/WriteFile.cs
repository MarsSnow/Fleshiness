using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 写文件
/// </summary>
public class WriteFile
{
    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="name">文件名字</param>
    /// <param name="info">文件内容</param>
    public static void CreateFile(string path, string name, string info)
    {
        string filePath = path + "/" + name;
        StreamWriter sw = null;
        FileInfo t = new FileInfo(filePath);
        if (!File.Exists(filePath))
        {
            sw = File.CreateText(filePath);
        }
        else
        {
            sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
        }
        sw.Write(info);
        sw.Close();
        sw.Dispose();
    }
}

