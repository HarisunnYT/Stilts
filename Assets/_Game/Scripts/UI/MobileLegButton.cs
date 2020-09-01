using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MobileLegButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private UnityEvent onHeld;

    private bool held = false;

    private void Update()
    {
        if (held)
            onHeld?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        held = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        held = false;
    }
}
