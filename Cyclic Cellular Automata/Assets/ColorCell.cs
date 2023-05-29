using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ColorCell : MonoBehaviour
{

    public int state;
    public Color color;
    public string hexCode;

    //public TMP_Text stateText;
    public Image colorImage;
    public TMP_InputField hexInputField;

    public RectTransform colorGrid;
     ColorGridPanel panel;

    private void Awake()
    {
        panel = colorGrid.GetComponent<ColorGridPanel>();
    }
    public void UpdateCell()
    {
        colorImage.color = color;
        hexInputField.text = hexCode;
    }
    private void Start()
    {
        UpdateCell();

    }
    public void SetCell(Color c,string hex)
    {
        color = c;
        hexInputField.text = hex;
        hexCode = hex;
        colorImage.color = color;
    }
    public void InputColor(Color c)
    {
        color = c;
        string hex = ColorUtility.ToHtmlStringRGB(c);
        hexInputField.text = hex;
        hexCode = hex;
        colorImage.color = color;
    }
    public void ShowColorGrid()
    {
        if (panel.shown)
        {
            return;
        }
        colorGrid.DOLocalMove(new Vector3(-colorGrid.offsetMax.x,0, 0f), 0.2f).SetEase(Ease.OutExpo);
        panel.shown = true;

    }public void HideColorGrid()
    {
        if (!panel.shown)
        {
            return;
        }
        colorGrid.DOLocalMove(new Vector3(-colorGrid.offsetMax.x, 0, 0f), 0.2f).SetEase(Ease.OutExpo);
        panel.shown = false;

    }
    public void InputHex()
    {
        string hex = hexInputField.text;

        //.TrimStart('#')
        // Add the "#" symbol at the start if it's missing

        if (!hex.StartsWith("#"))
        {
            hex = "#" + hex;
        }
        // Convert hexadecimal color to Color object

        Color c;
        if (ColorUtility.TryParseHtmlString(hex, out c))
        {
            Debug.Log("Cell " + state + " Changed from " + hexCode + " to " + hex);
            color = c;
            hexCode = hex;
        }
        else
        {
            Debug.Log("Invalid hexadecimal color: " + hex);
            hexInputField.text = hexCode;

        }
        UpdateCell();


    }
}
