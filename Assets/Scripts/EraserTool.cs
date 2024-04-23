using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraserTool : Tool, IPointerDownHandler, IDragHandler
{
    public int thickness = 5; // Çizgi kalýnlýðý

    private void Start()
    {
        color = Color.white; // always white
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Erase(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Erase(eventData.position);
    }

    void Erase(Vector2 position)
    {
        //Vector2 localPos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea.rectTransform, position, drawingArea.canvas.worldCamera, out localPos);
        //int x = (int)(localPos.x + drawingArea.rectTransform.rect.width * 0.5f);
        //int y = (int)(localPos.y + drawingArea.rectTransform.rect.height * 0.5f);

        //// Pikseli sildiðimizde, pikselin rengini transparan yaparak temizliyoruz
        //drawingTexture.SetPixel(x, y, Color.white);
        //drawingTexture.Apply();

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingArea.rectTransform, position, drawingArea.canvas.worldCamera, out localPos);  // ekrana dokundugumuz konumu texture konumuna uygun tutuyor
        int x = (int)(localPos.x + drawingArea.rectTransform.rect.width * 0.5f);
        int y = (int)(localPos.y + drawingArea.rectTransform.rect.height * 0.5f);
        for (int i = x - thickness / 2; i <= x + thickness / 2; i++)
        {
            for (int j = y - thickness / 2; j <= y + thickness / 2; j++)
            {
                drawingTexture.SetPixel(i, j, color);  // localpos belirtilen noktaya thickness kalinlik ayari kadar pixel olusturuyor
            }
        }

        drawingTexture.Apply(); // texture changes uploding to GPU
    }

    public override void SetColor(Color color)
    {
        Debug.Log(" cnt be changed to color of eraser");
    }
}
