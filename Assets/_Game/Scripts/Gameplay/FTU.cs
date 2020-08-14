using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTU : Singleton<FTU>
{
    [SerializeField]
    private MovementController player;

    [Space()]
    [SerializeField]
    private float startingLeg1Force;

    [SerializeField]
    private float startingLeg2Force;

    [Space()]
    [SerializeField]
    private float cameraFollowDelay = 2;

    [Space()]
    [SerializeField]
    private CameraManager cameraManager;

    private void Start()
    {
        player.LegPivot1.AddTorque(new Vector3(0, 0, startingLeg1Force), ForceMode.Impulse);
        player.LegPivot2.AddTorque(new Vector3(0, 0, startingLeg2Force), ForceMode.Impulse);

        player.InputEnabled = false;

        Invoke("StartFollowing", cameraFollowDelay);
    }

    private void StartFollowing()
    {
        cameraManager.StartFollowing();
        player.InputEnabled = true;
    }
}
