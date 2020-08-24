using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnObjectTouched : MonoBehaviour
{
    [Tooltip("Will trigger callback when an object in the list hits this object")]
    [SerializeField]
    private GameObject[] targets;

    [Space()]
    [SerializeField]
    private UnityEvent hitCallback;

    private void OnTriggerEnter(Collider other)
    {
        foreach(var obj in targets)
        {
            if (obj == other.gameObject)
                hitCallback?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var obj in targets)
        {
            if (obj == collision.gameObject)
                hitCallback?.Invoke();
        }
    }
}
