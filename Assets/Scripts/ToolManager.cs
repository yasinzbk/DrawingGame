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

    public Color selectedColor = Color.black; // Se�ilen rengi tutan de�i�ken

    [SerializeField] BucketTool bucket;
    void Start()
    {
        LoadDrawing();

        // Ba�lang��ta ilk arac� se�
        SelectTool(tools[0]);
    }

    public void SelectTool(Tool selectedTool)
    {
        foreach (Tool tool in tools)
        {
            // Se�ili arac� etkinle�tir
            tool.enabled = (tool == selectedTool);

            // Buton etkile�imlerini g�ncelle
            int index = tools.IndexOf(tool);
            toolButtons[index].interactable = !tool.enabled;

        }
    }

    public void ChangeSelectedColor(Color newColor)
    {
        selectedColor = newColor;

        ChangeImageColor(toolPanelImage, selectedColor);

        // Se�ilen rengi ara�lara ilet
        foreach (Tool tool in tools)
        {
            tool.SetColor(selectedColor);
        }
    }

    void ChangeImageColor(Image image, Color newColor)
    {
        // Mevcut alfa de�erini al
        float alpha = image.color.a;

        // Yeni rengi belirlerken mevcut alfa de�erini kullanarak renk olu�tur
        Color finalColor = new Color(newColor.r, newColor.g, newColor.b, alpha);

        // Image bile�eninin rengini de�i�tir
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
            // Daha �nce kaydedilmi� bir resim varsa y�kle
            byte[] bytes = File.ReadAllBytes(filePath);
            drawingTexture = new Texture2D((int)drawingArea.rectTransform.rect.width, (int)drawingArea.rectTransform.rect.height);
            drawingTexture.LoadImage(bytes);

            // �izim alan�na y�klenen resmi atayarak g�ster

            drawingTexture.filterMode = FilterMode.Point;
            drawingArea.material.mainTexture = drawingTexture;

            SetCanvasToAllTools();


        }
        else
        {
            // Daha �nce kaydedilmi� bir resim yoksa bo� sayfa resmini g�ster

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
