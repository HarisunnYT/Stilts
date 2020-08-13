using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private Vector3 offset;

    private bool following = false;

    private void StartFollowing()
    {
        following = true;
    }

    private void LateUpdate()
    {
        if (following || FTU.Instance == null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z), moveSpeed);
    }
}
