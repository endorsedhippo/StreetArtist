using UnityEngine;
using System.Collections;
using Text = UnityEngine.UI.Text;

public class PlayerIcon : MonoBehaviour {

    public Text ready;
    public Text notReady;


    int playNumber;
    bool isReady;

    UnityEngine.UI.Image rend;
    Material defaultMaterial;

    // Use this for initialization
    void Awake ()
    {
        isReady = false;
        rend = GetComponent<UnityEngine.UI.Image>();
        defaultMaterial = rend.material;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Pauser.paused)
        {
            ready.enabled = false;
            notReady.enabled = false;
            isReady = false;
        }
        else
        {
            if (isReady)
            {
                ready.enabled = true;
                notReady.enabled = false;
            }
            else
            {
                ready.enabled = false;
                notReady.enabled = true;
            }
        }
    }

    public void SetToDefault()
    {
        rend.material = defaultMaterial;
        isReady = true;
    }

    public void TakePicture()
    {
        Texture2D tex;
        RectTransform rt = GetComponent<RectTransform>();
        tex = new Texture2D(Mathf.CeilToInt(rt.sizeDelta.x), Mathf.CeilToInt(rt.sizeDelta.y), TextureFormat.RGB24, false);

        //Get texture from webcam
        WebCamTexture wt = new WebCamTexture();
        tex.SetPixels(wt.GetPixels(wt.width / 2, wt.height / 2, Mathf.CeilToInt(rt.sizeDelta.x), Mathf.CeilToInt(rt.sizeDelta.y)));
        tex.Apply();

        Material mat = new Material(defaultMaterial);
        mat.mainTexture = tex;
        rend.material = mat;
        isReady = true;
    }

    public void SetReady(bool value)
    {
        isReady = value;
    }

    public void SetPlayerNumber(int number)
    {
        playNumber = number;
    }
    public int GetPlayerNumber()
    {
        return playNumber;
    }

    public bool IsReady()
    {
        return isReady;
    }
}
