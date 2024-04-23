using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    public Image toolPanelImage;

    public Image drawingArea;
    public Texture2D drawingTexture;

    public List<Tool> tools;
    public Button[] toolButtons;

    public Color selectedColor = Color.black; // Seçilen rengi tutan deðiþken

    [SerializeField] BucketTool bucket;
    void Start()
    {
        LoadDrawing();

        // Baþlangýçta ilk aracý seç
        SelectTool(tools[0]);
    }

    public void SelectTool(Tool selectedTool)
    {
        foreach (Tool tool in tools)
        {
            // Seçili aracý etkinleþtir
            tool.enabled = (tool == selectedTool);

            // Buton etkileþimlerini güncelle
            int index = tools.IndexOf(tool);
            toolButtons[index].interactable = !tool.enabled;

        }
    }

    public void ChangeSelectedColor(Color newColor)
    {
        selectedColor = newColor;

        ChangeImageColor(toolPanelImage, selectedColor);

        // Seçilen rengi araçlara ilet
        foreach (Tool tool in tools)
        {
            tool.SetColor(selectedColor);
        }
    }

    void ChangeImageColor(Image image, Color newColor)
    {
        // Mevcut alfa deðerini al
        float alpha = image.color.a;

        // Yeni rengi belirlerken mevcut alfa deðerini kullanarak renk oluþtur
        Color finalColor = new Color(newColor.r, newColor.g, newColor.b, alpha);

        // Image bileþeninin rengini deðiþtir
        image.color = finalColor;
    }

    public Texture2D GetTexture()
    {
        return drawingTexture;
    }

    void LoadDrawing()   // loads saved drawing. if it is not exits, create empty canvas
    {
        string filePath = Path.Combine(Application.persistentDataPath, "drawing.png");
        if (File.Exists(filePath))
        {
            // Daha önce kaydedilmiþ bir resim varsa yükle
            byte[] bytes = File.ReadAllBytes(filePath);
            drawingTexture = new Texture2D((int)drawingArea.rectTransform.rect.width, (int)drawingArea.rectTransform.rect.height);
            drawingTexture.LoadImage(bytes);

            // Çizim alanýna yüklenen resmi atayarak göster

            drawingTexture.filterMode = FilterMode.Point;
            drawingArea.material.mainTexture = drawingTexture;

            SetCanvasToAllTools();


        }
        else
        {
            // Daha önce kaydedilmiþ bir resim yoksa boþ sayfa resmini göster

            CreateEmptyCanvas();
        }
    }

    // when start the game, canvas is painted by gray color for some reason. To prevent this, it paints the canvas white with a bucket.
    [ContextMenu(" Crate Empty Canvas")]
    public void CreateEmptyCanvas()
    {
        if (drawingArea != null)
        {
            drawingTexture = new Texture2D((int)drawingArea.rectTransform.rect.width, (int)drawingArea.rectTransform.rect.height);  // image ile ayni boyutta ve sekilde bir texture olusturuyor
            drawingTexture.filterMode = FilterMode.Point;
            drawingArea.material.mainTexture = drawingTexture;
        }

        drawingArea.enabled = false;
        drawingArea.enabled = true;

        SetCanvasToAllTools();

        bucket?.UseBucketTool(new Vector2(435, 330), Color.white);  // set any canvas point as new vector2, can find by using camera either

        drawingTexture.Apply();
    }


    public void SetCanvasToAllTools()
    {
        foreach (var item in tools) // set textures and canvas image for all tools
        {
            item.SetCanvas(drawingArea, drawingTexture);
        }
    }
}
