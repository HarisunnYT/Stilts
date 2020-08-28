using Steamworks;
using Steamworks.Ugc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField]
    private GameObject gameCanvasPrefab;

    [SerializeField]
    private AudioMixer audioMixer;

    public float MusicVolume { get; private set; }
    public float SoundsVolume { get; private set; }
    public int ResWidth { get; private set; }
    public int ResHeight { get; private set; }
    public bool ResFullscreen { get; private set; }

    protected override void Initialize()
    {
        SteamClient.Init(1394510);

        SceneManager.activeSceneChanged += ActiveSceneChanged;

#if !UNITY_EDITOR
        Invoke("TriggerStartUpAchievements", 2);
#endif

        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        SoundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        ResWidth = PlayerPrefs.GetInt("width");
        ResHeight = PlayerPrefs.GetInt("height");
    }

    private void Start()
    {
        SetMusicVolume(MusicVolume);
        SetSoundVolume(SoundsVolume);
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

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        MusicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("SoundsVolume", Mathf.Log10(volume) * 20);
        SoundsVolume = volume;
    }

    public void SetRes(int width, int height, bool fullscreen)
    {
        ResWidth = width;
        ResHeight = height;
        ResFullscreen = fullscreen;

        Screen.SetResolution(width, height, fullscreen);

        PlayerPrefs.SetInt("windowed", fullscreen ? 0 : 1);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("height", height);

        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        SteamClient.Shutdown();
#endif

        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SoundsVolume", SoundsVolume);
        PlayerPrefs.Save();
    }
}
