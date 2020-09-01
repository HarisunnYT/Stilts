using Steamworks;
using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementManager
{
#if !UNITY_ANDROID && !UNITY_IOS
    public static Achievement GetAchievement(string achievementName)
    {
        foreach(var achievement in SteamUserStats.Achievements)
        {
            if (achievement.Name == achievementName)
                return achievement;
        }

        return default;
    }
#endif

    public static void CompleteAchievement(string achievementName)
    {
#if !UNITY_ANDROID && !UNITY_IOS
        try
        {
            foreach (var achievement in SteamUserStats.Achievements)
            {
                if (achievement.Identifier == achievementName)
                    achievement.Trigger();
            }
        }
        catch (System.Exception e) { }
#endif
    }
}
