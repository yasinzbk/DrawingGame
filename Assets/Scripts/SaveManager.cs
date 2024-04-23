using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Image drawingArea;
    public Texture2D defaultTexture; // Bo� sayfa resmi
    private Texture2D drawingTexture; // �izilen resmin texture'�

    void Start()
    {
        LoadDrawing();
    }

    void LoadDrawing()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "drawing.png");
        if (File.Exists(filePath))
        {
            // Daha �nce kaydedilmi� bir resim varsa y�kle
            byte[] bytes = File.ReadAllBytes(filePath);
            drawingTexture = new Texture2D(defaultTexture.width, defaultTexture.height);
            drawingTexture.LoadImage(bytes);

            // �izim alan�na y�klenen resmi atayarak g�ster
            drawingArea.sprite = Sprite.Create(drawingTexture, new Rect(0, 0, drawingTexture.width, drawingTexture.height), Vector2.one * 0.5f);
        }
        else
        {
            // Daha �nce kaydedilmi� bir resim yoksa bo� sayfa resmini g�ster
            drawingArea.sprite = Sprite.Create(defaultTexture, new Rect(0, 0, defaultTexture.width, defaultTexture.height), Vector2.one * 0.5f);
        }
    }
}
