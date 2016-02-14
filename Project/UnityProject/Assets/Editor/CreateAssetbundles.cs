using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CreateAssetbundles : EditorWindow 
{
    [MenuItem("Custom Editor/Create AssetBundles Main")]
    private static void CreateAssetBundlesMain()
    {
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object obj in SelectedAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);
            string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
            bool isSucess = BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies);
            //bool isSucess = BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
            //bool isSucess = BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
            if (isSucess)
            {
                Debug.Log(obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log(obj.name + "资源打包失败");
            }
            AssetDatabase.Refresh();
        }
    }

    [MenuItem("Custom Editor/Create AssetBunldes ALL")]
    static void CreateAssetBunldesALL()
    {

        Caching.CleanCache();

        string Path = Application.dataPath + "/StreamingAssets/ALL.assetbundle";

        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            Debug.Log("Create AssetBunldes name :" + obj);
        }

        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies))
        {
            AssetDatabase.Refresh();
        }
        else
        {

        }
    }

}

