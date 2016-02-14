using UnityEngine;
using System.Collections;

public class InfoView : MonoBehaviour 
{
    public Transform m_InfoViewNode = null;
    public GameObject m_returnBtn = null;
    public GameObject m_helpBtn = null;
    private GameObject m_spriteObj = null;

    public void InitInfoView(int id)
    {
        GameObject.DestroyImmediate(m_spriteObj);
        m_spriteObj = Utility.DynamicCreatImageObject(UnityConfig.kPicInfoPath + id, m_InfoViewNode, 1, 1, 1, 1024, 520, "sprite", UnityConfig.UILayer, true);
    }

    private void Start()
    {
        UIEventListener.Get(m_returnBtn).onClick = OnNodeClick;
        UIEventListener.Get(m_helpBtn).onClick = OnClickHelpBtn;
    }

    private void OnNodeClick(GameObject obj)
    {
        Globals.instance.m_selectedView.gameObject.SetActive(true);
        Globals.instance.m_selectedViewTabel.gameObject.SetActive(true);
        Globals.instance.m_infoView.gameObject.SetActive(false);
        Globals.instance.m_helpView.gameObject.SetActive(false);
    }

    private void OnClickHelpBtn(GameObject obj)
    {
        Globals.instance.m_selectedView.gameObject.SetActive(false);
        Globals.instance.m_selectedViewTabel.gameObject.SetActive(false);
        Globals.instance.m_infoView.gameObject.SetActive(false);
        Globals.instance.m_helpView.gameObject.SetActive(true);

    }
}
