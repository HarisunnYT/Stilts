using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : Panel
{
    [SerializeField]
    private GameObject difficultyButton;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject warningPrompt;

    protected override void OnShow()
    {
        difficultyButton.SetActive(PlayerPrefs.GetInt("EasyPressed", 0) == 0);

        warningPrompt.SetActive(false);
        buttons.SetActive(true);

        Cursor.visible = true;
    }

    protected override void OnClose()
    {
        Cursor.visible = false;
    }

    public void HideDifficultyButton()
    {
        difficultyButton.SetActive(false);
        PlayerPrefs.SetInt("EasyPressed", 1);
    }

    public void TryQuit()
    {
        warningPrompt.SetActive(true);
        buttons.SetActive(false);
    }

    public void ActuallyQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        warningPrompt.SetActive(false);
        buttons.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            Close();
    }
}
