using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BucketTool : Tool, IPointerDownHandler
{
    public void UseBucketTool(Vector2 position, Color newColor)
    {
        Debug.Log("Bucket is used");

        Debug.Log("Pos orj: " + position);

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea.rectTransform, position, drawingArea.canvas.worldCamera, out localPos);
        int x = (int)(localPos.x + drawingArea.rectTransform.rect.width * 0.5f);
        int y = (int)(localPos.y + drawingArea.rectTransform.rect.height * 0.5f);


        Debug.Log("Pos local: " + localPos);

        Color targetColor = drawingTexture.GetPixel(x, y);

        if (targetColor == newColor)
            return;

        Queue<Vector2> pixels = new Queue<Vector2>();
        pixels.Enqueue(new Vector2(x, y));

        while (pixels.Count > 0)
        {
            Vector2 p = pixels.Dequeue();
            if (p.x < 0 || p.x >= drawingTexture.width || p.y < 0 || p.y >= drawingTexture.height)
                continue;

            if (drawingTexture.GetPixel((int)p.x, (int)p.y) == targetColor)
            {
                drawingTexture.SetPixel((int)p.x, (int)p.y, newColor);
                pixels.Enqueue(new Vector2(p.x + 1, p.y));
                pixels.Enqueue(new Vector2(p.x - 1, p.y));
                pixels.Enqueue(new Vector2(p.x, p.y + 1));
                pixels.Enqueue(new Vector2(p.x, p.y - 1));
            }
        }
        drawingTexture.Apply();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UseBucketTool(Input.mousePosition, color);
    }

    //public void UseBucketTool(Vector2 position, Color newColor)
    //{
    //    // T�klanan konumu texture koordinatlar�na d�n��t�r
    //    Vector2 localPos;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea.rectTransform, position, drawingArea.canvas.worldCamera, out localPos);
    //    int x = (int)(localPos.x + drawingArea.rectTransform.rect.width * 0.5f);
    //    int y = (int)(localPos.y + drawingArea.rectTransform.rect.height * 0.5f);

    //    // T�klanan pikselin rengini al
    //    Color[] targetColors = drawingTexture.GetPixels(x, y, 1, 1); // T�klanan pikselin rengini al
    //    Color targetColor = targetColors[0];

    //    // Flood Fill algoritmas� ile t�klanan alan� yeni renk ile doldur
    //    FloodFill(x, y, targetColor, newColor);
    //    drawingTexture.Apply();
    //}

    //void FloodFill(int x, int y, Color targetColor, Color newColor)
    //{
    //    // Kenar kontrol�, texture d���na ��k�ld���nda dur
    //    if (x < 0 || x >= drawingTexture.width || y < 0 || y >= drawingTexture.height)
    //        return;

    //    // T�klanan pikselin rengini al
    //    Color[] colors = drawingTexture.GetPixels(x, y, 1, 1);
    //    if (colors[0] != targetColor)
    //        return; // E�er farkl� bir renk varsa i�lem yapma

    //    // Pikselin rengini de�i�tir
    //    drawingTexture.SetPixel(x, y, newColor);
    //    drawingTexture.Apply();

    //    // Recursive olarak kom�u pikselleri kontrol et
    //    FloodFill(x + 1, y, targetColor, newColor);
    //    FloodFill(x - 1, y, targetColor, newColor);
    //    FloodFill(x, y + 1, targetColor, newColor);
    //    FloodFill(x, y - 1, targetColor, newColor);
    //}
}
