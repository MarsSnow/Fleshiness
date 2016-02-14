// 此文件为自动生成请勿手工修改
using UnityEngine;
using System.Collections.Generic;

public class SpeciesConfig : ConfigDataBase
{
    private string m_name = "";
    public string name{ get{return m_name;}}
    private string m_info = "";
    public string info{ get{return m_info;}}
    private int m_cid = 0;
    public int cid{ get{return m_cid;}}
    private int m_type = 0;
    public int type{ get{return m_type;}}
    private int m_needLevel = 0;
    public int needLevel{ get{return m_needLevel;}}
    private int m_costPower = 0;
    public int costPower{ get{return m_costPower;}}
    private int m_enterCostPower = 0;
    public int enterCostPower{ get{return m_enterCostPower;}}
    private int m_times = 0;
    public int times{ get{return m_times;}}
    private string m_icon = "";
    public string icon{ get{return m_icon;}}
    private string m_openStoryID = "";
    public string openStoryID{ get{return m_openStoryID;}}
    private int m_levelScore = 0;
    public int levelScore{ get{return m_levelScore;}}
    private string m_enemyids = "";
    public string enemyids{ get{return m_enemyids;}}
    private string m_props = "";
    public string props{ get{return m_props;}}
    private int m_summonerExp = 0;
    public int summonerExp{ get{return m_summonerExp;}}
    private int m_heroExp = 0;
    public int heroExp{ get{return m_heroExp;}}
    private int m_maxTime = 0;
    public int maxTime{ get{return m_maxTime;}}
    private string m_exp = "";
    public string exp{ get{return m_exp;}}
    private int m_money = 0;
    public int money{ get{return m_money;}}
    private string m_storyStar = "";
    public string storyStar{ get{return m_storyStar;}}
    private int m_storyPic = 0;
    public int storyPic{ get{return m_storyPic;}}
    private int m_dropperId = 0;
    public int dropperId{ get{return m_dropperId;}}
    private int m_bossDrop = 0;
    public int bossDrop{ get{return m_bossDrop;}}
    private int m_otherDrop = 0;
    public int otherDrop{ get{return m_otherDrop;}}
    private int m_sweepDropID = 0;
    public int sweepDropID{ get{return m_sweepDropID;}}
    public override ConfigTypeEnum Type
    {
        get { return ConfigTypeEnum.Species; }
    }
    public override bool Parse(LitJson.JsonData data)
    {
        if(data["id"].ToString() != "") m_id = int.Parse(data["id"].ToString());
        m_name = data["name"].ToString();
        m_info = data["info"].ToString();
        if(data["cid"].ToString() != "") m_cid = int.Parse(data["cid"].ToString());
        if(data["type"].ToString() != "") m_type = int.Parse(data["type"].ToString());
        if(data["needLevel"].ToString() != "") m_needLevel = int.Parse(data["needLevel"].ToString());
        if(data["costPower"].ToString() != "") m_costPower = int.Parse(data["costPower"].ToString());
        if(data["enterCostPower"].ToString() != "") m_enterCostPower = int.Parse(data["enterCostPower"].ToString());
        if(data["times"].ToString() != "") m_times = int.Parse(data["times"].ToString());
        m_icon = data["icon"].ToString();
        m_openStoryID = data["openStoryID"].ToString();
        if(data["levelScore"].ToString() != "") m_levelScore = int.Parse(data["levelScore"].ToString());
        m_enemyids = data["enemyids"].ToString();
        m_props = data["props"].ToString();
        if(data["summonerExp"].ToString() != "") m_summonerExp = int.Parse(data["summonerExp"].ToString());
        if(data["heroExp"].ToString() != "") m_heroExp = int.Parse(data["heroExp"].ToString());
        if(data["maxTime"].ToString() != "") m_maxTime = int.Parse(data["maxTime"].ToString());
        m_exp = data["exp"].ToString();
        if(data["money"].ToString() != "") m_money = int.Parse(data["money"].ToString());
        m_storyStar = data["storyStar"].ToString();
        if(data["storyPic"].ToString() != "") m_storyPic = int.Parse(data["storyPic"].ToString());
        if(data["dropperId"].ToString() != "") m_dropperId = int.Parse(data["dropperId"].ToString());
        if(data["bossDrop"].ToString() != "") m_bossDrop = int.Parse(data["bossDrop"].ToString());
        if(data["otherDrop"].ToString() != "") m_otherDrop = int.Parse(data["otherDrop"].ToString());
        if(data["sweepDropID"].ToString() != "") m_sweepDropID = int.Parse(data["sweepDropID"].ToString());
        return true;
    }
    public object GetValueByName(string name)
    {
        switch (name)
        {
            case "id": return m_id;
            case "name": return m_name;
            case "info": return m_info;
            case "cid": return m_cid;
            case "type": return m_type;
            case "needLevel": return m_needLevel;
            case "costPower": return m_costPower;
            case "enterCostPower": return m_enterCostPower;
            case "times": return m_times;
            case "icon": return m_icon;
            case "openStoryID": return m_openStoryID;
            case "levelScore": return m_levelScore;
            case "enemyids": return m_enemyids;
            case "props": return m_props;
            case "summonerExp": return m_summonerExp;
            case "heroExp": return m_heroExp;
            case "maxTime": return m_maxTime;
            case "exp": return m_exp;
            case "money": return m_money;
            case "storyStar": return m_storyStar;
            case "storyPic": return m_storyPic;
            case "dropperId": return m_dropperId;
            case "bossDrop": return m_bossDrop;
            case "otherDrop": return m_otherDrop;
            case "sweepDropID": return m_sweepDropID;
        }
        return null;
    }
}