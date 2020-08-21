using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [Header("Hit")]
    [Tooltip("The amount of force on impact required to trigger sound")]
    [SerializeField]
    private float hitImpactThreshold = 5;
    
    [SerializeField]
    private AudioClip[] hitSounds;

    [Header("Falling")]
    [Tooltip("The amount of falling speed required to trigger sound")]
    [SerializeField]
    private float fallingSpeedThreshold = 10;

    [SerializeField]
    private AudioClip fallingSound;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

    private bool falling = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!falling && rigidbody.velocity.y < -fallingSpeedThreshold)
        {
            falling = true;
            PlaySound(fallingSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Landed()
    {
        if (falling)
        {
            falling = false;
            AudioClip clip = hitSounds[Random.Range(0, hitSounds.Length)];
            PlaySound(clip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > hitImpactThreshold)
        {
            Landed();
        }
    }
}
