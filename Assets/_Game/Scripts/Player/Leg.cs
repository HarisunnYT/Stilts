using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField]
    private float audioDelay = 1;

    [SerializeField]
    private float soundDamper = 15;

    [SerializeField]
    private float pitchOffset = 0.2f;

    [SerializeField]
    private AudioDataList audioList;

    private AudioSource audioSource;

    private float audioTimer = 0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > audioTimer)
        {
            AudioData audioData = audioList.GetAudioData(collision.transform.tag);
            if (audioData != null)
                PlaySound(audioData.GetRandomClip(), collision);
        }
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        audioSource.volume = volume;
        audioSource.pitch = 1 + Random.Range(-pitchOffset, pitchOffset);

        audioSource.clip = sound;
        audioSource.Play();

        audioTimer = Time.time + audioDelay;
    }

    public void PlaySound(AudioClip sound, Collision collision)
    {
        PlaySound(sound, collision.relativeVelocity.magnitude / soundDamper);
    }
}
