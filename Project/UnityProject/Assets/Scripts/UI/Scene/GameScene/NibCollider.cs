using UnityEngine;
using System.Collections;

public class NibCollider : MonoBehaviour {

	private void Start () {
	
	}
	
	private void Update () {
	
	}

    private const string kBorderTagName = "Border";

    private void OnTriggerEnter(Collider e)
    {
        if (e.gameObject.tag.CompareTo(kBorderTagName) == 0)
        {
            Debug.LogError("Border！");
            MessageManager.Dispatch(MsgType.GameScene.UnBindJoyStick);    

        }
    }
}
