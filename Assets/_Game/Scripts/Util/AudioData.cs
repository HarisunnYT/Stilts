using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Audio Data")]
public class AudioData : ScriptableObject
{
    public AudioClip[] AudioClips;

    public AudioClip GetRandomClip()
    {
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }
}
