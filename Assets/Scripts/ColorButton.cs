using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public Color color; // Butona atanan renk
    public ToolManager toolManager; // Ara� y�neticisi

    void Start()
    {
        // Butonun t�klama olay�n� ba�la
        GetComponent<Button>().onClick.AddListener(ChangeColor);
    }

    void ChangeColor()
    {
        // Renk de�i�imini Ara� Y�neticisine ile
        toolManager.ChangeSelectedColor(color);
    }
}
