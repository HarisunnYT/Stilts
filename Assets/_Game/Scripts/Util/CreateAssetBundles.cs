using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Tools/Build Asset Bundles")]
    private static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/WorkshopItem", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}

#endif
