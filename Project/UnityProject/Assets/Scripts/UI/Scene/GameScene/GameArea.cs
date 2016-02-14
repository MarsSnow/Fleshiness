using UnityEngine;
using System.Collections;

public class GameArea : MonoBehaviour
{

    #region variable
    public GameObject m_penObj = null;
    public GameObject m_paperObj = null;
    #endregion

    #region LifeCircle
    private void Awake()
    {
        MessageManager.AddListener(MsgType.GameScene.SetPosition, SetPosition);
    }

	private void Start () 
    {
	
	}
	
	private void Update () 
    {
	
	}
    #endregion

    private void SetPosition(Message msg)
    {
        Vector3 vector3 = (Vector3)msg.args[0];

        float xOffset = vector3.x;
        float yOffset = vector3.y;

        float y = m_penObj.transform.localPosition.y;
        float x = m_penObj.transform.localPosition.x;

        m_penObj.transform.localPosition = new Vector3(x + xOffset, y + yOffset, 0);
    }
}
