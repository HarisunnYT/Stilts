using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelsPanel : Panel
{
    [SerializeField]
    private GameObject levelCellPrefab;

    [SerializeField]
    private GameObject togglePrefab;

    [SerializeField]
    private SimpleScrollSnap scroller;

    [SerializeField]
    private Transform levelCellContent;

    protected override void OnShow()
    {
        foreach (var sceneName in AssetBundleLoader.Instance.LoadedScenes)
        {
            bool exists = false;
            for (int i = 0; i < scroller.Panels.Count; i++)
            {
                if (scroller.Panels[i].GetComponent<LevelCell>().LevelName == sceneName)
                    exists = true;
            }

            if (!exists)
                CreateLevelCell(sceneName);
        }

        for (int i = 0; i < scroller.Panels.Count; i++)
        {
            bool needsDeleting = true;
            foreach (var sceneName in AssetBundleLoader.Instance.LoadedScenes)
            {
                if (scroller.Panels[i].GetComponent<LevelCell>().LevelName == sceneName)
                    needsDeleting = false;
            }

            if (needsDeleting)
                scroller.Remove(i);
        }
    }

    public void Refresh()
    {
        AssetBundleLoader.Instance.Refresh();
    }

    private void CreateLevelCell(string sceneName)
    {
        Instantiate(togglePrefab, scroller.pagination.transform.position + new Vector3(10 * (scroller.NumberOfPanels + 1), 0, 0), Quaternion.identity, scroller.pagination.transform);
        scroller.pagination.transform.position -= new Vector3(10 / 2f, 0, 0);

        GameObject obj = scroller.Add(levelCellPrefab, 0);
        obj.GetComponent<LevelCell>().Configure(sceneName);
    }
}
