using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float scale = 1.1f;

    [SerializeField]
    private float duration = 0.1f;

    private float target = 0;
    private float previousScale;
    private float timer;
    private float originalScale;

    private void Awake()
    {
        originalScale = transform.localScale.x;
    }

    private void Update()
    {
        if (target != 0)
        {
            timer += Time.unscaledDeltaTime;
            float normTime = timer / duration;

            transform.localScale = Vector3.Lerp(Vector3.one * previousScale, Vector3.one * target, normTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        timer = 0;
        target = scale;
        previousScale = originalScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        timer = 0;
        target = originalScale;
        previousScale = scale;
    }
}
