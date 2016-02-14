using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// @author 邱洪波
/// </summary>
public class SceneScale : MonoBehaviour 
{
    public Camera camera;
    public UIRoot root;

    public int uiWidth = 1152;
    public int uiHeight = 720;

    private float uiRatio = 1;

    private float screenScale
    {
        get
        {
            return (float)root.activeHeight * (float)uiWidth / ((float)uiHeight * ((float)root.activeHeight / (float)Screen.height * (float)Screen.width));
        }
    }

    private float screenRatio
    {
        get
        {
            return (float)Screen.width / (float)Screen.height;
        }
    }

    void Start()
    {
        
        //如果没有为公共变量赋值
        if (!camera || !root)
        {
            Transform p = transform;
            while (p.parent)
                p = p.parent;

            if (!camera)
                camera = p.GetComponentInChildren<UICamera>().camera;

            if (!root)
                root = p.GetComponent<UIRoot>();
        }

        uiRatio = (float)uiWidth / (float)uiHeight;
        if (screenRatio < uiRatio)
            camera.orthographicSize = screenScale;
        else
            camera.orthographicSize = 1;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (camera && root)
        {
            if (screenRatio < uiRatio)
                camera.orthographicSize = screenScale;
            else
                camera.orthographicSize = 1;
        }
    }

    void OnDestroy()
    {
        camera.orthographicSize = 1;
    }
#endif
}
