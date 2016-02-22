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
		m_infoLbel.text = "欢迎使用，支持我一下吧^_^";
		m_payBtnLabel.text = "支持一下";
		m_adBtnLabel.text = "下载其他应用";
	}
	
	private void Update () 
	{
	
	}
}
