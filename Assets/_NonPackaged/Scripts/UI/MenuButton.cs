using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float expressionValue;

    [Space()]
    [SerializeField]
    private UnityEvent onExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PanelManager.Instance.GetPanel<MainMenuPanel>().SetExpression(expressionValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke();
    }
}
