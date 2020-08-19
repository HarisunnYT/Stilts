using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Tag Data")]
public class TagData : ScriptableObject
{
    public AudioClip[] AudioClips;
    public GameObject[] Particles;

    public AudioClip GetRandomClip()
    {
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }

    public GameObject GetRandomParticle()
    {
        if (Particles.Length == 0)
            return null;

        return Particles[Random.Range(0, Particles.Length)];
    }
}
