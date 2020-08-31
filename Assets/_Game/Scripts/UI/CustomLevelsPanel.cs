using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelsPanel : Panel
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private LevelCell levelCellPrefab;

    [SerializeField]
    private Transform levelCellContent;

    [SerializeField]
    private GameObject message;

    private List<LevelCell> panels = new List<LevelCell>();

    protected override void OnShow()
    {
        player.SetActive(false);

        foreach (var workshopItem in AssetBundleLoader.Instance.LoadedWorkshopItems)
        {
            bool exists = false;
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].GetComponent<LevelCell>().LevelName == workshopItem.LevelName)
                    exists = true;
            }

            if (!exists)
                CreateLevelCell(workshopItem);
        }

        for (int i = 0; i < panels.Count; i++)
        {
            bool needsDeleting = true;
            foreach (var workshopItem in AssetBundleLoader.Instance.LoadedWorkshopItems)
            {
                if (panels[i].GetComponent<LevelCell>().LevelName == workshopItem.LevelName)
                    needsDeleting = false;
            }

            if (needsDeleting)
                panels.RemoveAt(i);
        }

        message.SetActive(panels.Count == 0);
    }

    public void Refresh()
    {
        AssetBundleLoader.Instance.Refresh();
    }

    private void CreateLevelCell(AssetBundleLoader.WorkshopItem workshopItem)
    {
        LevelCell levelCell = Instantiate(levelCellPrefab, levelCellContent);
        levelCell.Configure(workshopItem);

        panels.Add(levelCell);
    }

    public void OpenWorkshop()
    {
        SteamFriends.OpenWebOverlay("https://steamcommunity.com/app/1394510/workshop/");
    }
}
