using Steamworks;
using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementManager
{
    public static Achievement GetAchievement(string achievementName)
    {
        foreach(var achievement in SteamUserStats.Achievements)
        {
            if (achievement.Name == achievementName)
                return achievement;
        }

        return default;
    }

    public static void CompleteAchievement(string achievementName)
    {
        foreach (var achievement in SteamUserStats.Achievements)
        {
            if (achievement.Identifier == achievementName)
                achievement.Trigger();
        }
    }
}
