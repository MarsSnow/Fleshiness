// 此文件为自动生成请勿手工修改
using UnityEngine;
using System.Collections.Generic;

public class FamilyConfig : ConfigDataBase
{
    private int m_index = 0;
    public int index{ get{return m_index;}}
    private string m_name = "";
    public string name{ get{return m_name;}}
    private int m_type = 0;
    public int type{ get{return m_type;}}
    private string m_nodeids = "";
    public string nodeids{ get{return m_nodeids;}}
    private int m_needLevel = 0;
    public int needLevel{ get{return m_needLevel;}}
    private string m_sceneid = "";
    public string sceneid{ get{return m_sceneid;}}
    private string m_normalStoryIds = "";
    public string normalStoryIds{ get{return m_normalStoryIds;}}
    private string m_eliteStoryIds = "";
    public string eliteStoryIds{ get{return m_eliteStoryIds;}}
    private string m_normalMapNodePath = "";
    public string normalMapNodePath{ get{return m_normalMapNodePath;}}
    private string m_eliteMapNodePath = "";
    public string eliteMapNodePath{ get{return m_eliteMapNodePath;}}
    private string m_starnumber = "";
    public string starnumber{ get{return m_starnumber;}}
    private string m_normalStoryReward = "";
    public string normalStoryReward{ get{return m_normalStoryReward;}}
    private string m_eliteStoryReward = "";
    public string eliteStoryReward{ get{return m_eliteStoryReward;}}
    private string m_chapterBossIconPath = "";
    public string chapterBossIconPath{ get{return m_chapterBossIconPath;}}
    public override ConfigTypeEnum Type
    {
        get { return ConfigTypeEnum.Family; }
    }
    public override bool Parse(LitJson.JsonData data)
    {
        if(data["id"].ToString() != "") m_id = int.Parse(data["id"].ToString());
        if(data["index"].ToString() != "") m_index = int.Parse(data["index"].ToString());
        m_name = data["name"].ToString();
        if(data["type"].ToString() != "") m_type = int.Parse(data["type"].ToString());
        m_nodeids = data["nodeids"].ToString();
        if(data["needLevel"].ToString() != "") m_needLevel = int.Parse(data["needLevel"].ToString());
        m_sceneid = data["sceneid"].ToString();
        m_normalStoryIds = data["normalStoryIds"].ToString();
        m_eliteStoryIds = data["eliteStoryIds"].ToString();
        m_normalMapNodePath = data["normalMapNodePath"].ToString();
        m_eliteMapNodePath = data["eliteMapNodePath"].ToString();
        m_starnumber = data["starnumber"].ToString();
        m_normalStoryReward = data["normalStoryReward"].ToString();
        m_eliteStoryReward = data["eliteStoryReward"].ToString();
        m_chapterBossIconPath = data["chapterBossIconPath"].ToString();
        return true;
    }
    public object GetValueByName(string name)
    {
        switch (name)
        {
            case "id": return m_id;
            case "index": return m_index;
            case "name": return m_name;
            case "type": return m_type;
            case "nodeids": return m_nodeids;
            case "needLevel": return m_needLevel;
            case "sceneid": return m_sceneid;
            case "normalStoryIds": return m_normalStoryIds;
            case "eliteStoryIds": return m_eliteStoryIds;
            case "normalMapNodePath": return m_normalMapNodePath;
            case "eliteMapNodePath": return m_eliteMapNodePath;
            case "starnumber": return m_starnumber;
            case "normalStoryReward": return m_normalStoryReward;
            case "eliteStoryReward": return m_eliteStoryReward;
            case "chapterBossIconPath": return m_chapterBossIconPath;
        }
        return null;
    }
}