using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelProcessor : MonoBehaviour
{
    public RenderTexture RenderTex;
    public RawImage RawImage;
    Texture2D _image;    

    public static PixelProcessor Instance;
    
    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _image = new Texture2D(RawImage.texture.width, RawImage.texture.height, TextureFormat.RGBA32, false);
    }

    // Update is called once per frame
    public Color[] GetPixels()
    {
        RenderTexture.active = RenderTex;
        _image.ReadPixels(new Rect(0, 0, RenderTex.width, RenderTex.height), 0, 0);
        _image.Apply();

        Color[] pixels = _image.GetPixels();

        return pixels;
        Debug.Log(pixels[0]);
    }
}
