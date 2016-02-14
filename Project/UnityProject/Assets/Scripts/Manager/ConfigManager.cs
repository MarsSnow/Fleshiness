using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public sealed class ConfigManager : Singleton<ConfigManager> 
{
    private Dictionary<ConfigTypeEnum, Dictionary<int, ConfigDataBase>> m_configs = 
        new Dictionary<ConfigTypeEnum, Dictionary<int, ConfigDataBase>>();

    public void Init()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        foreach (KeyValuePair<string, Type> pair in ConfigDataBase.ConfigFileList)
        {
            string path = UnityConfig.kConfigDataPath + pair.Key;
            UnityEngine.Object asset = Resources.Load(path);
            Parse(asset, pair.Value);
            Resources.UnloadAsset(asset);
        }
    }

    public T GetConfig<T>(ConfigTypeEnum type, int id) where T : ConfigDataBase
    {
        Dictionary<int, ConfigDataBase> dic;
        m_configs.TryGetValue(type, out dic);

        ConfigDataBase configBase;
        dic.TryGetValue(id, out configBase);

        return (T)configBase;
    }
    public Dictionary<int, ConfigDataBase> GetConfig(ConfigTypeEnum type)
    {
        Dictionary<int, ConfigDataBase> dic;
        m_configs.TryGetValue(type, out dic);

        return dic;
    } 

    public void Parse(UnityEngine.Object assert, Type type)
    {
        if (assert == null)
        {
            return;
        }

        if (!(assert is TextAsset))
        {
            return;
        }

        TextAsset textAsset = (TextAsset)assert;
        string json = textAsset.text;
		LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject<LitJson.JsonData>(json);

        int count = jsonData.Count;
        for (int i = 0; i < count; i++)
        {
            LitJson.JsonData data = jsonData[i];
            if (data == null)
            {
                continue;
            }
            ConfigDataBase configObj = (ConfigDataBase)Activator.CreateInstance(type);
            if (!configObj.Parse(data))
            {
                Debug.LogWarning("::Config:Parse() PreseError: Type =" + configObj.Type.ToString());
                continue;
            }

            if (configObj.id == -1)
            {
                continue;
            }

            if(m_configs.ContainsKey(configObj.Type))
            {
                try
                {
                    m_configs[configObj.Type].Add(configObj.id, configObj);
                }
                catch(Exception e)
                {
                    Debug.Log(e.Message + "name:" + configObj.ToString() + ", the repeated id is:" + configObj.id);
                }
            }
            else
            {
                Dictionary<int, ConfigDataBase> configDic = new Dictionary<int, ConfigDataBase>();
                configDic.Add(configObj.id, configObj);
                m_configs.Add(configObj.Type, configDic);
            }
        }
    }	
}
