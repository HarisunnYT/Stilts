using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField]
    private Animator leg1;

    [SerializeField]
    private Animator leg2;

    [SerializeField]
    private float firstInstructionsDelay = 3;

    [SerializeField]
    private float secondInstructionsDelay = 1f;

    [SerializeField]
    private float hideDelay = 3;

    private bool firstInstructionsComplete = false;
    private bool secondInstructionsComplete = false;

    private bool hidden = false;

    private void Awake()
    {
        Invoke("ShowLeg1", firstInstructionsDelay);
    }

    private void Update()
    {
        if (firstInstructionsComplete && !secondInstructionsComplete && Input.GetAxisRaw("Leg1") != 0)
        {
            Invoke("ShowLeg2", secondInstructionsDelay);
            secondInstructionsComplete = true;
        }

        if (!hidden && secondInstructionsComplete && Input.GetAxisRaw("Leg2") != 0)
        {
            Invoke("Hide", hideDelay);
            hidden = true;
        }
    }

    private void ShowLeg1()
    {
        leg1.SetTrigger("Show");
        firstInstructionsComplete = true;
    }

    private void ShowLeg2()
    {
        //leg1.SetTrigger("Stop");
        leg2.SetTrigger("Show");
    }

    private void Hide()
    {
        leg1.SetTrigger("Hide");
        leg2.SetTrigger("Hide");
    }
}
