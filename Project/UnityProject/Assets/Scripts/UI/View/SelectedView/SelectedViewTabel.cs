using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedViewTabel : MonoBehaviour 
{
    public UIGrid m_leftUiGrid = null;
    public UIGrid m_rightUiGrid = null;
    public GameObject m_nodePrefab = null;

    private void OnEnable()
    {
        if (m_leftUiGrid.transform.childCount == 0 && m_rightUiGrid.transform.childCount == 0)
        {
            CreateGrid();
        }    
    }

    private void CreateGrid()
    {
        int max = ConfigManager.instance.GetConfig(ConfigTypeEnum.Family).Count;                //最大值

        int positionIndex = PrefsMng.GetPrefs(PrefsType.SelectedViewTabIndex);                  //点击位置
        //左Tab
        for (int i = 0; i < max/2; ++i)
        {
            GameObject nodeObj = (GameObject)Instantiate(m_nodePrefab) as GameObject;
            Utility.ResetGameObject(m_leftUiGrid.gameObject, nodeObj, UnityConfig.UILayer);
            nodeObj.GetComponent<SelectedViewTabelNode>().Init(i);
            if (positionIndex == i)
            {
                nodeObj.GetComponent<UIToggle>().startsActive = true;
            }
        }

        //右Tab
        for (int i = max/2; i < max; ++i)
        {
            GameObject nodeObj = (GameObject)Instantiate(m_nodePrefab) as GameObject;
            Utility.ResetGameObject(m_rightUiGrid.gameObject, nodeObj, UnityConfig.UILayer);
            nodeObj.GetComponent<SelectedViewTabelNode>().Init(i);
            if (positionIndex == i)
            {
                nodeObj.GetComponent<UIToggle>().startsActive = true;
            }
        }
    }
}
