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
/// 类名：隐藏非活动Node
/// 作用：提高效率
/// @author 彭博
/// </summary>
public class HideInActiveNode : MonoBehaviour 
{
    private bool m_isLock = true;
    private float m_startX = 0;
    private float m_endX = 0;
    private GameObject m_itemObj = null;
    private float m_itemPositionX = 0;
    private UIPanel m_uiPanel;
    private List<GameObject> m_gridList = null;

    private const float kStartOffset = 1200;
    private const float kEndOffset = 2000;

    public void TriggerHideItem(Message msg)
    {
        m_isLock = (bool)msg.args[0];
        m_uiPanel = (UIPanel)msg.args[1];
        m_gridList = (List<GameObject>)msg.args[2];
    }
    private void Awake()
    {
        MessageManager.AddListener(MsgType.HideInActiveNode.triggerHideItem, TriggerHideItem);
    }

    private void Update()
    {
        if (m_isLock)
        {
            return;
        }
        HideInActiveItem(m_uiPanel, m_gridList);
    }

    private void HideInActiveItem(UIPanel uiPanel, List<GameObject> gridObjList)
    {
        m_startX = uiPanel.clipOffset.x - kStartOffset;
        m_endX = uiPanel.clipOffset.x + kEndOffset;
        for (int i = 0; i < gridObjList.Count; ++i)
        {
            m_itemObj = gridObjList[i];
            m_itemPositionX = m_itemObj.transform.localPosition.x;
            if (m_itemPositionX > m_endX || m_itemPositionX < m_startX)
            {
                m_itemObj.SetActive(false);
            }
            else
            {
                m_itemObj.SetActive(true);
            }
        }
    }
}
