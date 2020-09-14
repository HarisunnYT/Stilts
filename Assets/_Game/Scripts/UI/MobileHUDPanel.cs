using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileHUDPanel : Panel
{
    private List<MovementController> controllers;

    private void Start()
    {
        controllers = Util.FindObjectsOfTypeAll<MovementController>();    
    }

    public void MoveLeftLeg(int direction)
    {
        foreach(var controller in controllers)
        {
            if (controller.gameObject.activeInHierarchy)
                controller.MoveLeftLeg(direction);
        }
    }

    public void MoveRightLeg(int direction)
    {
        foreach (var controller in controllers)
        {
            if (controller.gameObject.activeInHierarchy)
                controller.MoveRightLeg(direction);
        }
    }
}
