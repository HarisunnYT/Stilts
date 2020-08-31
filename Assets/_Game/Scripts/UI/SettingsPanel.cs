using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : Panel
{
    [System.Serializable]
    public struct Resolution
    {
        public int Width;
        public int Height;
    }

    [SerializeField]
    private List<Resolution> resolutions;

    [SerializeField]
    private TMP_Text resolutionText;

    [SerializeField]
    private Toggle windowedToggle;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider soundsSlider;

    private int resIndex = 0;

    public override void Initialise()
    {
        musicSlider.value = GameManager.Instance.MusicVolume;
        soundsSlider.value = GameManager.Instance.SoundsVolume;
        SetRes();
    }

    protected override void OnShow()
    {
        Cursor.visible = true;

        Vector2 currentRes = new Vector2(Screen.width, Screen.height);
        bool hasRes = false;

        foreach(var res in resolutions)
        {
            if (res.Width == currentRes.x && res.Height == currentRes.y)
                hasRes = true;
        }

        if (!hasRes)
            resolutions.Add(new Resolution() { Width = (int)currentRes.x, Height = (int)currentRes.y });
    }

    protected override void OnClose()
    {
        Cursor.visible = false;
    }

    public void ChangeRes(int dir)
    {
        resIndex += dir;
        if (resIndex < 0)
            resIndex = resolutions.Count - 1;
        else if (resIndex >= resolutions.Count)
            resIndex = 0;

        SetRes(resIndex);
    }

    private void SetRes(int index)
    {
        GameManager.Instance.SetRes(resolutions[index].Width, resolutions[index].Height, GameManager.Instance.ResFullscreen);
        resolutionText.text = resolutions[index].Width + "x" + resolutions[index].Height;
    }

    private void SetRes()
    {
        resolutionText.text = Screen.width + "x" + Screen.height;
        windowedToggle.isOn = !Screen.fullScreen;
    }

    public void SetWindowed(bool value)
    {
        if (gameObject.activeInHierarchy)
            GameManager.Instance.SetRes(GameManager.Instance.ResWidth, GameManager.Instance.ResHeight, !value);
    }

    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.SetMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        GameManager.Instance.SetSoundVolume(volume);
    }
}
