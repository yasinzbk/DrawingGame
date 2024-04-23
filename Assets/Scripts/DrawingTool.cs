using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DrawingTool : Tool, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public int thickness = 5; // Çizgi kalýnlýðý
    private bool isDrawing = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrawing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrawing = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrawing)
        {
            Draw(eventData.position);
        }
    }

    void Draw(Vector2 position)
    {
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

}
