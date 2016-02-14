// 此文件为自动生成请勿手工修改
using UnityEngine;
using System.Collections.Generic;

public class HeroConfig : ConfigDataBase
{
    private string m_name = "";
    public string name{ get{return m_name;}}
    private int m_career = 0;
    public int career{ get{return m_career;}}
    private int m_formationPriority = 0;
    public int formationPriority{ get{return m_formationPriority;}}
    private int m_idlePosPriority = 0;
    public int idlePosPriority{ get{return m_idlePosPriority;}}
    private int m_attacktype = 0;
    public int attacktype{ get{return m_attacktype;}}
    private string m_headIcon = "";
    public string headIcon{ get{return m_headIcon;}}
    private string m_headIconSpell = "";
    public string headIconSpell{ get{return m_headIconSpell;}}
    private string m_headIconDead = "";
    public string headIconDead{ get{return m_headIconDead;}}
    private string m_roleListIcon = "";
    public string roleListIcon{ get{return m_roleListIcon;}}
    private string m_resource = "";
    public string resource{ get{return m_resource;}}
    private string m_heroBackground = "";
    public string heroBackground{ get{return m_heroBackground;}}
    private string m_description = "";
    public string description{ get{return m_description;}}
    private int m_profession = 0;
    public int profession{ get{return m_profession;}}
    private string m_professionIcon = "";
    public string professionIcon{ get{return m_professionIcon;}}
    private int m_rightWeaponType = 0;
    public int rightWeaponType{ get{return m_rightWeaponType;}}
    private int m_leftWeaponType = 0;
    public int leftWeaponType{ get{return m_leftWeaponType;}}
    private string m_initWeapons = "";
    public string initWeapons{ get{return m_initWeapons;}}
    private int m_maxLevel = 0;
    public int maxLevel{ get{return m_maxLevel;}}
    private int m_bornStar = 0;
    public int bornStar{ get{return m_bornStar;}}
    private int m_soulShardId = 0;
    public int soulShardId{ get{return m_soulShardId;}}
    private int m_shardCount = 0;
    public int shardCount{ get{return m_shardCount;}}
    private int m_cardToShard = 0;
    public int cardToShard{ get{return m_cardToShard;}}
    private string m_leaderSpellId = "";
    public string leaderSpellId{ get{return m_leaderSpellId;}}
    private string m_comSpellList = "";
    public string comSpellList{ get{return m_comSpellList;}}
    private string m_spellList = "";
    public string spellList{ get{return m_spellList;}}
    private int m_warnRange = 0;
    public int warnRange{ get{return m_warnRange;}}
    private string m_expression = "";
    public string expression{ get{return m_expression;}}
    private string m_uiModelRes = "";
    public string uiModelRes{ get{return m_uiModelRes;}}
    private float m_strArg = 0.0f;
    public float strArg{ get{return m_strArg;}}
    private float m_agiArg = 0.0f;
    public float agiArg{ get{return m_agiArg;}}
    private float m_spiArg = 0.0f;
    public float spiArg{ get{return m_spiArg;}}
    private float m_intArg = 0.0f;
    public float intArg{ get{return m_intArg;}}
    private float m_staArg = 0.0f;
    public float staArg{ get{return m_staArg;}}
    private string m_strengTimes = "";
    public string strengTimes{ get{return m_strengTimes;}}
    private string m_hpArg = "";
    public string hpArg{ get{return m_hpArg;}}
    private string m_apArg = "";
    public string apArg{ get{return m_apArg;}}
    private string m_spArg = "";
    public string spArg{ get{return m_spArg;}}
    private string m_armArg = "";
    public string armArg{ get{return m_armArg;}}
    private string m_resArg = "";
    public string resArg{ get{return m_resArg;}}
    private string m_roleInfoProfessionIcon = "";
    public string roleInfoProfessionIcon{ get{return m_roleInfoProfessionIcon;}}
    private string m_roleInfoHeroListIcon = "";
    public string roleInfoHeroListIcon{ get{return m_roleInfoHeroListIcon;}}
    private string m_roleInfoHeroShowIcon = "";
    public string roleInfoHeroShowIcon{ get{return m_roleInfoHeroShowIcon;}}
    private string m_lobbySound = "";
    public string lobbySound{ get{return m_lobbySound;}}
    private string m_equipSound = "";
    public string equipSound{ get{return m_equipSound;}}
    public override ConfigTypeEnum Type
    {
        get { return ConfigTypeEnum.Hero; }
    }
    public override bool Parse(LitJson.JsonData data)
    {
        if(data["id"].ToString() != "") m_id = int.Parse(data["id"].ToString());
        m_name = data["name"].ToString();
        if(data["career"].ToString() != "") m_career = int.Parse(data["career"].ToString());
        if(data["formationPriority"].ToString() != "") m_formationPriority = int.Parse(data["formationPriority"].ToString());
        if(data["idlePosPriority"].ToString() != "") m_idlePosPriority = int.Parse(data["idlePosPriority"].ToString());
        if(data["attacktype"].ToString() != "") m_attacktype = int.Parse(data["attacktype"].ToString());
        m_headIcon = data["headIcon"].ToString();
        m_headIconSpell = data["headIconSpell"].ToString();
        m_headIconDead = data["headIconDead"].ToString();
        m_roleListIcon = data["roleListIcon"].ToString();
        m_resource = data["resource"].ToString();
        m_heroBackground = data["heroBackground"].ToString();
        m_description = data["description"].ToString();
        if(data["profession"].ToString() != "") m_profession = int.Parse(data["profession"].ToString());
        m_professionIcon = data["professionIcon"].ToString();
        if(data["rightWeaponType"].ToString() != "") m_rightWeaponType = int.Parse(data["rightWeaponType"].ToString());
        if(data["leftWeaponType"].ToString() != "") m_leftWeaponType = int.Parse(data["leftWeaponType"].ToString());
        m_initWeapons = data["initWeapons"].ToString();
        if(data["maxLevel"].ToString() != "") m_maxLevel = int.Parse(data["maxLevel"].ToString());
        if(data["bornStar"].ToString() != "") m_bornStar = int.Parse(data["bornStar"].ToString());
        if(data["soulShardId"].ToString() != "") m_soulShardId = int.Parse(data["soulShardId"].ToString());
        if(data["shardCount"].ToString() != "") m_shardCount = int.Parse(data["shardCount"].ToString());
        if(data["cardToShard"].ToString() != "") m_cardToShard = int.Parse(data["cardToShard"].ToString());
        m_leaderSpellId = data["leaderSpellId"].ToString();
        m_comSpellList = data["comSpellList"].ToString();
        m_spellList = data["spellList"].ToString();
        if(data["warnRange"].ToString() != "") m_warnRange = int.Parse(data["warnRange"].ToString());
        m_expression = data["expression"].ToString();
        m_uiModelRes = data["uiModelRes"].ToString();
        if(data["strArg"].ToString() != "") m_strArg = float.Parse(data["strArg"].ToString());
        if(data["agiArg"].ToString() != "") m_agiArg = float.Parse(data["agiArg"].ToString());
        if(data["spiArg"].ToString() != "") m_spiArg = float.Parse(data["spiArg"].ToString());
        if(data["intArg"].ToString() != "") m_intArg = float.Parse(data["intArg"].ToString());
        if(data["staArg"].ToString() != "") m_staArg = float.Parse(data["staArg"].ToString());
        m_strengTimes = data["strengTimes"].ToString();
        m_hpArg = data["hpArg"].ToString();
        m_apArg = data["apArg"].ToString();
        m_spArg = data["spArg"].ToString();
        m_armArg = data["armArg"].ToString();
        m_resArg = data["resArg"].ToString();
        m_roleInfoProfessionIcon = data["roleInfoProfessionIcon"].ToString();
        m_roleInfoHeroListIcon = data["roleInfoHeroListIcon"].ToString();
        m_roleInfoHeroShowIcon = data["roleInfoHeroShowIcon"].ToString();
        m_lobbySound = data["lobbySound"].ToString();
        m_equipSound = data["equipSound"].ToString();
        return true;
    }
    public object GetValueByName(string name)
    {
        switch (name)
        {
            case "id": return m_id;
            case "name": return m_name;
            case "career": return m_career;
            case "formationPriority": return m_formationPriority;
            case "idlePosPriority": return m_idlePosPriority;
            case "attacktype": return m_attacktype;
            case "headIcon": return m_headIcon;
            case "headIconSpell": return m_headIconSpell;
            case "headIconDead": return m_headIconDead;
            case "roleListIcon": return m_roleListIcon;
            case "resource": return m_resource;
            case "heroBackground": return m_heroBackground;
            case "description": return m_description;
            case "profession": return m_profession;
            case "professionIcon": return m_professionIcon;
            case "rightWeaponType": return m_rightWeaponType;
            case "leftWeaponType": return m_leftWeaponType;
            case "initWeapons": return m_initWeapons;
            case "maxLevel": return m_maxLevel;
            case "bornStar": return m_bornStar;
            case "soulShardId": return m_soulShardId;
            case "shardCount": return m_shardCount;
            case "cardToShard": return m_cardToShard;
            case "leaderSpellId": return m_leaderSpellId;
            case "comSpellList": return m_comSpellList;
            case "spellList": return m_spellList;
            case "warnRange": return m_warnRange;
            case "expression": return m_expression;
            case "uiModelRes": return m_uiModelRes;
            case "strArg": return m_strArg;
            case "agiArg": return m_agiArg;
            case "spiArg": return m_spiArg;
            case "intArg": return m_intArg;
            case "staArg": return m_staArg;
            case "strengTimes": return m_strengTimes;
            case "hpArg": return m_hpArg;
            case "apArg": return m_apArg;
            case "spArg": return m_spArg;
            case "armArg": return m_armArg;
            case "resArg": return m_resArg;
            case "roleInfoProfessionIcon": return m_roleInfoProfessionIcon;
            case "roleInfoHeroListIcon": return m_roleInfoHeroListIcon;
            case "roleInfoHeroShowIcon": return m_roleInfoHeroShowIcon;
            case "lobbySound": return m_lobbySound;
            case "equipSound": return m_equipSound;
        }
        return null;
    }
}