using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageSaver : MonoBehaviour
{
    public ToolManager toolmanager;

    Texture2D drawingTexture; // Çizdiðiniz resmin texture'ý

    public void SaveImage()
    {
        drawingTexture = toolmanager.GetTexture();

        // Texture'ý PNG formatýnda kaydet
        byte[] bytes = drawingTexture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, "drawing.png");
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Resim kaydedildi: " + filePath);
    }
}
