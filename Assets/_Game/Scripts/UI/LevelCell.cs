using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    #if !UNITY_ANDROID && !UNITY_IOS
    [SerializeField]
    private TMP_Text mapAndAuthorNameText;

    [SerializeField]
    private RawImage previewImage;

    [SerializeField]
    private GameObject continueObj;

    public string LevelName { get; private set; }
    private ulong ownerId;

    public void Configure(AssetBundleLoader.WorkshopItem workshopItem)
    {
        mapAndAuthorNameText.text = string.Format("{0} <size=36><color=#262626>by</size></color> {1}", workshopItem.LevelName, workshopItem.Item.Owner.Name);
        LoadPreviewImage(workshopItem.Item.PreviewImageUrl);

        LevelName = workshopItem.LevelName;
        ownerId = workshopItem.Item.Owner.Id.Value;
        continueObj.SetActive(SaveManager.Instance.HasSavedData(LevelName + "_" + ownerId));
    }

    private async void LoadPreviewImage(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Texture2D texture = await GetRemoteTexture(url);
            previewImage.texture = texture;
        }
    }

    private async Task<Texture2D> GetRemoteTexture(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            //begin requenst:
            var asyncOp = www.SendWebRequest();

            //await until it's done: 
            while (asyncOp.isDone == false)
            {
                await Task.Delay(1000 / 30);//30 hertz
            }

            //read results:
            if (www.isNetworkError || www.isHttpError)
            {
                //log error:
#if DEBUG
                Debug.Log($"{ www.error }, URL:{ www.url }");
#endif

                //nothing to return on error:
                return null;
            }
            else
            {
                //return valid results:
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }

    public void Continue()
    {
        SaveManager.Instance.SetCurrentMap(LevelName + "_" + ownerId);
        SaveManager.Instance.CommunityLevelPlayed(LevelName + "_" + ownerId);
        SceneManager.LoadScene(LevelName);
    }

    public void NewGame()
    {
        SaveManager.Instance.SetCurrentMap(LevelName + "_" + ownerId);
        SaveManager.Instance.CommunityLevelPlayed(LevelName + "_" + ownerId);
        SaveManager.Instance.ClearSavedData(LevelName + "_" + ownerId);
        SceneManager.LoadScene(LevelName);
    }
#endif
}
