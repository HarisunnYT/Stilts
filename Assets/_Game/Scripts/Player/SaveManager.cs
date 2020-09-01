using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : PersistentSingleton<SaveManager>
{
    public string CurrentMap { get; private set; }

    public const string CampaignMapName = "campaign_map_main";

    public void SavePosition(Vector3 pos)
    {
        PlayerPrefs.SetFloat(CurrentMap + "_x", pos.x);
        PlayerPrefs.SetFloat(CurrentMap + "_y", pos.y);

        PlayerPrefs.Save();
    }

    public void SaveTime(float timePlayed)
    {
        PlayerPrefs.SetFloat(CurrentMap + "_time_played", PlayerPrefs.GetFloat(CurrentMap + "_time_played") + timePlayed);
    }

    public Vector3 LoadCheckpoint()
    {
        Vector3 vec = Vector3.zero;
        vec.x = PlayerPrefs.GetFloat(CurrentMap + "_x");
        vec.y = PlayerPrefs.GetFloat(CurrentMap + "_y");
        return vec;
    }

    public float GetTimePlayed()
    {
        return PlayerPrefs.GetFloat(CurrentMap + "_time_played");
    }

    public bool HasSavedData(string mapName)
    {
        return PlayerPrefs.HasKey(mapName + "_x");
    }

    public void ClearSavedData(string mapName)
    {
        PlayerPrefs.DeleteKey(mapName + "_x");
        PlayerPrefs.DeleteKey(mapName + "_y");
        PlayerPrefs.DeleteKey(CurrentMap + "_time_played");
        PlayerPrefs.Save();
    }

    public void SetCurrentMap(string mapName)
    {
        CurrentMap = mapName;
    }

    public void CommunityLevelPlayed(string mapName)
    {
#if !UNITY_ANDROID && !UNITY_IOS
        if (!PlayerPrefs.HasKey(mapName + "_played"))
        {
            PlayerPrefs.SetString(mapName + "_played", "true");
            SteamUserStats.AddStat("community_levels_played", 1);

            if (SteamUserStats.GetStatInt("community_levels_played") > 5)
                AchievementManager.CompleteAchievement("played_5_comm_levels");

            if (mapName.Contains(SteamClient.SteamId.Value.ToString()))
                AchievementManager.CompleteAchievement("play_own_level");
        }
#endif
    }
}
