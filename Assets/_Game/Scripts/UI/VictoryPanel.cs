using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : Panel
{
    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private GameObject backButton; 

    public override void Initialise()
    {
        CompleteTrigger.Instance.OnComplete += ShowPanel;
    }

    private void OnDestroy()
    {
        if (CompleteTrigger.Instance)
            CompleteTrigger.Instance.OnComplete -= ShowPanel;
    }

    public void ShowTime()
    {
        StartCoroutine(ShowTimeIE());
    }

    private IEnumerator ShowTimeIE()
    {
        float completeTime = SaveManager.Instance.GetTimePlayed() + MovementController.Instance.TimePlayed;

        float increment = completeTime / 100;
        float counter = 0;

        TimeSpan time;
        StringBuilder str = new StringBuilder();

        while (counter < completeTime)
        {
            str.Clear();
            counter += increment;

            time = TimeSpan.FromSeconds(counter);

            if (time.Hours > 0)
            {
                str.Append(time.Hours);
                str.Append("h ");
            }

            if (time.Minutes > 0)
            {
                str.Append(time.Minutes);
                str.Append("m ");
            }

            str.Append(time.Seconds);
            str.Append("s");

            timeText.text = str.ToString();

            yield return new WaitForSeconds(0.01f);
        }

        Cursor.visible = true;
        backButton.SetActive(true);
    }

    public void Back()
    {
        SaveManager.Instance.ClearSavedData(SaveManager.Instance.CurrentMap);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.ClearSavedData(SaveManager.Instance.CurrentMap);
    }
}
