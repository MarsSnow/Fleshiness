using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    #region varialbe
    //show in inspector
    public GameObject m_penSprite = null;
    public GameObject[] m_controlBtns = null;

    //cosnt
    private const int kStep = 10;

    //enum
    public enum ControlBtnType
    {
        Up    = 0,
        Down  = 1,
        Left  = 2,
        Right = 3,
    }
    #endregion

    #region LifeCircle
    private void Awake()
    {
    }

	private void Start () 
    {
        BindingControlBtnListener();
	}
	
	private void Update () 
    {
	
	}
    #endregion

    #region ClickEvent
    private void BindingControlBtnListener()
    {
        for (int i = 0; i < m_controlBtns.Length; ++i)
        {
            GameObject btnObj = m_controlBtns[i];
            //UIEventListener.Get(btnObj).onClick = OnControlBtnClick;
            UIEventListener.Get(btnObj).onPress = OnControlBtnPress;
        }
    }

    private void OnControlBtnPress(GameObject obj, bool ispress)
    {
        string btnName = obj.name;
        ControlBtnType controlBtnType = (ControlBtnType)System.Enum.Parse(typeof(ControlBtnType), btnName);

        switch (controlBtnType)
        {
            case ControlBtnType.Up:
                {
                    Debug.LogError("Up");
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(0, kStep, 0));
                    break;
                }
            case ControlBtnType.Down:
                {
                    Debug.LogError("Down");
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(0, -kStep, 0));
                    break;
                }
            case ControlBtnType.Left:
                {
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(-kStep, 0, 0));
                    Debug.LogError("Left");
                    break;
                }
            case ControlBtnType.Right:
                {
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(kStep, 0, 0));
                    Debug.LogError("Right");
                    break;
                }
        }
    }

    private void OnControlBtnClick(GameObject obj)
    {
        string btnName = obj.name;
        ControlBtnType controlBtnType = (ControlBtnType)System.Enum.Parse(typeof(ControlBtnType), btnName);
        switch (controlBtnType)
        {
            case ControlBtnType.Up:
                {
                    Debug.LogError("Up");
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(0, kStep, 0));
                    break;
                }
            case ControlBtnType.Down:
                {
                    Debug.LogError("Down");
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(0, -kStep, 0));
                    break;
                }
            case ControlBtnType.Left:
                {
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(-kStep, 0, 0));
                    Debug.LogError("Left");
                    break;
                }
            case ControlBtnType.Right:
                {
                    MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(kStep, 0, 0));
                    Debug.LogError("Right");
                    break;
                }
        }
    }
    #endregion
}
