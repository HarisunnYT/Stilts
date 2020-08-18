using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Ugc;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;
using System.Linq;
using Steamworks.Data;
#if UNITY_EDITOR
using UnityEditor;

public class WorkshopUploader : MonoBehaviour
{
    [MenuItem("Tools/Workshop Uploader")]
    private static void ShowWorkshopUploader()
    {
        EditorWindow.GetWindow(typeof(WorkshopUploaderWindow));
    }

    public static async void Upload(ulong ID, string title, string description, string iconPath)
    {
        Steamworks.Ugc.Editor commFile;
        if (ID == 0)
            commFile = Steamworks.Ugc.Editor.NewCommunityFile;
        else
        {
            PublishedFileId publishedId = new PublishedFileId() { Value = ID };
            commFile = new Steamworks.Ugc.Editor(publishedId);
        }

        string path = Application.streamingAssetsPath  + "/WorkshopItem";

        DirectoryInfo directory = new DirectoryInfo(path);

        commFile.WithContent(directory);
        commFile.WithTitle(title);
        commFile.WithDescription(description);
        commFile.WithTag("Level");
        commFile.WithPreviewFile(iconPath);
        commFile.WithPublicVisibility();

        SteamClient.Init(1394510);
        var result = await commFile.SubmitAsync(new ProgressClass());

        Application.OpenURL("https://steamcommunity.com/sharedfiles/filedetails/?id=" + result.FileId + "&searchtext=");
        Debug.Log("Upload Complete");

        SteamClient.Shutdown();
    }

    class ProgressClass : IProgress<float>
    {
        float lastvalue = 0;
        public void Report(float value)
        {
            if (lastvalue >= value) return;

            lastvalue = value;

            Debug.Log(lastvalue);
        }
    }
}

public class WorkshopUploaderWindow : EditorWindow
{
    private bool connectedToSteam = false;

    string title = "Awesome level";
    string description = "New Workshop Item";
    string workshopId = "Workshop ID eg: 2199949864";

    private string iconPath = "";

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Title");
        title = EditorGUILayout.TextField(title);

        EditorGUILayout.LabelField("Description");
        description = EditorGUILayout.TextArea(description, GUILayout.Height(position.height - 400));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Icon");
        EditorGUILayout.LabelField(iconPath);
        if (GUILayout.Button("Upload"))
            ChooseIcon();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        workshopId = EditorGUILayout.TextField(workshopId);
        EditorGUILayout.LabelField("Use an existing ID to edit workshop item (leave empty for new item)");
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Submit"))
        {
            ulong id = 0;
            if (!string.IsNullOrEmpty(workshopId))
            {
                if (workshopId.Any(x => char.IsLetter(x)) || !ulong.TryParse(workshopId, out id))
                {
                    EditorUtility.DisplayDialog("ID contains letters", "Enter a valid workshop ID or leave empty", "Yes");
                    return;
                }
            }

            WorkshopUploader.Upload(id, title, description, iconPath);
        }
    }

    private void ChooseIcon()
    {
        string srcFilePath = EditorUtility.OpenFilePanel("Select Icon", Application.streamingAssetsPath, "png");

        if (string.IsNullOrEmpty(srcFilePath))
            return;

        FileInfo file = new FileInfo(srcFilePath);

        if (!file.Exists)
        {
            EditorUtility.DisplayDialog("Icon could not be loaded", "Was not found", "Yes");
            return;
        }

        if (file.Length > 1000000)
        {
            EditorUtility.DisplayDialog("Icon could not be loaded", "File is over 1MB", "Yes");
            return;
        }

        iconPath = srcFilePath;
    }
}

#endif