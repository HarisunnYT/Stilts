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
