using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Audio List")]
public class AudioDataList : ScriptableObject
{
    public AudioData[] AudioData;

    public AudioData GetAudioData(string tag)
    {
        foreach(var data in AudioData)
        {
            if (data.name == tag)
                return data;
        }

        return null;
    }
}
