/*
* 　　　　　　　　┏┓　　　┏┓+ +
*　　　　　　　┏┛┻━━━┛┻┓ + +
*　　　　　　　┃　　　　　　　┃ 　
*　　　　　　　┃　　　━　　　┃ ++ + + +
*　　　　　　 ████━████ ┃+
*　　　　　　　┃　　　　　　　┃ +
*　　　　　　　┃　　　┻　　　┃
*　　　　　　　┃　　　　　　　┃ + +
*　　　　　　　┗━┓　　　┏━┛
*　　　　　　　　　┃　　　┃　　　　　　　　　
*　　　　　　　　　┃　　　┃ + + + +
*　　　　　　　　　┃　　　┃　
*　　　　　　　　　┃　　　┃ + 　　　　
*　　　　　　　　　┃　　　┃
*　　　　　　　　　┃　　　┃　　+　　　　　　　　　
*　　　　　　　　　┃　 　　┗━━━┓ + +
*　　　　　　　　　┃ 　　　　　　　┣┓
*　　　　　　　　　┃ 　　　　　　　┏┛
*　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
*　　　　　　　　　　┃┫┫　┃┫┫
*　　　　　　　　　　┗┻┛　┗┻┛+ + + +
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 选择ListView
/// @author 彭博
/// </summary>
public class SelectedView : MonoBehaviour 
{
    public UIPanel[] m_uiPanels = null; 
    public UIScrollView[] m_uiScrollViews = null;
    public UIGrid[] m_uiGrids = null;
    public GameObject m_nodePrefab = null;
    public GameObject m_returnBtn = null;

    private void OnEnable()
    {
        UIEventListener.Get(m_returnBtn).onClick = OnClickReturn;
        MessageManager.AddListener(MsgType.SelectedView.showNode, ShowNode);
        int tabIndex = PrefsMng.GetPrefs(PrefsType.SelectedViewTabIndex);
        InitNode(tabIndex);
    }

    private void OnDisable()
    {
        MessageManager.RemoveListener(MsgType.SelectedView.showNode, ShowNode);
    }

    private void OnClickReturn(GameObject obj)
    {
        Application.Quit(); 
    }

    private void ShowNode(Message msg)
    {
        int tabIndex = (int)msg.args[0];
        InitNode(tabIndex);
    }

    private void HideInActiveScrollView(int tabIndex)
    {
        for (int i = 0; i < m_uiScrollViews.Length; ++i)
        {
            if (i == tabIndex)
            {
                m_uiScrollViews[i].gameObject.SetActive(true);
            }
            else
            {
                m_uiScrollViews[i].gameObject.SetActive(false);
            }
        }
    }

    private void InitNode(int tabIndex)
    {
        HideInActiveScrollView(tabIndex);

        PrefsMng.SetPrefs(PrefsType.SelectedViewTabIndex, tabIndex);
        UIGrid uiGrid = m_uiGrids[tabIndex];       
        if (uiGrid.transform.childCount == 0)
        {
            StartCoroutine("CreateGrid", tabIndex);
        }
        else
        {
            MessageManager.Dispatch(MsgType.HideInActiveNode.triggerHideItem, false, m_uiPanels[tabIndex], SelectedViewManager.instance.gridList[tabIndex]);
        }
    }


    private IEnumerator CreateGrid(int tabIndex)
    {       
        UIGrid uiGrid = m_uiGrids[tabIndex];
        UIPanel uiPanel = m_uiPanels[tabIndex];

        MessageManager.Dispatch(MsgType.HideInActiveNode.triggerHideItem, true, m_uiPanels[tabIndex], SelectedViewManager.instance.gridList[tabIndex]);

        List<int> nodeIds = new List<int>();
        FamilyConfig familyConfig = ConfigManager.instance.GetConfig<FamilyConfig>(ConfigTypeEnum.Family, tabIndex);
        string nodeIdsStr = familyConfig.nodeids;
        Utility.TransValueList(nodeIdsStr, out nodeIds, "|");
        for (int i = 0; i < nodeIds.Count; ++i)
        {

            yield return new WaitForEndOfFrame(); 
            GameObject nodeObj = (GameObject)Instantiate(m_nodePrefab) as GameObject;
            Utility.ResetGameObject(uiGrid.gameObject, nodeObj, UnityConfig.UILayer);
            nodeObj.GetComponent<UIDragScrollView>().scrollView = m_uiScrollViews[tabIndex];
            nodeObj.GetComponent<SelectedViewNode>().Init(tabIndex, i, nodeIds[i]);
            uiGrid.Reposition();
            uiPanel.Refresh();
            SelectedViewManager.instance.gridList[tabIndex].Add(nodeObj);
        }

        MessageManager.Dispatch(MsgType.HideInActiveNode.triggerHideItem, false, m_uiPanels[tabIndex], SelectedViewManager.instance.gridList[tabIndex]);
    }

}
