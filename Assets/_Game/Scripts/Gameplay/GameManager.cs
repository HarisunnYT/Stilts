using Steamworks;
using Steamworks.Ugc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField]
    private GameObject gameCanvasPrefab;

    protected override void Initialize()
    {
        SceneManager.activeSceneChanged += ActiveSceneChanged;

#if !UNITY_EDITOR
        Invoke("TriggerStartUpAchievements", 2);
#endif
    }

    private async void TriggerStartUpAchievements()
    {
        if (SteamClient.Name == "Emurinoo")
            AchievementManager.CompleteAchievement("one_of_us");

        var query = Query.All.WhereUserPublished(SteamClient.SteamId);
        var task = await query.GetPageAsync(1);
        foreach(var entry in task.Value.Entries)
        {
            if (entry.VotesUp > 50)
                AchievementManager.CompleteAchievement("receive_50_upvotes");
        }
    }

    private void ActiveSceneChanged(Scene from, Scene to)
    {
        Time.timeScale = 1;
        if (to.name != "MainMenu")
            Instantiate(gameCanvasPrefab);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !PanelManager.Instance.GetPanel<DifficultyPanel>().gameObject.activeSelf && !PanelManager.Instance.GetPanel<PausePanel>().gameObject.activeSelf)
            PanelManager.Instance.ShowPanel<PausePanel>();
    }
}
