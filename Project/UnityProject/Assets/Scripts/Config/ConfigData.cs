// 此文件为自动生成请勿手工修改
using UnityEngine;
using System;
using System.Collections.Generic;

public enum ConfigTypeEnum
{
    Constant,
    Family,
    Hero,
    Species,
    
}

public abstract class ConfigDataBase
{
    public static Dictionary<string, Type> ConfigFileList = new Dictionary<string, Type>()
    {
        {"constant", typeof(ConstantConfig)},
        {"family", typeof(FamilyConfig)},
        {"hero", typeof(HeroConfig)},
        {"species", typeof(SpeciesConfig)},
        
    };
    
    public int id { get { return m_id; } set { m_id = value; } }
    protected int m_id = 0;
    public abstract ConfigTypeEnum Type { get; }
    public abstract bool Parse(LitJson.JsonData data);
}