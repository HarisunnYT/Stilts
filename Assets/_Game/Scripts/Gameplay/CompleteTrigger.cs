using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CompleteTrigger : MonoBehaviour
{
    public void Complete()
    {
        PanelManager.Instance.ShowPanel<VictoryPanel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Complete();
    }
}
