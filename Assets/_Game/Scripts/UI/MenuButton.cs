using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private float expressionValue;

    [SerializeField]
    private Animator player;

    public void OnPointerEnter(PointerEventData eventData)
    {
        player.SetFloat("Expression", expressionValue);
    }
}
