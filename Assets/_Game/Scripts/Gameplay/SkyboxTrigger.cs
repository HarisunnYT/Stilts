using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTrigger : MonoBehaviour
{
    [SerializeField]
    private Color topColor;

    [SerializeField]
    private Color bottomColor;

    [SerializeField]
    private Material skyboxMaterial;

    [SerializeField]
    private float lerpTimeInSeconds;

    private bool transitioning = false;
    private bool triggered = false;

    private float fadeDuration;
    private float timer;

    private Color previousTopColor;
    private Color previousBottomColor;

    private void Update()
    {
        if (transitioning)
        {
            timer += Time.deltaTime;
            float normTime = timer / fadeDuration;

            Color top = Color.Lerp(previousTopColor, topColor, normTime);
            Color bottom = Color.Lerp(previousBottomColor, bottomColor, normTime);

            skyboxMaterial.SetColor("_SkyGradientTop", top);
            skyboxMaterial.SetColor("_SkyGradientBottom", bottom);

            if (normTime > 1)
                transitioning = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.tag == "Player")
        {
            TriggerSkybox();
        }
    }

    public void TriggerSkybox(float duration = -1)
    {
        transitioning = true;
        triggered = true;
        timer = 0;

        fadeDuration = duration == -1 ? lerpTimeInSeconds : duration;

        previousTopColor = skyboxMaterial.GetColor("_SkyGradientTop");
        previousBottomColor = skyboxMaterial.GetColor("_SkyGradientBottom");
    }
}
