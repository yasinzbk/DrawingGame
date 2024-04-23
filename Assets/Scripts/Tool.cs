using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    protected Image drawingArea;
    protected Texture2D drawingTexture;

    protected Color color = Color.black;

    public virtual void SetColor(Color color)
    {
        this.color = color;
    }

    public void SetCanvas(Image area, Texture2D texture)
    {
        drawingArea = area;
        drawingTexture = texture;

    }
}
