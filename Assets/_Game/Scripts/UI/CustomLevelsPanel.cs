using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelsPanel : Panel
{
    [SerializeField]
    private DynamicContentController contentController;

    private void Start()
    {
        foreach(var sceneName in AssetBundleLoader.Instance.LoadedScenes)
        {
            LevelCell cell = contentController.AddToFront().GetComponent<LevelCell>();
            cell.Configure(sceneName);
        }
    }
}
