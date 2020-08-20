using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject continueObj;

    [SerializeField]
    private GameObject cursorCollider;

    [SerializeField]
    private Button customLevelsButton;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    protected override void OnShow()
    {
        player.SetActive(true);
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

    private void Update()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        cursorCollider.transform.position = new Vector3(target.x, target.y, 0);
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
