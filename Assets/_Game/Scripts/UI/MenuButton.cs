using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private float expressionValue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PanelManager.Instance.GetPanel<MainMenuPanel>().SetExpression(expressionValue);
    }
}
