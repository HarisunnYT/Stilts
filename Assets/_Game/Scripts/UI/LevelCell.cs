using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelNameText;

    [SerializeField]
    private TMP_Text ownerNameText;

    [SerializeField]
    private RawImage previewImage;

    public string LevelName { get; private set; }
    private string ownerName;

    public void Configure(AssetBundleLoader.WorkshopItem workshopItem)
    {
        levelNameText.text = workshopItem.LevelName;
        ownerNameText.text = workshopItem.Item.Owner.Name;
        LoadPreviewImage(workshopItem.Item.PreviewImageUrl);

        LevelName = workshopItem.LevelName;
        ownerName = workshopItem.Item.Owner.Name;
    }

    private async void LoadPreviewImage(string url)
    {
        Texture2D texture = await GetRemoteTexture(url);
        previewImage.texture = texture;
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

    public void PlayLevel()
    {
        SaveManager.Instance.SetCurrentMap(LevelName + "_" + ownerName);
        SceneManager.LoadScene(LevelName);
    }
}
