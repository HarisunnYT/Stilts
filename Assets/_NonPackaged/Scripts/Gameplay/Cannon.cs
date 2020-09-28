using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float delay = 10;

    private float timer = -1;

    private Animator animator;
    private AudioSource audioSource;

    private bool playerWithinRadius = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        timer = Time.time + delay;
    }

    private void Update()
    {
        if (playerWithinRadius)
        {
            if (Time.time > timer)
                Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Fire");
        audioSource.Play();

        timer = Time.time + delay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Leg"))
            playerWithinRadius = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Leg"))
            playerWithinRadius = false;
    }
}
