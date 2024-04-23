using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public Color color; // Butona atanan renk
    public ToolManager toolManager; // Araç yöneticisi

    void Start()
    {
        // Butonun týklama olayýný baðla
        GetComponent<Button>().onClick.AddListener(ChangeColor);
    }

    void ChangeColor()
    {
        // Renk deðiþimini Araç Yöneticisine ile
        toolManager.ChangeSelectedColor(color);
    }
}
