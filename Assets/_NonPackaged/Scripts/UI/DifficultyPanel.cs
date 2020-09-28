using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyPanel : Panel
{
    [SerializeField]
    private TMP_Text message;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private string messageText;

    [SerializeField]
    private float delayBetweenLetters = 0.05f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnShow()
    {
        Cursor.visible = true;
    }

    protected override void OnClose()
    {
        Cursor.visible = false;
    }

    public void EasyPressed()
    {
        buttons.SetActive(false);
        message.gameObject.SetActive(true);

        StartCoroutine(TypeMessage());
    }

    private IEnumerator TypeMessage()
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            message.text += messageText[i];
            audioSource.Play();

            yield return new WaitForSecondsRealtime(delayBetweenLetters);
        }

        yield return new WaitForSecondsRealtime(2);

        if (gameObject.activeSelf)
            PanelManager.Instance.ShowPanel<PausePanel>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
            PanelManager.Instance.ShowPanel<PausePanel>();
    }
}
