using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetBundleLoader : PersistentSingleton<AssetBundleLoader>
{
    public List<string> LoadedScenes { get; private set; } = new List<string>();

    private const string assetBundleName = "mod";

    private bool bundlesLoaded = false;

    public void LoadBundles()
    {
        if (bundlesLoaded)
            PanelManager.Instance.ShowPanel<CustomLevelsPanel>();
        else
            StartCoroutine(LoadBundlesIE());
    }

    private IEnumerator LoadBundlesIE()
    {
        PanelManager.Instance.ShowPanel<LoadingBundlesPanel>();

        var directory = new DirectoryInfo(Application.streamingAssetsPath);
        foreach(var file in directory.GetFiles())
        {
            //it means it could be an asset bundle file
            if (string.IsNullOrEmpty(file.Extension))
            {
                AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, file.Name));
                yield return bundleLoadRequest;

                AssetBundle myLoadedAssetBundle = bundleLoadRequest.assetBundle;
                if (myLoadedAssetBundle == null)
                {
                    Debug.LogError("Failed to load asset bundle");
                    yield break;
                }

                if (myLoadedAssetBundle.isStreamedSceneAssetBundle)
                {
                    string[] scenePaths = myLoadedAssetBundle.GetAllScenePaths();
                    foreach (var scenePath in scenePaths)
                    {
                        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                        LoadedScenes.Add(sceneName);
                    }
                }
            }
        }

        bundlesLoaded = true;
        PanelManager.Instance.ShowPanel<CustomLevelsPanel>();
    }
}
