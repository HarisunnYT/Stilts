using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float torque = 10;

    [SerializeField]
    private bool rotateRight = true;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.angularVelocity = new Vector3(0, 0, rotateRight ? torque : -torque);
    }
}
