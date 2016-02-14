using UnityEngine;
using System.Collections.Generic;
using System.IO;

public enum PrefsType
{
    SelectedViewTabIndex,
    ItemRecord,
}

/// <summary>
/// 本地存储信息
/// </summary>
public static class PrefsMng
{
    public static void SetPrefs(PrefsType type, int value)
    {
        string prefsName = GetPrefsName(type, false);
        PlayerPrefs.SetInt(prefsName, value);
    }

    public static int GetPrefs(PrefsType type)
    {
        string prefsName = GetPrefsName(type, false);
        return PlayerPrefs.GetInt(prefsName, 0);
    }

    public static void SetLocalArchive(PrefsType type, string value)
    {
        string prefsName = GetPrefsName(type);
        WriteFile.CreateFile(GetArchivePath(), prefsName, value);
    }

    public static bool GetLocalArchive(PrefsType type, out string value)
    {
        string prefsName = GetPrefsName(type);
        value = ReadFile.LoadFile(GetArchivePath(), prefsName);
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }
        return true;
    }

    private static string GetArchivePath()
    {
        string targetPath = LtPlatform.Instance.WriteablePath + "/Archive";
        if (!Directory.Exists(targetPath)) 
        {
            Directory.CreateDirectory(targetPath); 
        }
        return targetPath;
    }

    private static string GetPrefsName(PrefsType type, bool isLua = true)
    {
        string typeName = type.ToString();
        if (isLua)
        {
            typeName = typeName + ".lua";
        }
        return typeName;
    }

}

