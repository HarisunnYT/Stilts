using DanielLochner.Assets.SimpleScrollSnap;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelsPanel : Panel
{
    [SerializeField]
    private GameObject player;

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
        player.SetActive(false);

        foreach (var workshopItem in AssetBundleLoader.Instance.LoadedWorkshopItems)
        {
            bool exists = false;
            for (int i = 0; i < scroller.Panels.Count; i++)
            {
                if (scroller.Panels[i].GetComponent<LevelCell>().LevelName == workshopItem.LevelName)
                    exists = true;
            }

            if (!exists)
                CreateLevelCell(workshopItem);
        }

        for (int i = 0; i < scroller.Panels.Count; i++)
        {
            bool needsDeleting = true;
            foreach (var workshopItem in AssetBundleLoader.Instance.LoadedWorkshopItems)
            {
                if (scroller.Panels[i].GetComponent<LevelCell>().LevelName == workshopItem.LevelName)
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

    private void CreateLevelCell(AssetBundleLoader.WorkshopItem workshopItem)
    {
        Instantiate(togglePrefab, scroller.pagination.transform.position + new Vector3(10 * (scroller.NumberOfPanels + 1), 0, 0), Quaternion.identity, scroller.pagination.transform);
        scroller.pagination.transform.position -= new Vector3(10 / 2f, 0, 0);

        GameObject obj = scroller.Add(levelCellPrefab, 0);
        obj.GetComponent<LevelCell>().Configure(workshopItem);
    }

    public void OpenWorkshop()
    {
        SteamFriends.OpenWebOverlay("https://steamcommunity.com/app/1394510/workshop/");
    }
}
