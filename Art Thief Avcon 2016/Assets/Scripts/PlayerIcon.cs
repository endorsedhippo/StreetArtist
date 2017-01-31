using UnityEngine;
using System.Collections;

public class PlayerIcon : MonoBehaviour {

    public int playerNumber;
    public Material selectionMaterial;

    bool selectingTexture;

    UnityEngine.UI.Image rend;
    Material defaultMaterial;

    // Use this for initialization
    void Start ()
    {
        
        rend = GetComponent<UnityEngine.UI.Image>();
        defaultMaterial = rend.material;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown("1"))
        {
            Debug.Log("Key Down");
            rend.material = selectionMaterial;
            selectingTexture = true;
        }

        if (selectingTexture && Input.GetKeyDown("d"))
        {
            rend.material = defaultMaterial;
            selectingTexture = false;
        }

        if (selectingTexture && Input.GetKeyDown("c"))
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
            selectingTexture = false;
        }
    }
}
