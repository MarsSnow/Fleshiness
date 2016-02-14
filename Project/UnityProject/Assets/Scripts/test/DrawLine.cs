using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

    public GameObject LineRenderGameObject = null;
    private  LineRenderer lineRenderer = null;
    private int lineLength = 4;

    private Vector3 v0 = new Vector3(10.0f, 0.0f, 0.0f);
    private Vector3 v1 = new Vector3(0.0f, 10.0f, 0.0f);
    private Vector3 v2 = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 v3 = new Vector3(10.0f, 0.0f, 0.0f);

    private void Start()
    {
        lineRenderer = LineRenderGameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(lineLength);

        lineRenderer.SetPosition(0, v0);
        lineRenderer.SetPosition(1, v1);
        lineRenderer.SetPosition(2, v2);
        lineRenderer.SetPosition(3, v3);
    }

    private void Update()
    {
        //lineRenderer.SetPosition(0, v0);
        //lineRenderer.SetPosition(1, v1);
        //lineRenderer.SetPosition(2, v2);
        //lineRenderer.SetPosition(3, v3);
    }
}
