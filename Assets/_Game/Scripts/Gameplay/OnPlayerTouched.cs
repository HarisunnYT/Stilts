using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPlayerTouched : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTouched;

    [SerializeField]
    private bool occurOnce = false;

    private bool triggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!triggered && collision.gameObject.layer == LayerMask.NameToLayer("Leg"))
        {
            triggered = occurOnce;
            OnTouched?.Invoke();
        }
    }
}
