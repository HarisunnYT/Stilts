﻿using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Material triggeredMaterial;

    [SerializeField]
    private MeshRenderer flagRenderer;

    [SerializeField]
    private GameObject triggeredParticle;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "Player")
        {
            triggered = true;
            if (SaveManager.Instance)
                SaveManager.Instance.SavePosition(transform.position);

            flagRenderer.material = triggeredMaterial;

            if (MovementController.Instance.TimePlayed > 5)
            {
                triggeredParticle.SetActive(true);
                GetComponent<AudioSource>().Play();
            }

            Debug.Log("Checkpoint Triggered");
        }
    }
}
