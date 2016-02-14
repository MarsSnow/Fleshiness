using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos = transform.position - cameraPos;
        transform.LookAt(cameraPos, Vector3.up);
    }
}
