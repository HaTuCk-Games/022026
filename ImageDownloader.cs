using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageDownloader : MonoBehaviour
{
    [SerializeField] private string _url;

    private Image _img;

    private void Start()
    {
        _img = GetComponent<Image>();

        StartCoroutine(LoadImage());
    }
    private IEnumerator LoadImage()
    {
        Debug.Log("Download Image");
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_url);
        yield return webRequest.SendWebRequest();

        if(webRequest.isDone == false)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

            _img.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
