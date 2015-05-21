using UnityEngine;
using System.Collections;

public class MapConfig : MonoBehaviour {
    private string _mapUrl;

    public string MapUrl{
        get { return _mapUrl; }
        set 
        {
            _mapUrl = value;
            DownloadImage(MapUrl);            
        }
    }

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            var seedRandom = new System.Random();
            var longitude = (float)((seedRandom.NextDouble() * 20) + 45);
            var latitude = (float)((seedRandom.NextDouble() * 90) + 35);
            SetMapCoordinates(latitude, longitude);
        }
    }

    public void DownloadImage(string url) {
        StartCoroutine(coDownloadImage(url));
    }

    IEnumerator coDownloadImage(string imageUrl) {
        WWW www = new WWW(imageUrl);
        yield return www;
        var mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
        if (mainTexture != null)
        {
            www.LoadImageIntoTexture((Texture2D)mainTexture);
        }
        www.Dispose();
    }

    [RPC]
    private void SetMapCoordinates(float latitude, float longitude)
    {
        MapUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + longitude + "," + latitude + "&zoom=10&size=360x640&maptype=satellite";
        Debug.Log("Getting map from: " + MapUrl);
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("SetMapCoordinates", PhotonTargets.OthersBuffered, latitude, longitude);            
        }
    }

}
