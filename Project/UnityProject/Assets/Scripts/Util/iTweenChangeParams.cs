using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// I tween change parameters.
/// </summary>
public class iTweenChangeParams
{
	private GameObject m_tweenObj;
    public UnityEngine.GameObject TweenObj
    {
        get { return m_tweenObj; }
    }

	private List<iTweenParam> m_paramList;
    public List<iTweenParam> ParamList
    {
        get { return m_paramList; }
    }

    /// <summary>
    /// 是否结束后销毁
    /// </summary>
    private bool m_endDestory = false;

	//if there is function of destory that the user declared
	public delegate void iTweenEndDelegate (GameObject obj);
	public event iTweenEndDelegate iTweenEnd = null; 

    /// <summary>
    /// 构造一个Itween参数
    /// </summary>
    /// <param name="tweenObj">补间对象</param>
    /// <param name="paramsList">参数列表</param>
    /// <param name="endDestory">是否结束后销毁</param>
    public iTweenChangeParams(GameObject tweenObj, List<iTweenParam> paramsList, bool endDestory)
    {
        m_paramList = paramsList;
        m_tweenObj = tweenObj;
        m_endDestory = endDestory;
    }

	/// <summary>
	/// Calls the destory func.
	/// </summary>
	/// <param name="obj">Object.</param>
	public void OniTweenEnd(GameObject obj)
	{
		if(iTweenEnd != null)
		{
			iTweenEnd(obj);
		}

        if (m_endDestory)
        {
            GameObject.Destroy(obj);
        }
	}
}

/// <summary>
/// I tween parameter.
/// </summary>
public class iTweenParam
{
	public string m_functionName = "";
	//HashTable
	public Hashtable m_hashTable;

}
