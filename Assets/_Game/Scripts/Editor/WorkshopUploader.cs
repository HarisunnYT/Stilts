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

    public static async void Upload(ulong ID, string title, string description, string iconPath, params string[] tags)
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
        commFile.WithPreviewFile(iconPath);
        commFile.WithPublicVisibility();

        foreach(var tag in tags)
        {
            if (!string.IsNullOrEmpty(tag))
                commFile.WithTag(tag);
        }

        var result = await commFile.SubmitAsync(new ProgressClass());

        Application.OpenURL("https://steamcommunity.com/sharedfiles/filedetails/?id=" + result.FileId + "&searchtext=");
        Debug.Log("Upload Complete");
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

    private bool easyState = false;
    private bool hardState = false;
    private bool hardcoreState = false;
    private bool arcadeState = false;
    private bool sportState = false;
    private bool memeState = false;
    private bool puzzleState = false;
    private bool simulationState = false;

    private void OnGUI()
    {
        if (GUILayout.Button("Build Bundles"))
        {
            if (!Directory.Exists(Application.streamingAssetsPath + "/WorkshopItem"))
                Directory.CreateDirectory(Application.streamingAssetsPath + "/WorkshopItem");

            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/WorkshopItem", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

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

        EditorGUILayout.LabelField("Tags");

        string easyTag = "";
        string hardTag = "";
        string hardcoreTag = "";
        string arcadeTag = "";
        string sportTag = "";
        string memeTag = "";
        string puzzleTag = "";
        string sumulationTag = "";

        EditorGUILayout.BeginHorizontal();
        easyTag = (easyState = GUILayout.Toggle(easyState, "Easy")) ? "Easy" : "";
        hardTag = (hardState = GUILayout.Toggle(hardState, "Hard")) ? "Hard" : "";
        hardcoreTag = (hardcoreState = GUILayout.Toggle(hardcoreState, "Hardcore")) ? "Hardcore" : "";
        arcadeTag = (arcadeState = GUILayout.Toggle(arcadeState, "Arcade")) ? "Arcade" : "";
        sportTag = (sportState = GUILayout.Toggle(sportState, "Sport")) ? "Sport" : "";
        memeTag = (memeState = GUILayout.Toggle(memeState, "Meme")) ? "Meme" : "";
        puzzleTag = (puzzleState = GUILayout.Toggle(puzzleState, "Puzzle")) ? "Puzzle" : "";
        sumulationTag = (simulationState = GUILayout.Toggle(simulationState, "Simulation")) ? "Simluation" : "";
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(100);

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

            WorkshopUploader.Upload(id, title, description, iconPath, easyTag, hardTag, hardcoreTag, arcadeTag);
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Init Steam"))
            SteamClient.Init(1394510);
        if (GUILayout.Button("Shutdown Steam"))
            SteamClient.Shutdown();
        EditorGUILayout.EndHorizontal();
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