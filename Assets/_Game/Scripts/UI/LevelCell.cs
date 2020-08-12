using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCell : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelNameText;

    public void Configure(string sceneName)
    {
        levelNameText.text = sceneName;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(levelNameText.text);
    }
}
