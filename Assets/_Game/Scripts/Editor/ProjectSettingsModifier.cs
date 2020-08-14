using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class ProjectSettingsModifier : MonoBehaviour
{
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        //input axis set up
		var inputManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
		var serializedObject = new SerializedObject(inputManagerAsset);
		var axisArray = serializedObject.FindProperty("m_Axes");

        //check if already set up
        for (int i = 0; i < axisArray.arraySize; i++)
        {
            var axisEntry = axisArray.GetArrayElementAtIndex(i);
            if (GetChildProperty(axisEntry, "m_Name").stringValue == "Leg1")
                return;
        }

        axisArray.arraySize = axisArray.arraySize + 3;
        serializedObject.ApplyModifiedProperties();

        SetUpAxisButton(axisArray.GetArrayElementAtIndex(axisArray.arraySize - 3), "Leg1", "d", "a", "", "");
        SetUpAxisButton(axisArray.GetArrayElementAtIndex(axisArray.arraySize - 2), "Leg2", "right", "left", "", "");
        SetUpAxisButton(axisArray.GetArrayElementAtIndex(axisArray.arraySize - 1), "Pause", "escape", "", "joystick button 7", "");

        serializedObject.ApplyModifiedProperties();

        //tag set up
        var tagManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0];
        serializedObject = new SerializedObject(tagManagerAsset);

        SerializedProperty tagsProp = serializedObject.FindProperty("tags");
        tagsProp.arraySize = tagsProp.arraySize + 5;
        serializedObject.ApplyModifiedProperties();

        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 5).stringValue = "Grass";
        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 4).stringValue = "Stone";
        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 3).stringValue = "Wood";
        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 2).stringValue = "Metal";
        tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = "Generic";

        serializedObject.ApplyModifiedProperties();

        AssetDatabase.Refresh();
    }

    private static void SetUpAxisButton(SerializedProperty proptery, string axisName, string positive, string negative, string altPositive, string altNegative)
    {
        GetChildProperty(proptery, "m_Name").stringValue = axisName;
        GetChildProperty(proptery, "negativeButton").stringValue = negative;
        GetChildProperty(proptery, "positiveButton").stringValue = positive;
        GetChildProperty(proptery, "altNegativeButton").stringValue = altNegative;
        GetChildProperty(proptery, "altPositiveButton").stringValue = altPositive;
        GetChildProperty(proptery, "gravity").floatValue = 1000;
        GetChildProperty(proptery, "dead").floatValue = 0.001f;
        GetChildProperty(proptery, "sensitivity").floatValue = 1000;
    }

    private static SerializedProperty GetChildProperty(SerializedProperty p, string n)
    {
        var c = p.Copy();
        c.Next(true);

        do
        {
            if (c.name == n)
            {
                return c;
            }
        } while (c.Next(false));

        return null;
    }
}

#endif