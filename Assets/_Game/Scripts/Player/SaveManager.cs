using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : PersistentSingleton<SaveManager>
{
    public string CurrentMap { get; private set; }

    public const string CampaignMapName = "campaign_map_main";

    public void Save(Vector3 pos, float timePlayed)
    {
        PlayerPrefs.SetFloat(CurrentMap + "_x", pos.x);
        PlayerPrefs.SetFloat(CurrentMap + "_y", pos.y);
        PlayerPrefs.SetFloat(CurrentMap + "_z", pos.z);
        PlayerPrefs.SetFloat(CurrentMap + "_time_played", PlayerPrefs.GetFloat(CurrentMap + "_time_played") + timePlayed);

        PlayerPrefs.Save();
    }

    public Vector3 LoadCheckpoint()
    {
        Vector3 vec = new Vector3();
        vec.x = PlayerPrefs.GetFloat(CurrentMap + "_x");
        vec.y = PlayerPrefs.GetFloat(CurrentMap + "_y");
        vec.z = PlayerPrefs.GetFloat(CurrentMap + "_z");
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
        PlayerPrefs.DeleteKey(mapName + "_z");
        PlayerPrefs.DeleteKey(CurrentMap + "_time_played");
        PlayerPrefs.Save();
    }

    public void SetCurrentMap(string mapName)
    {
        CurrentMap = mapName;
    }
}
