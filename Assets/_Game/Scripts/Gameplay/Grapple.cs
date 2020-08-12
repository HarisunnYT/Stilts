using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour, IResetable
{
    [SerializeField]
    private Rigidbody physics;

    [SerializeField]
    private Rigidbody handle;

    [SerializeField]
    private Joint joint;

    [SerializeField]
    private float delay = 2;

    [SerializeField]
    private float disconnectDelay = 3;

    private bool dropped = false;

    private Vector3 originalPhysicsPosition;
    private Vector3 originalHandlePosition;
    private Quaternion originalHandleRotation;

    private void Awake()
    {
        Register();

        originalPhysicsPosition = physics.transform.position;
        originalHandlePosition = handle.transform.position;
        originalHandleRotation = handle.transform.rotation;
    }

    public void Drop(Rigidbody leg)
    {
        if (!dropped)
        {
            joint.connectedBody = leg;
            StartCoroutine(DropIE());
        }
    }

    private IEnumerator DropIE()
    {
        dropped = true;

        yield return new WaitForSeconds(delay);

        physics.isKinematic = false;

        StartCoroutine(Disconnect());
    }

    private IEnumerator Disconnect()
    {
        yield return new WaitForSeconds(disconnectDelay);

        joint.connectedBody = null;
    }

    public void Register()
    {
        InterfaceHandler.Register(this);
    }

    public void OnReset()
    {
        dropped = false;

        physics.transform.position = originalPhysicsPosition;
        handle.transform.position = originalHandlePosition;
        handle.transform.rotation = originalHandleRotation;

        physics.isKinematic = true;
        handle.velocity = Vector3.zero;
        handle.angularVelocity = Vector3.zero;
    }
}
