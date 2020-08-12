using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayVines : MonoBehaviour
{
    [SerializeField]
    private float torque;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 axis = new Vector3(0, 0, 1);

    private void FixedUpdate()
    {
        float r = Mathf.PingPong(Time.time * speed, torque) - (torque / 2);
        transform.RotateAround(transform.position, axis, r);
    }
}
