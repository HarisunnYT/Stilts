using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderOptimiser : MonoBehaviour, IResetable
{
    [SerializeField]
    private GameObject nextStage;

    private bool triggered = false;

    private Collider[] collidersToDisable;
    private Collider[] collidersToEnable = new Collider[0];

    private void Awake()
    {
        Register();

        collidersToDisable = GetComponentsInChildren<Collider>();
        if (nextStage)
        {
            collidersToEnable = nextStage.GetComponentsInChildren<Collider>();

//#if !UNITY_EDITOR
            foreach (var collider in collidersToEnable)
                collider.enabled = false;
//#endif
        }
    }

    public void OnReset()
    {
        triggered = false;

        foreach (var collider in collidersToDisable)
            collider.enabled = true;

        foreach (var collider in collidersToEnable)
            collider.enabled = false;
    }

    public void Register()
    {
        InterfaceHandler.Register(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            foreach (var collider in collidersToDisable)
                collider.enabled = false;

            foreach (var collider in collidersToEnable)
                collider.enabled = true;

            triggered = true;
        }
    }
}
