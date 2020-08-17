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
    private AudioClip[] fallingSounds;

    [Space()]
    [SerializeField]
    private float pitchOffset = 0.1f;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude > fallingSpeedThreshold)
            PlayRandomAudioClip(fallingSounds);
    }

    private void PlayRandomAudioClip(AudioClip[] clips)
    {
        if (audioSource.isPlaying)
            return;

        AudioClip clip =  clips[Random.Range(0, clips.Length)];
        audioSource.clip = clip;
        audioSource.pitch = 1 + Random.Range(-pitchOffset, pitchOffset);
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > hitImpactThreshold)
            PlayRandomAudioClip(hitSounds);
    }
}
