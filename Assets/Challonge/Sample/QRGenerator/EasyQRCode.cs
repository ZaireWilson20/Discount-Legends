using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyQRCode : MonoBehaviour {

    public string textToEncode = "Hello World!";
    public Color darkColor = Color.black;
    public Color lightColor = Color.white;

    void Start()
    {
        Invoke("encode", 1);
        //// Example usage of QR Generator
        //// The text can be any string, link or other QR Code supported string

        //Texture2D qrTexture = QRGenerator.EncodeString(textToEncode, darkColor, lightColor);

        //// Set the generated texture as the mainTexture on the quad
        //GetComponent<Renderer>().material.mainTexture = qrTexture;
    }

    private void encode()
    {
        Debug.Log("Encode Start...");
       // GetComponent<RawImage>().texture = QRGenerator.EncodeString(textToEncode);
        Debug.Log("Encode Finished");
    }
}
