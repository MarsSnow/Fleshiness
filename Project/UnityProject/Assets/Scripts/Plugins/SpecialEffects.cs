using UnityEngine;
using System.Collections;

public class SpecialEffects : MonoBehaviour
{
    private RenderTexture m_renderTexture = null;

    public Camera OutlineCamera = null;

    public Material OutlineMaterial = null;

    public Material BlurMaterial = null;

    public Material CompositeMaterial = null;

    public Color OutlineColor = new Color(0, 0, 1, 1);

    public float OutlineWidth = 2.0f;
    public float FadeWidth = 2.0f;

    void Start()
    {
        m_renderTexture = new RenderTexture((int)this.camera.pixelWidth, (int)this.camera.pixelHeight, 16);
        m_renderTexture.hideFlags = HideFlags.DontSave;
    }

    //The outLine camera's fieldOfView should be the same to the maincamera's
    //otherwise, the outline effect should be error
    private void LateUpdate()
    {
        if (OutlineCamera == null)
        {
            return;
        }
     
        OutlineCamera.fieldOfView = Camera.main.fieldOfView;
    }

    private Material m_renderMat = null;
    private Material RenderMat
    {
        get
        {
            if (m_renderMat == null)
            {
                m_renderMat = new Material("Shader\"Hidden/SolidBody1\"{SubShader{Pass{Color(1,1,1,1)}}}");
                m_renderMat.hideFlags = HideFlags.HideAndDontSave;
                m_renderMat.shader.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_renderMat;
        }
    }


    void OnPreRender()
    {
        OutlineCamera.targetTexture = m_renderTexture;
        OutlineCamera.RenderWithShader(RenderMat.shader, "");
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
        RenderTexture bufferFull = RenderTexture.GetTemporary(m_renderTexture.width/2, m_renderTexture.height/2, 0);
        // 双方向模糊算法
        BlurMaterial.SetVector("offsets", new Vector4(0.0f, FadeWidth * 1.0f / 512.0f, 0.0f, 0.0f));
        Graphics.Blit(m_renderTexture, m_renderTexture, BlurMaterial);

        BlurMaterial.SetVector("offsets", new Vector4(FadeWidth / 1.25f * 1.0f / 512.0f, 0.0f, 0.0f, 0.0f));
        Graphics.Blit(m_renderTexture, m_renderTexture, BlurMaterial);

        // 获得通道梯度并压缩
        Graphics.BlitMultiTap(m_renderTexture, bufferFull, OutlineMaterial, new Vector2(OutlineWidth, OutlineWidth));

        // 融合像素
        Graphics.Blit(source, destination);
        CompositeMaterial.SetColor("_OutlineColor", OutlineColor);
        Graphics.Blit(bufferFull, destination, CompositeMaterial);

        RenderTexture.ReleaseTemporary(bufferFull);
	}
}