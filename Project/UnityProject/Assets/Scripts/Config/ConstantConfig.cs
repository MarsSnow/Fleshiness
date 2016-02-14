// 此文件为自动生成请勿手工修改
using UnityEngine;
using System.Collections.Generic;

public class ConstantConfig : ConfigDataBase
{
    private string m_name = "";
    public string name{ get{return m_name;}}
    private string m_constant = "";
    public string constant{ get{return m_constant;}}
    private string m_note = "";
    public string note{ get{return m_note;}}
    public override ConfigTypeEnum Type
    {
        get { return ConfigTypeEnum.Constant; }
    }
    public override bool Parse(LitJson.JsonData data)
    {
        if(data["id"].ToString() != "") m_id = int.Parse(data["id"].ToString());
        m_name = data["name"].ToString();
        m_constant = data["constant"].ToString();
        m_note = data["note"].ToString();
        return true;
    }
    public object GetValueByName(string name)
    {
        switch (name)
        {
            case "id": return m_id;
            case "name": return m_name;
            case "constant": return m_constant;
            case "note": return m_note;
        }
        return null;
    }
}