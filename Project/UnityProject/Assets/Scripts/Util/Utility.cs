using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utility : MonoBehaviour
{
    public const float kValidValue = 0.02f;

    public static GameObject FindGameObjectByTag(Transform root, string tag)
    {
        if (root.tag == tag)
        {
            return root.gameObject;
        }

        foreach (Transform t in root)
        {
            if (tag == t.tag)
            {
                return t.gameObject ;
            }
        }
        return null;
    }

	public static Transform FindTransform(Transform root,string name)
	{
		if(root.name == name)
		{
			return root;
		}
		
		for(int i = 0; i < root.childCount; i++)
		{
			Transform tr = FindTransform(root.GetChild(i).transform,name);
			if(tr != null)
				return tr;
		}
		
		return null;
	}

    public static List<GameObject> FindObjects(Transform root, string name)
    {
        List<GameObject> list = new List<GameObject>(); 
        for (int i = 0; i < root.childCount; i++)
        {
            Transform tr = FindTransform(root.GetChild(i).transform,name);
            if (tr != null)
                list.Add(tr.gameObject);
        }
        return list;
    }

	/// <summary>
    /// 设定本节点及其子节点渲染标记层
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="layer"></param>
    public static void SetLayer(GameObject obj, int layer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = layer;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayer(obj.transform.GetChild(i).gameObject, layer);
        }
    }
	
	public static void GetAvatarAllAnimator(Transform avatar, ref List<Animator> lstAnimator)
    {
        if (avatar == null || lstAnimator == null)
        {
            return;
        }

        Animator aniCtrl = avatar.GetComponent<Animator>();
		if (aniCtrl != null)
		{
			lstAnimator.Add(aniCtrl);
		}
		
        for (int i = 0; i < avatar.transform.childCount; i++)
        {
            GetAvatarAllAnimator(avatar.transform.GetChild(i), ref lstAnimator);
        }

        return;
    }

    public static void GetAvatarAllCollider(Transform avatar, ref List<Collider> lstCollider)
    {
        if (avatar == null || lstCollider == null)
        {
            return;
        }

        Collider aniCtrl = avatar.GetComponent<Collider>();
        if (aniCtrl != null)
        {
            lstCollider.Add(aniCtrl);
        }

        for (int i = 0; i < avatar.transform.childCount; i++)
        {
            GetAvatarAllCollider(avatar.transform.GetChild(i), ref lstCollider);
        }
    }
	
	public static void GetAvatarAllParticleSystem(Transform avatar, ref List<ParticleSystem> lstParticleSystem)
    {
        if (avatar == null || lstParticleSystem == null)
        {
            return;
        }

        ParticleSystem particleSystem = avatar.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            lstParticleSystem.Add(particleSystem);
        }

        for (int i = 0; i < avatar.transform.childCount; i++)
        {
            GetAvatarAllParticleSystem(avatar.transform.GetChild(i), ref lstParticleSystem);
        }

        return;
    }

    public static void GetAvatarAllMeshRenderer(Transform avatar, ref List<MeshRenderer> lstMeshRenderer)
    {
        if (avatar == null || lstMeshRenderer == null)
        {
            return;
        }

        MeshRenderer meshRenderer = avatar.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            lstMeshRenderer.Add(meshRenderer);
        }

        for (int i = 0; i < avatar.transform.childCount; i++)
        {
            GetAvatarAllMeshRenderer(avatar.transform.GetChild(i), ref lstMeshRenderer);
        }

        return;
    }

    public enum ShakeType
    {
        XType,
        YType,
        ZType,
    }

    public static Vector3 IgnoreY(Vector3 vec)
    {
        return new Vector3(vec.x, 0, vec.z);
    }

    public static Vector3 IgnoreZ(Vector3 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }
	
	public static void ResetParticleSystemTime(GameObject obj, float time)
    {
        ParticleSystem particleSystem = obj.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Simulate(time);
        }

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform tran = obj.transform.GetChild(i).transform;
            ResetParticleSystemTime(tran.gameObject, time);
        }

    }

    /// <summary>
    /// 动态生成Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <param name="atlasName"></param>
    public static void DynamicCreateSprite(string path, string atlasName)
    {
        UIAtlas atlas = Resources.Load(path + "/" + atlasName, typeof(UIAtlas)) as UIAtlas;
        UISpriteData uiSpriteData = new UISpriteData();
        UnityEngine.Object[] textures = Resources.LoadAll(path, typeof(Texture2D));
        foreach (Texture tex in textures)
        {
            atlas.spriteMaterial.mainTexture = tex;
            uiSpriteData.name = "DynamicSprite";
            uiSpriteData.SetRect(0, 0, tex.width, tex.height);

            atlas.spriteList.Clear();
            atlas.spriteList.Add(uiSpriteData);
        }
    }

	/// <summary>
    /// 动态生成Sprite
    /// </summary>
    /// <param name="uiSpriteName"></param>
    /// <param name="path"></param>
    /// <param name="atlasName"></param>
    public static void DynamicCreateSprite(string uiSpriteName, string path, string atlasName)
    {
        UIAtlas atlas = Resources.Load(path + "/" + atlasName, typeof(UIAtlas)) as UIAtlas;
        UISpriteData uiSpriteData = new UISpriteData();
        UnityEngine.Object[] textures = Resources.LoadAll(path, typeof(Texture2D));
        foreach (Texture tex in textures)
        {
            atlas.spriteMaterial.mainTexture = tex;
            uiSpriteData.name = "DynamicSprite";
            uiSpriteData.SetRect(0, 0, tex.width, tex.height);

            atlas.spriteList.Clear();
            atlas.spriteList.Add(uiSpriteData);
        }
        UISprite uiSprite = GameObject.Find(uiSpriteName).GetComponent<UISprite>();
        uiSprite.spriteName = "DynamicSprite";
    }

    /// <summary>
    /// 动态生成Sprite
    /// </summary>
    /// <param name="rootGameObject"></param>
    /// <param name="uiSpriteGameObjectName"></param>
    /// <param name="atlasPath"></param>
    /// <param name="atlasName"></param>
    public static void DynamicCreateSprite(GameObject rootGameObject, string uiSpriteGameObjectName, string atlasPath, string atlasName)
    {
        UIAtlas atlas = Resources.Load(atlasPath + "/" + atlasName, typeof(UIAtlas)) as UIAtlas;
        UISpriteData uiSpriteData = new UISpriteData();
        UnityEngine.Object[] textures = Resources.LoadAll(atlasPath, typeof(Texture2D));
        foreach (Texture tex in textures)
        {
            atlas.spriteMaterial.mainTexture = tex;
            uiSpriteData.name = "DynamicSprite";
            uiSpriteData.SetRect(0, 0, tex.width, tex.height);

            atlas.spriteList.Clear();
            atlas.spriteList.Add(uiSpriteData);
        }
        SetDynamicSprite(rootGameObject, uiSpriteGameObjectName);
    }

    public static void DynamicCreateSprite(GameObject rootGameObject, string uiSpriteGameObjectName, string atlasPath, string atlasName, string picturePath)
    {
        UIAtlas atlas = Resources.Load(atlasPath + "/" + atlasName, typeof(UIAtlas)) as UIAtlas;

        UISpriteData uiSpriteData = new UISpriteData();

        UnityEngine.Object textureObject = Resources.Load(picturePath, typeof(Texture2D));
        Texture texture = (Texture)textureObject;
        atlas.spriteMaterial.mainTexture = texture;
        uiSpriteData.name = "DynamicSprite";
        uiSpriteData.SetRect(0, 0, texture.width, texture.height);

        atlas.spriteList.Clear();
        atlas.spriteList.Add(uiSpriteData);
  
        SetDynamicSprite(rootGameObject, uiSpriteGameObjectName);
    }

    public static void SetDynamicSprite(GameObject rootGameObject, string uiSpriteGameObjectName)
    {
        UISprite uiSprite = FindTransform(rootGameObject.transform, uiSpriteGameObjectName).GetComponent<UISprite>();
        uiSprite.spriteName = "DynamicSprite";
    }

	/// <summary>
    /// 设置GameObject所有层级的layer
    /// </summary>
    public static void SetLayer(GameObject rooot, string layerName)
    {
        Transform[] childs = rooot.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in childs)
        {
            t.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

    /// <summary>
    /// 复位GameObject
    /// </summary>
    public static void ResetGameObject(GameObject m_gameObject)
    {
        m_gameObject.transform.localPosition = Vector3.zero;
        m_gameObject.transform.localEulerAngles = Vector2.zero;
        m_gameObject.transform.localScale = Vector3.one;
    }

    

    public static void ResetGameObject(GameObject father, GameObject m_gameObject)
    {
        m_gameObject.transform.parent = father.transform;
        ResetGameObject(m_gameObject);
    }

    public static void ResetGameObject(GameObject father, GameObject m_gameObject, string layer)
    {
        ResetGameObject(father, m_gameObject);
        SetLayer(m_gameObject, layer);
    }

    
    public static float GetSqrDistance(Vector3 posA, Vector3 posB)
    {
        return Vector3.SqrMagnitude(posB - posA);
    }

    public static void AddChild(GameObject parent, GameObject child)
    {
        child.transform.parent = parent.transform;
        //child.transform.localPosition = Vector3.zero;
    }

    public static bool TransValueList(string content, out List<int> valueList, string separator = "|")
    {
        valueList = null;
        string[] contentArray = content.Split(separator.ToCharArray());
        if (contentArray.Length == 0)
        {
            return false;
        }
        valueList = new List<int>();
        foreach (string subContent in contentArray)
        {
            if (string.IsNullOrEmpty(subContent))
            {
                continue;
            }
            valueList.Add(int.Parse(subContent));
        }

        return true;
    }

    public static bool TransValueList(string content, out List<float> valueList, string separator = "|")
    {
        valueList = null;
        string[] contentArray = content.Split(separator.ToCharArray());
        if (contentArray.Length == 0)
        {
            return false;
        }
        valueList = new List<float>();
        foreach (string subContent in contentArray)
        {
            valueList.Add(float.Parse(subContent));
        }

        return true;
    }

    public static bool TransValueList(string content, out List<string> valueList, string separator = "|")
    {
        valueList = null;
        string[] contentArray = content.Split(separator.ToCharArray());
        if (contentArray.Length == 0)
        {
            return false;
        }

        valueList = new List<string>(contentArray);
        return true;
    }

    public static GameObject LoadImgToGameObject(string iconPatch, Transform parentTrans, 
        int depth, float widthScale, float heightScale, int width = -1, int height = -1)
    {
        //Texture2D tex = BundleManager.instance.LoadResource(iconPatch) as Texture2D;
        Texture2D tex = Resources.Load(iconPatch) as Texture2D;
        GameObject icon = new GameObject();
        UISprite iconSp = icon.AddComponent<UISprite>();
        UIAtlas atlas = ImageLoad(icon, tex);
        iconSp.atlas = atlas;
        iconSp.spriteName = tex.name;

        iconSp.transform.parent = parentTrans;

        icon.transform.localPosition = new Vector3(0, 0, 0);
        icon.transform.localScale = new Vector3(widthScale, heightScale, 1);
        iconSp.depth = depth;
        if (width != -1 && height != -1)
        {
            iconSp.width = width;
            iconSp.height = height;
        }

        return icon;
    }

    //DynamicCreatImageObject
    public static GameObject DynamicCreatImageObject(string iconPatch, Transform parentTrans,
    int depth, float widthScale, float heightScale, int width = -1, int height = -1, string objName = "DynamicCreatImageObject", string layer = UnityConfig.UILayer, bool hasCollider = false)
    {
        Texture2D tex = Resources.Load(iconPatch) as Texture2D;
     
        GameObject icon = new GameObject();
        UISprite iconSp = icon.AddComponent<UISprite>();
        UIAtlas atlas = ImageLoad(icon, tex);
        iconSp.atlas = atlas;
        iconSp.spriteName = tex.name;

        iconSp.transform.parent = parentTrans;

        icon.transform.localEulerAngles = Vector2.zero;
        icon.transform.localPosition = Vector2.zero;
        icon.transform.localScale = new Vector3(widthScale, heightScale, 1);

        ResetGameObject(icon);

        icon.name = objName;
        SetLayer(icon, layer);
        iconSp.depth = depth;
        if (width != -1 && height != -1)
        {
            iconSp.width = width;
            iconSp.height = height;
        }
        else
        {
            iconSp.width = tex.width;
            iconSp.height = tex.height;
        }

        if(hasCollider)
        {
            BoxCollider2D boxCollider = icon.GetComponent<BoxCollider2D>();
            if(boxCollider == null)
            {
                boxCollider = icon.AddComponent<BoxCollider2D>();
            }
            boxCollider.size = new Vector2(iconSp.width, iconSp.height);
        }

        return icon;
    }

    public static UIAtlas ImageLoad(GameObject obj, Texture2D tex)
    {
        UIAtlas uiAtlas = null;
        if (tex == null)
        {
            return uiAtlas;
        }

        //准备对象和材质球
        if (uiAtlas == null)
        {
            Material mat;
            Shader shader = Shader.Find("Unlit/Transparent Colored");
            mat = new Material(shader);
            uiAtlas = obj.AddComponent<UIAtlas>();
            //m_uiAtlas.name = tex.name;
            uiAtlas.spriteMaterial = mat;
        }
        //设定贴图
        uiAtlas.spriteMaterial.mainTexture = tex;
        //m_uiAtlas.coordinates = UIAtlas.Coordinates.Pixels;

        //为对应UISprite接口，给Atlas加对象
        UISpriteData sprite = new UISpriteData();
        sprite.name = tex.name;
        sprite.SetRect(0, 0, tex.width, tex.height);

        uiAtlas.spriteList.Clear();
        uiAtlas.spriteList.Add(sprite);
        //设置完成
        return uiAtlas;
    }

    public static void DeleteChildObjects(GameObject g)
    {
        Transform[] childs = g.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < childs.Length; i++)
        {
            Destroy(childs[i].gameObject);
        }
    }

    public static void DeletaChildObjectsImmediata(GameObject g)
    {
        Transform[] childs = g.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < childs.Length; i++)
        {
            DestroyImmediate(childs[i].gameObject);
        }
    }

    public static Vector3 ConvertPositionFromWorldToUI(Camera from, Camera toUI, Vector3 inPos)
    {
        Vector3 conPos = from.WorldToScreenPoint(inPos);
        conPos.z = 0;
        return toUI.ScreenToWorldPoint(conPos);
    }

    /// <summary>
    /// 通过List的值获取List的索引
    /// </summary>
    /// <typeparam name="T">List元素类型</typeparam>
    /// <param name="list">源List</param>
    /// <param name="value">List值</param>
    /// <returns>List索引</returns>
    public static int GetListIndexByValue<T>(List<T> list, int value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (value.Equals(list[i]))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 拷贝一份List，使其与源List无引用关系
    /// </summary>
    /// <typeparam name="T">List中元素类型</typeparam>
    /// <param name="sourceList">源List</param>
    /// <returns>返回List</returns>
    public static List<T> CopyListToAnother<T>(List<T> sourceList)
    {
        List<T> targetList = new List<T>();
        for (int i = 0; i < sourceList.Count; i++)
        {
            targetList.Add(sourceList[i]);
        }
        return targetList;
    }

    /// <summary>
    /// 比较两个List是否一致（包括顺序）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="aList"></param>
    /// <param name="bList"></param>
    /// <returns></returns>
    public static bool IsListSame<T>(List<T> aList, List<T> bList)
    {
        if (aList.Count != bList.Count)
        {
            return false;
        }
        for (int i = 0; i < aList.Count; ++i)
        {
            if (!(aList[i].Equals(bList[i])))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 比较两个List是否一致（不包含顺序）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="aList"></param>
    /// <param name="bList"></param>
    /// <returns></returns>
    public static bool IsListContentSame<T>(List<T> aList, List<T> bList)
    {
        if (aList.Count != bList.Count)
        {
            return false;
        }

        int count = 0;
        for (int i = 0; i < aList.Count; i++)
        {
            if (bList.Contains(aList[i]))
            {
                count++;
            }
        }
        return count == aList.Count;
    }
}

