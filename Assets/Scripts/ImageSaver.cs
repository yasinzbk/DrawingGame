using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageSaver : MonoBehaviour
{
    public ToolManager toolmanager;

    Texture2D drawingTexture; // �izdi�iniz resmin texture'�

    public void SaveImage()
    {
        drawingTexture = toolmanager.GetTexture();

        // Texture'� PNG format�nda kaydet
        byte[] bytes = drawingTexture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, "drawing.png");
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Resim kaydedildi: " + filePath);
    }
}
