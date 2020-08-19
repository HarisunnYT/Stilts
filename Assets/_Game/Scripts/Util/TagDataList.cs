using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Tag List")]
public class TagDataList : ScriptableObject
{
    public TagData[] TagData;

    public TagData GetTagData(string tag)
    {
        foreach(var data in TagData)
        {
            if (data.name == tag)
                return data;
        }

        return null;
    }
}
