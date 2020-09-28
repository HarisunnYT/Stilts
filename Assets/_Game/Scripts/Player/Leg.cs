using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Leg : MonoBehaviour
{
    [SerializeField]
    private float delay = 1;

    [SerializeField]
    private float soundDamper = 15;

    [SerializeField]
    private float pitchOffset = 0.2f;

    [SerializeField]
    private TagDataList dataList;

    private AudioSource audioSource;
    private PlayerSoundController playerSoundController;

    private float timer = 0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Vector3 lastGroundedPosition = Vector3.zero;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerSoundController = GetComponentInParent<PlayerSoundController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > timer)
        {
            TagData data = dataList.GetTagData(collision.transform.tag);
            if (data != null)
            {
                PlayParticle(data.GetRandomParticle(), collision);
                PlaySound(data.GetRandomClip(), collision);
            }
        }

        if (lastGroundedPosition != Vector3.zero)
        {
            float distance = Vector3.Distance(lastGroundedPosition, transform.position);
            if (distance > 150)
                AchievementManager.CompleteAchievement("big_fall");
        }

        playerSoundController.Landed();
    }

    private void OnCollisionExit(Collision collision)
    {
        lastGroundedPosition = transform.position;
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        audioSource.volume = volume;
        audioSource.pitch = 1 + Random.Range(-pitchOffset, pitchOffset);

        audioSource.clip = sound;
        audioSource.Play();

        timer = Time.time + delay;
    }

    public void PlaySound(AudioClip sound, Collision collision)
    {
        PlaySound(sound, collision.relativeVelocity.magnitude / soundDamper);
    }

    private void PlayParticle(GameObject particlePrefab, Collision collision)
    {
        if (particlePrefab == null)
            return;

        GameObject particle = ObjectPooler.GetPooledObject(particlePrefab);
        Vector3 point = collision.GetContact(0).point;
        particle.transform.position = new Vector3(point.x, point.y, -50);
        ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
        main.maxParticles = (int)(collision.relativeVelocity.magnitude / 2);
    }

    public void AssignMixer(AudioMixerGroup mixer)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer;
    }
}
