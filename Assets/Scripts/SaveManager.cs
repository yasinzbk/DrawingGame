using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Image drawingArea;
    public Texture2D defaultTexture; // Boþ sayfa resmi
    private Texture2D drawingTexture; // Çizilen resmin texture'ý

    void Start()
    {
        LoadDrawing();
    }

    void LoadDrawing()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "drawing.png");
        if (File.Exists(filePath))
        {
            // Daha önce kaydedilmiþ bir resim varsa yükle
            byte[] bytes = File.ReadAllBytes(filePath);
            drawingTexture = new Texture2D(defaultTexture.width, defaultTexture.height);
            drawingTexture.LoadImage(bytes);

            // Çizim alanýna yüklenen resmi atayarak göster
            drawingArea.sprite = Sprite.Create(drawingTexture, new Rect(0, 0, drawingTexture.width, drawingTexture.height), Vector2.one * 0.5f);
        }
        else
        {
            // Daha önce kaydedilmiþ bir resim yoksa boþ sayfa resmini göster
            drawingArea.sprite = Sprite.Create(defaultTexture, new Rect(0, 0, defaultTexture.width, defaultTexture.height), Vector2.one * 0.5f);
        }
    }
}
