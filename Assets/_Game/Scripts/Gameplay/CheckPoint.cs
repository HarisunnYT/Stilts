using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            triggered = true;
            if (SaveManager.Instance)
            {
                SaveManager.Instance.SavePosition(transform.position);
                SaveManager.Instance.SaveTime(other.GetComponent<MovementController>().TimePlayed);
            }

            Debug.Log("Checkpoint Triggered");
        }
    }
}
