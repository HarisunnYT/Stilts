using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [SerializeField]
    private GameObject continueObj;

    [SerializeField]
    private Button customLevelsButton;

    protected override void OnShow()
    {
        continueObj.SetActive(SaveManager.Instance.HasSavedData(SaveManager.CampaignMapName));
        try
        {
            bool b = SteamClient.IsLoggedOn;
        }
        catch (System.Exception e)
        {
            customLevelsButton.interactable = false;
        }
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
