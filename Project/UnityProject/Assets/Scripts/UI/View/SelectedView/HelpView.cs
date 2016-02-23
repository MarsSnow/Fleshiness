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

/// <summary>
/// 帮助说明类
/// @author 彭博
/// </summary>
public class HelpView : MonoBehaviour 
{
	public GameObject m_returnBtn = null;
    public UILabel m_contact = null;
    public GameObject m_author = null;
	public UILabel m_infoLbel = null;
	public UILabel m_payBtnLabel = null;
	public UILabel m_adBtnLabel = null;

	private void Start () 
	{
		UIEventListener.Get(m_returnBtn).onClick += (g) => 
		{ 
			// Show Info View
			Globals.instance.m_selectedView.gameObject.SetActive(false);
			Globals.instance.m_selectedViewTabel.gameObject.SetActive(false);       
			Globals.instance.m_infoView.gameObject.SetActive(true);
			Globals.instance.m_helpView.gameObject.SetActive(false);
		};
	    m_contact.text = "联系：851104757@qq.com";
        string path = "Chinese/UI/Icon/pic/about";
	    Utility.DeleteChildObjects(m_author);
        Utility.DynamicCreatImageObject(path, m_author.transform, 3, 1, 1, -1, -1);
		m_infoLbel.text = "打赏一下^_^";
		m_payBtnLabel.text = "支持一下";
		m_adBtnLabel.text = "下载其他应用";
	}
	
	private void Update () 
	{
	
	}
}
