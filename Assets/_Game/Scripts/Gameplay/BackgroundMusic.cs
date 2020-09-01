using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    public void AssignMixer(AudioMixerGroup mixer)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer;
    }
}
