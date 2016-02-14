using UnityEngine;
using System.Collections;

public class GameFunction : MonoBehaviour
{
    #region variable
    public GameObject m_exitBtn = null;
    public GameObject m_nextBtn = null;
    #endregion

    #region lifeCircle
    private void Awake()
    {
        BindingListener();
    }

	private void Start () 
    {
	    
	}
	
	// Update is called once per frame
	private void Update () 
    {

    }
    #endregion

    #region ClickEvent
    private void BindingListener()
    {
        UIEventListener.Get(m_exitBtn).onClick = DoExit;
        UIEventListener.Get(m_nextBtn).onClick = DoNext;
    }

    private void DoExit(GameObject obj)
    {
        Debug.LogError("游戏退出");
        Application.Quit();
    }

    private void DoNext(GameObject obj)
    {
        Debug.LogError("下一关");
    }
    #endregion
}
