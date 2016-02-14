using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;

public static class ItemRecordMng
{

    public static int GetCurItemState(int family, int species)
    {
        ItemRecordKey twoArgKey = new ItemRecordKey(family, species);
        Dictionary<ItemRecordKey, int> dic = GetMap();

        bool isContains = dic.ContainsKey(twoArgKey);
        if (isContains)
        {
            int stateValue = 0;
            dic.TryGetValue(twoArgKey, out stateValue);
            return stateValue;
        }
        return 0;
    }

    public static void SetCurItemState(int family, int species, int curState)
    {
        ItemRecordKey twoArgKey = new ItemRecordKey(family, species);
        Dictionary<ItemRecordKey, int> dic = GetMap();

        bool isContains = dic.ContainsKey(twoArgKey);
        if (isContains)
        {
            int stateValue = 0;
            dic.TryGetValue(twoArgKey, out stateValue);
            stateValue = curState;
        }
        else
        {
            dic.Add(twoArgKey, curState);
        }
        Write(dic);
    }

    private static void Write(Dictionary<ItemRecordKey, int> dic)
    {
        List<int> list = new List<int>();
        list.Add(1);
        Dictionary < int, int> dics = new Dictionary<int, int>();
        dics.Add(0,1);
        string chatInfoStr = JsonMapper.ToJson(dics);
        PrefsMng.SetLocalArchive(PrefsType.ItemRecord, chatInfoStr);
    }

    private static Dictionary<ItemRecordKey, int> GetMap()
    {
        Dictionary<ItemRecordKey, int> dic = new Dictionary<ItemRecordKey, int>();
        string dicStr = "";
        bool isGet = PrefsMng.GetLocalArchive(PrefsType.ItemRecord, out dicStr);
        if (!isGet)
        {
            PrefsMng.SetLocalArchive(PrefsType.ItemRecord, dicStr);
            return dic;
        }
        dic = JsonMapper.ToObject<Dictionary<ItemRecordKey, int>>(dicStr);
        return dic;
    }
}

