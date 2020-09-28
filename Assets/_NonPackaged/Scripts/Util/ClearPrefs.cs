using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClearPrefs
{
#if UNITY_EDITOR
    [MenuItem("Tools/Clear Prefs")]
    static void ClearPrefData()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
