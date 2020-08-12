using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingAnchor : MonoBehaviour
{
    [SerializeField]
    private float dropDelay = 1;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Leg"))
            Invoke("Drop", dropDelay);
    }

    private void Drop()
    {
        Destroy(GetComponent<MeshCollider>());
        rigidbody.isKinematic = false;
    }
}
