/*
* 　　　　　　　　┏┓　　　┏┓+ +
*　　　　　　　┏┛┻━━━┛┻┓ + +
*　　　　　　　┃　　　　　　　┃ 　
*　　　　　　　┃　　　━　　　┃ ++ + + +
*　　　　　　 ████━████ ┃+
*　　　　　　　┃　　　　　　　┃ +
*　　　　　　　┃　　　┻　　　┃
*　　　　　　　┃　　　　　　　┃ + +
*　　　　　　　┗━┓　　　┏━┛
*　　　　　　　　　┃　　　┃　　　　　　　　　
*　　　　　　　　　┃　　　┃ + + + +
*　　　　　　　　　┃　　　┃　
*　　　　　　　　　┃　　　┃ + 　　　　
*　　　　　　　　　┃　　　┃
*　　　　　　　　　┃　　　┃　　+　　　　　　　　　
*　　　　　　　　　┃　 　　┗━━━┓ + +
*　　　　　　　　　┃ 　　　　　　　┣┓
*　　　　　　　　　┃ 　　　　　　　┏┛
*　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
*　　　　　　　　　　┃┫┫　┃┫┫
*　　　　　　　　　　┗┻┛　┗┻┛+ + + +
*/
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Collections;

/// <summary>
/// 条目记录管理器
/// @author 彭博
/// </summary>
public static class ItemRecordMng
{
    private const string kKeyString = "family:{0}|species:{1}";

    public static string GetCurItemState(int family, int species)
    {
        string twoArgKey = string.Format(kKeyString, family, species);
        Dictionary<string, string> dic = GetMap();

        bool isContains = dic.ContainsKey(twoArgKey);
        if (isContains)
        {
            string stateValue = "false";
            dic.TryGetValue(twoArgKey, out stateValue);
            return stateValue;
        }
        return "notContain";
    }

    public static void SetCurItemState(int family, int species, string curState)
    {
        string keyString = string.Format(kKeyString, family, species);
        Dictionary<string, string> dic = GetMap();

        bool isContains = dic.ContainsKey(keyString);
        if (isContains)
        {
            dic[keyString] = curState;
        }
        else
        {
            dic.Add(keyString, curState);
        }
        Write(dic);
    }


    private static void Write(Dictionary<string, string> dic)
    {
        string chatInfoStr = JsonMapper.ToJson(dic);     
        PrefsMng.SetLocalArchive(PrefsType.ItemRecord, chatInfoStr);
    }

    private static Dictionary<string, string> GetMap()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string dicStr = "";
        bool isGet = PrefsMng.GetLocalArchive(PrefsType.ItemRecord, out dicStr);
        if (!isGet)
        {
            PrefsMng.SetLocalArchive(PrefsType.ItemRecord, dicStr);
            return dic;
        }
        dic = JsonMapper.ToObject<Dictionary<string, string>>(dicStr);
        return dic;
    }
}

