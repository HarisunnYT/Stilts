using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileHUDPanel : Panel
{
    public void MoveLeftLeg(int direction)
    {
        MovementController.Instance.MoveLeftLeg(direction);
    }

    public void MoveRightLeg(int direction)
    {
        MovementController.Instance.MoveRightLeg(direction);
    }
}
