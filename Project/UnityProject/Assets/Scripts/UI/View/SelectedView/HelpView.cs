using UnityEngine;
using System.Collections;

public class HelpView : MonoBehaviour 
{
    public GameObject m_returnBtn = null;
    public GameObject m_downAppBtn = null;
    public GameObject m_payBtn = null;

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
	}
	
	private void Update () 
    {
	
	}
}
