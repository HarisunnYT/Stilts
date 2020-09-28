using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardCell : MonoBehaviour
{
    [SerializeField]
    private TMP_Text podiumPositionText;

    [SerializeField]
    private TMP_Text playerNameText;

    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private Image bgImage;

    public void Configure(int podiumPosition, string playerName, int time, Color bgColor)
    {
        podiumPositionText.text = podiumPosition.ToString();
        playerNameText.text = playerName;

        timeText.text = Util.ConvertFloatToTime((float)time / 100);
        bgImage.color = bgColor;
    }
}
