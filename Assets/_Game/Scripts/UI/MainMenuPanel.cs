using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : Panel
{
    [SerializeField]
    private Animator player;

    [SerializeField]
    private Transform leftEyeball;

    [SerializeField]
    private Transform rightEyeball;

    [SerializeField]
    private GameObject continueObj;

    [SerializeField]
    private GameObject cursorCollider;

    [SerializeField]
    private Button customLevelsButton;

    [SerializeField]
    private TMP_Text newGameText;

    private Camera camera;

    private float currentExpression;
    private float targetExpression;

    private const float expressionChangeSpeed = 5.0f;

    private bool clickedNewGame = false;

    private void Start()
    {
        camera = Camera.main;
    }

    protected override void OnShow()
    {
        player.gameObject.SetActive(true);
        continueObj.SetActive(SaveManager.Instance.HasSavedData(SaveManager.CampaignMapName));

        newGameText.text = "New Game";
        clickedNewGame = false;

        Cursor.visible = true;

#if UNITY_ANDROID || UNITY_IOS
        customLevelsButton.gameObject.SetActive(false);
#else
        try
        {
            bool b = SteamClient.IsLoggedOn;
        }
        catch (System.Exception e)
        {
            customLevelsButton.interactable = false;
        }
#endif

        ExitedNewGameButton();
    }

    protected override void OnClose()
    {
        player.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        cursorCollider.transform.position = target;

        currentExpression = Mathf.Lerp(currentExpression, targetExpression, expressionChangeSpeed * Time.deltaTime);
        player.SetFloat("Expression", currentExpression);
    }

    private void LateUpdate()
    {
        Vector3 target = camera.ScreenToWorldPoint(Input.mousePosition);
        target.z = -15;
        Vector3 dir = target - leftEyeball.transform.position;
        leftEyeball.rotation = Quaternion.FromToRotation(-Vector3.right, dir);
        dir = target - rightEyeball.transform.position;
        rightEyeball.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }

    public void Continue()
    {
        SaveManager.Instance.SetCurrentMap(SaveManager.CampaignMapName);
        GameManager.Instance.LoadScene("Game");
    }

    public void NewGame()
    {
        if (SaveManager.Instance.HasSavedData(SaveManager.CampaignMapName) && !clickedNewGame)
        {
            clickedNewGame = true;
            newGameText.text = "Are you sure?";
        }
        else
        {
            SaveManager.Instance.SetCurrentMap(SaveManager.CampaignMapName);
            SaveManager.Instance.ClearSavedData(SaveManager.CampaignMapName);
            GameManager.Instance.LoadScene("Game");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CustomLevels()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        AssetBundleLoader.Instance.LoadBundles();
#endif
    }

    public void ExitedNewGameButton()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        newGameText.text = "New Game";
        clickedNewGame = false;
        EventSystem.current.SetSelectedGameObject(null);
#endif
    }

    public void SetExpression(float exp)
    {
        targetExpression = exp;
    }
}
