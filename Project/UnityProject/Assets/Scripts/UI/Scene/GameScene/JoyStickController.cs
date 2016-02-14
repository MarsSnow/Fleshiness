using UnityEngine;
using System.Collections;

public class JoyStickController : MonoBehaviour {

	// Use this for initialization
	private void Start () 
    {
        MessageManager.AddListener(MsgType.GameScene.UnBindJoyStick, UnBindingJoyStick);    
	}

    private void UnBindingJoyStick(Message msg)
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

	// Update is called once per frame
	private void Update () {
	
	}

    private void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    //cosnt
    private const int kStep = 10;
    private const string kJoystickName = "Joystick";

    private void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != kJoystickName)
        {
            Debug.Log("not Joystick!!");
            return;
        }

        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionX != 0 || joyPositionY != 0)
        {
            MessageManager.Dispatch(MsgType.GameScene.SetPosition, new Vector3(joyPositionX * kStep, joyPositionY * kStep, 0));
        }
    }

    private void OnJoystickMoveEnd(MovingJoystick move)
    {
        if (move.joystickName == kJoystickName)
        {
            Debug.Log("JoystickMoveEnd!");
        }
    }
}
