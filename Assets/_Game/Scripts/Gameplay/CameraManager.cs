using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float followDelay = 2;

    private bool following = false;

    private void Awake()
    {
        Invoke("StartFollowing", followDelay);
    }

    private void StartFollowing()
    {
        following = true;
    }

    private void LateUpdate()
    {
        if (following)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z), moveSpeed);
    }
}
