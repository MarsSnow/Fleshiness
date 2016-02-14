using UnityEngine;
using System.Reflection;
using UnityEngine.Scripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;

public class Globals : MonoBehaviour
{
    //类别Tab
    public SelectedViewTabel m_selectedViewTabel = null;
    //类别内容ListView
    public SelectedView m_selectedView = null;
    //类别内容View
    public InfoView m_infoView = null;
    //帮助View
    public HelpView m_helpView = null;

    //单例
    private static Globals s_instance = null;
    public static Globals instance
    {
        get { return s_instance; }
    }

    private void Awake()
    {
        s_instance = this;

        Object.DontDestroyOnLoad(this);
        ConfigManager.instance.Init();
        InitActiveObj();
    }
    
    private void InitActiveObj()
    {
        m_selectedViewTabel.gameObject.SetActive(true);
        m_selectedView.gameObject.SetActive(true);
        m_infoView.gameObject.SetActive(false);
        m_helpView.gameObject.SetActive(false);
    }

    public void OnApplicationPause(bool pause)
    {
    }
}
