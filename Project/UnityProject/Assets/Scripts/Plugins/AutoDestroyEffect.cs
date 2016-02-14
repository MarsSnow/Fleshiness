using UnityEngine;
using System.Collections;

public class AutoDestroyEffect : MonoBehaviour 
{
    private void OnAnimationEnd()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnAnimationEndParent()
    {
        GameObject.Destroy(this.transform.parent.gameObject);
    }

    private void OnAnimationEndDisable()
    {
        this.gameObject.SetActive(false);
    }

    private void OnAnimationEndDisableParent()
    {
        Transform parentTrans = this.transform.parent;
        if (parentTrans == null)
        {
            return;
        }

        parentTrans.gameObject.SetActive(false);
    }
}
