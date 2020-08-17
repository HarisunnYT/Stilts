using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : Panel
{
    [SerializeField]
    private GameObject continueObj;

    protected override void OnShow()
    {
        continueObj.SetActive(SaveManager.Instance.HasSavedData(SaveManager.CampaignMapName));
    }

    public void Continue()
    {
        SaveManager.Instance.SetCurrentMap(SaveManager.CampaignMapName);
        SceneManager.LoadScene("Game");
    }

    public void NewGame()
    {
        SaveManager.Instance.SetCurrentMap(SaveManager.CampaignMapName);
        SaveManager.Instance.ClearSavedData(SaveManager.CampaignMapName);
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CustomLevels()
    {
        AssetBundleLoader.Instance.LoadBundles();
    }
}
