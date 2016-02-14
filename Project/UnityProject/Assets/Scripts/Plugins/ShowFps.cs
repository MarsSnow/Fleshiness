using UnityEngine;
using System.Collections;

public class ShowFps : MonoBehaviour {
    private const float kUpdateInterval = 0.5f;
    private float m_lastUpdateTime = 0;
    private float m_frameCount = 0;
    private string m_text;
    private GUIStyle m_style;
    private Rect m_rect;
    void Start()
    {
        m_lastUpdateTime = Time.realtimeSinceStartup;

        m_style = new GUIStyle();
        m_rect = new Rect(10, Screen.height/2, 100, 20);
        m_style.alignment = TextAnchor.UpperLeft;
        m_style.fontSize = 20;
    }

    private void SetFpsColor(float fps)
    {
        if (fps < 10)
        {
            m_style.normal.textColor = Color.red;
        }
        else if (fps < 30)
        {
            m_style.normal.textColor = Color.yellow;
        }
        else
        {
            m_style.normal.textColor = Color.green;
        }
    }
    void Update()
    {
        m_frameCount++;
        float timeNow = Time.realtimeSinceStartup;
        float det = timeNow - m_lastUpdateTime;
        if (det >= kUpdateInterval)
        {
            float fps = m_frameCount / det;
            SetFpsColor(fps);
            m_text = string.Format("{0:0.0} fps", fps); 
            m_lastUpdateTime = timeNow;
            m_frameCount = 0;
        }
    }

    void OnGUI()
    {
        GUI.Label(m_rect, m_text, m_style);
    }
}
