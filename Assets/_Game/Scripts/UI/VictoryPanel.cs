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

    protected override void OnShow()
    {
        Cursor.visible = true;

        TimeSpan time = TimeSpan.FromSeconds(SaveManager.Instance.GetTimePlayed() + MovementController.Instance.TimePlayed);
        StringBuilder str = new StringBuilder();

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

        SaveManager.Instance.ClearSavedData(SaveManager.Instance.CurrentMap);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
