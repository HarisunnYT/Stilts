using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Steamworks;
using Steamworks.Data;

public class VictoryPanel : Panel
{
    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private GameObject backButton;

    [SerializeField]
    private Transform leaderboardContent;

    [SerializeField]
    private LeaderboardCell leaderboardCell;

    [SerializeField]
    private LeaderboardCell playerCell;

    [SerializeField]
    private UnityEngine.Color playerCellColor;

    [SerializeField]
    private UnityEngine.Color[] bgColors;

    private float completeTime;

    public override void Initialise()
    {
        CompleteTrigger.Instance.OnComplete += ShowPanel;
    }

    private void OnDestroy()
    {
        if (CompleteTrigger.Instance)
            CompleteTrigger.Instance.OnComplete -= ShowPanel;
    }

    private void Start()
    {
        completeTime = SaveManager.Instance.GetTimePlayed() + MovementController.Instance.TimePlayed;

#if !UNITY_ANDROID && !UNITY_IOS
        LoadLeaderboard();
#endif
    }

#if !UNITY_ANDROID && !UNITY_IOS
    private async void LoadLeaderboard()
    {
        if (!SteamClient.IsLoggedOn)
            return;

        Steamworks.Data.Leaderboard? leaderboard = await SteamUserStats.FindOrCreateLeaderboardAsync(SaveManager.Instance.CurrentMap, Steamworks.Data.LeaderboardSort.Ascending, Steamworks.Data.LeaderboardDisplay.Numeric);
        if (leaderboard.HasValue)
        {
            int result = (int)(completeTime * 100);
            await leaderboard.Value.SubmitScoreAsync(result);

            bool isInTopFive = false;
            LeaderboardEntry[] entries = await leaderboard.Value.GetScoresAsync(5);
            if (entries != null)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    LeaderboardCell cell = Instantiate(leaderboardCell, leaderboardContent);
                    cell.Configure(i + 1, entries[i].User.Name, entries[i].Score, bgColors[i % 2 == 0 ? 0 : 1]);

                    if (entries[i].User.Id == SteamClient.SteamId)
                        isInTopFive = true;
                }
            }

            if (!isInTopFive)
            {
                LeaderboardEntry[] playerEntry = await leaderboard.Value.GetScoresAroundUserAsync(1, 1);
                foreach (var entry in playerEntry)
                {
                    if (entry.User.Id == SteamClient.SteamId)
                    {
                        playerCell.Configure(entry.GlobalRank, SteamClient.Name, entry.Score, playerCellColor);
                        break;
                    }
                }
            }
            else
                playerCell.gameObject.SetActive(false);
        }
    }
#endif

    public void ShowTime()
    {
        StartCoroutine(ShowTimeIE());
    }

    private IEnumerator ShowTimeIE()
    {
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

    public void Continue()
    {
        SaveManager.Instance.ClearSavedData(SaveManager.Instance.CurrentMap);
        PanelManager.Instance.ShowPanel<LeaderboardPanel>();
    }

    private void OnApplicationQuit()
    {
        SaveManager.Instance.ClearSavedData(SaveManager.Instance.CurrentMap);
    }
}
