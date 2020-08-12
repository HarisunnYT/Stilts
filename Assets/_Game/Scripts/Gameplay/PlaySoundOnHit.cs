using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Leg"))
            collision.gameObject.GetComponent<Leg>().PlaySound(audioClips[Random.Range(0, audioClips.Length)], collision);
    }
}
