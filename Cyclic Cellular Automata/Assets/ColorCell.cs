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
