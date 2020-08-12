using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollider : MonoBehaviour
{
    private Grapple grapple;

    private void Awake()
    {
        grapple = GetComponentInParent<Grapple>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Leg"))
            grapple.Drop(collision.gameObject.GetComponent<Rigidbody>());
    }
}
