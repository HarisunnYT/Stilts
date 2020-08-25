using Steamworks;
using Steamworks.Ugc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetBundleLoader : PersistentSingleton<AssetBundleLoader>
{
    public struct WorkshopItem
    {
        public Item Item;
        public string LevelName;
    }

    public List<WorkshopItem> LoadedWorkshopItems { get; private set; } = new List<WorkshopItem>();

    private List<AssetBundle> loadedBundles = new List<AssetBundle>();

    public void LoadBundles()
    {
        QueryLevels();
    }

    public void Refresh()
    {
        LoadBundles();
    }

    private async void QueryLevels()
    {
        foreach (var bundle in loadedBundles)
            bundle.Unload(true);

        loadedBundles.Clear();
        LoadedWorkshopItems.Clear();

        var query = Query.Items;

        for (int i = 1; i < 5; i++)
        {
            var task = await query.GetPageAsync(i);
            if (!task.HasValue) //no more pages
                break;

            StartCoroutine(LoadBundlesIE(task.Value.Entries));
        }
    }

    private IEnumerator LoadBundlesIE(IEnumerable<Item> items)
    {
        PanelManager.Instance.ShowPanel<LoadingBundlesPanel>();

        foreach (var item in items)
        {
            if (!string.IsNullOrEmpty(item.Directory))
            {
                var directory = new DirectoryInfo(item.Directory);
                if (directory.Exists)
                {
                    foreach (var file in directory.GetFiles())
                    {
                        //it means it could be an asset bundle file
                        if (string.IsNullOrEmpty(file.Extension))
                        {
                            AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(item.Directory, file.Name));
                            yield return bundleLoadRequest;

                            AssetBundle myLoadedAssetBundle = bundleLoadRequest.assetBundle;
                            loadedBundles.Add(myLoadedAssetBundle);

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
                                    LoadedWorkshopItems.Add(new WorkshopItem() { Item = item, LevelName = sceneName });
                                }
                            }
                        }
                    }
                }
            }
        }

        PanelManager.Instance.ShowPanel<CustomLevelsPanel>();
    }
}
