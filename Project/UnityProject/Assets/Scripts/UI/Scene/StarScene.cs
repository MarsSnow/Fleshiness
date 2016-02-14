using UnityEngine;
using System.Collections;

public class StarScene : MonoBehaviour {

    public GameObject m_startButton = null;

	void Start () 
    {
        UIEventListener.Get(m_startButton).onClick = OnStartClick;
	}
	
	void Update () 
    {
	
	}

    private void OnStartClick(GameObject obj)
    {
        Application.LoadLevel("GameScene"); 
    }
}
