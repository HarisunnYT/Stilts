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

    [SerializeField]
    private Animator transitionAnimator;

    public float MusicVolume { get; private set; }
    public float SoundsVolume { get; private set; }
    public int ResWidth { get; private set; }
    public int ResHeight { get; private set; }
    public bool ResFullscreen { get; private set; }

    private Coroutine loadingSceneCoroutine;

    protected override void Initialize()
    {
#if !UNITY_ANDROID && !UNITY_IOS
        try
        {
            SteamClient.Init(1394510);
        }
        catch (System.Exception e) { }
#endif

        SceneManager.activeSceneChanged += ActiveSceneChanged;

#if !UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IOS
        Invoke("TriggerStartUpAchievements", 2);
#endif

        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        SoundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        ResWidth = PlayerPrefs.GetInt("width", Screen.currentResolution.width);
        ResHeight = PlayerPrefs.GetInt("height", Screen.currentResolution.height);

        if (ResWidth == 0)
            ResWidth = Screen.currentResolution.width;
        if (ResHeight == 0)
            ResHeight = Screen.currentResolution.height;
    }

    private void Start()
    {
        SetMusicVolume(MusicVolume);
        SetSoundVolume(SoundsVolume);
    }

    #if !UNITY_ANDROID && !UNITY_IOS
    private async void TriggerStartUpAchievements()
    {
        try
        {
            if (SteamClient.Name == "Emurinoo")
                AchievementManager.CompleteAchievement("one_of_us");

            var query = Query.All.WhereUserPublished(SteamClient.SteamId);
            var task = await query.GetPageAsync(1);
            foreach (var entry in task.Value.Entries)
            {
                if (entry.VotesUp > 50)
                    AchievementManager.CompleteAchievement("receive_50_upvotes");
            }
        }
        catch (System.Exception e) { }
    }
#endif

    private void ActiveSceneChanged(Scene from, Scene to)
    {
        Time.timeScale = 1;
        if (to.name != "MainMenu")
        {
            Instantiate(gameCanvasPrefab);
#if UNITY_ANDROID || UNITY_IOS
            PanelManager.Instance.ShowPanel<MobileHUDPanel>();
#endif

            FindObjectOfType<BackgroundMusic>().AssignMixer(audioMixer.FindMatchingGroups("Music")[0]);

            foreach (var character in FindObjectsOfType<MovementController>())
                character.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sounds")[0];

            foreach (var leg in FindObjectsOfType<Leg>())
                leg.AssignMixer(audioMixer.FindMatchingGroups("Sounds")[0]);
        }
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
    }

    public void LoadScene(string scene)
    {
        if (loadingSceneCoroutine == null)
            loadingSceneCoroutine = StartCoroutine(LoadSceneIE(scene));
    }

    private IEnumerator LoadSceneIE(string scene)
    {
        transitionAnimator.SetTrigger("Show");

        yield return new WaitForSecondsRealtime(1.2f);

        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(scene);

        yield return loadingScene;

        transitionAnimator.SetTrigger("Hide");
        loadingSceneCoroutine = null;
    }

    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IOS
        SteamClient.Shutdown();
#endif

        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SoundsVolume", SoundsVolume);
        PlayerPrefs.Save();
    }
}
