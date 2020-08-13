using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WeightDistributor : MonoBehaviour
{
    [SerializeField]
    private float torque;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float newTorque = torque * Mathf.Abs(rigidbody.rotation.z);
        if (rigidbody.rotation.z > 0)
            rigidbody.angularVelocity = new Vector3(0, 0, -newTorque);
        else if (rigidbody.rotation.z < 0)
            rigidbody.angularVelocity = new Vector3(0, 0, newTorque);
    }
}
