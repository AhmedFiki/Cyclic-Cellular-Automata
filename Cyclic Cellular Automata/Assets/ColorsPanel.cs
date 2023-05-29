using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsPanel : MonoBehaviour
{
    public float x;
    public float y;
    public float duration=0.5f;
    private RectTransform rectTransform;
    private Vector3 hiddenPosition;
    private Vector3 shownPosition;

    public ColorCell[] colorCells;
    public ColorPalette currentPalette;
    public ColorPalette defaultPalette;


    List<ColorPalette> colorPalettes = new List<ColorPalette>();
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        hiddenPosition = new Vector3(rectTransform.offsetMax.x, y, 0f);

        shownPosition = new Vector3(rectTransform.offsetMax.x, 0, 0f);

        rectTransform.localPosition = hiddenPosition;
    }
    private void Start()
    {
        LoadColorPalette(defaultPalette);
    }
    public void LoadColorPalette(ColorPalette palette)
    {
        palette.GenerateHexCodes();
        for(int i=0; i<colorCells.Length; i++)
        {
            colorCells[i].SetCell(palette[i], palette.GetHexCode(i));

        }
    }
    public ColorPalette SaveColorPalette(string name)
    {
        Color[] colors = new Color[colorCells.Length];

        for(int i = 0; i < colorCells.Length; i++)
        {
            colors[i] = colorCells[i].color;

        }

        ColorPalette newPalette= new ColorPalette(name,colors);

        return newPalette;
    }

    public void ShowPanel()
    {
        rectTransform.DOLocalMove(shownPosition, duration).SetEase(Ease.OutExpo);
    }
    public void HidePanel()
    {
        rectTransform.DOLocalMove(hiddenPosition, duration).SetEase(Ease.OutExpo);
    }
    public Color HexToColor(string hex)
    {
        // Convert hexadecimal color to Color object
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
           // Debug.Log("Color: " + color);
        }
        else
        {
            Debug.LogError("Invalid hexadecimal color: " + hex);
        }
        return color;
    }
    public string ColorToHex(Color color)
    {

        Color32 color32 = color;
        string hex = "#"+color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
        return hex;
    }
    public Color[] GetColorArray()
    {
        Color[] array= new Color[colorCells.Length];

        for(int i = 0; i < colorCells.Length; i++)
        {

            array[i] = colorCells[i].color;

        }

        return array;

    }

}







