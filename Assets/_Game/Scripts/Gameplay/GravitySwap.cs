using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwap : MonoBehaviour, IResetable
{
    [SerializeField]
    private bool reverseGravity = true;

    private float originalGravity;

    private bool triggered = false;

    private void Awake()
    {
        originalGravity = Physics.gravity.y;
        Register();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            triggered = true;
            if (reverseGravity)
            {
                other.transform.rotation = Quaternion.Euler(-180, 0, 0);
                Physics.gravity = new Vector3(0, originalGravity * -1, 0);
            }
            else
            {
                other.transform.rotation = Quaternion.Euler(0, 0, 0);
                Physics.gravity = new Vector3(0, originalGravity, 0);
            }
        }
    }

    public void OnReset()
    {
        triggered = false;
    }

    public void Register()
    {
        InterfaceHandler.Register(this);
    }
}
