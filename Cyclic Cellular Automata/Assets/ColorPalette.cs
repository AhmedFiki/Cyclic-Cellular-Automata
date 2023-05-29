using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Color Palette")]
[Serializable]
public class ColorPalette : ScriptableObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Color[] colors = new Color[8]; // Array to store the colors

    [SerializeField]
    private string[] hexCodes = new string[8]; // Array to store the hex codes

    private void Awake()
    {
        GenerateHexCodes();

    }
    // Constructor
    public ColorPalette(string name, Color[] colors)
    {
        this.name = name;
        this.colors = colors;
        GenerateHexCodes(); // Generate the hex codes
    }
    public ColorPalette() { }

    // Generate the hex codes for the colors
    public void GenerateHexCodes()
    {

        hexCodes = new string[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].a = 1;
            hexCodes[i] = "#"+ColorUtility.ToHtmlStringRGB(colors[i]);
        }
    }

    // Indexer to access colors, hex codes, and order numbers by index
    public Color this[int index]
    {
        get { return colors[index]; }
        set
        {
            colors[index] = value;
            hexCodes[index] = ColorUtility.ToHtmlStringRGB(value); // Update the hex code
        }
    }

    // Get the hex code of a color by index
    public string GetHexCode(int index)
    {
        return hexCodes[index];
    }
    public void SetHexCode(int index, string hexCode)
    {
        hexCodes[index] = hexCode;
    }

    public Color[] PopulateColors()
    {
        Color[] colors = new Color[hexCodes.Length];

        for (int i = 0; i < hexCodes.Length; i++)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hexCodes[i], out color))
            {
                colors[i] = color;
            }
            else
            {
                // Handle the case when the hex code parsing fails, e.g., set a default color
                colors[i] = Color.white;
            }
        }

        return colors;
    }

    public ColorPaletteSerializable ToSerializable()
    {
        ColorPaletteSerializable colorPaletteSerializable = new ColorPaletteSerializable(name,hexCodes);

        return colorPaletteSerializable;
    }

    public void SerializableToSO(ColorPaletteSerializable c)
    {
        this.name = c.GetName();
        this.hexCodes = c.GetHexCodes();
        this.colors = PopulateColors();
    }
    

    
}

[Serializable]
public class ColorPaletteSerializable
{
    [SerializeField]
    private string name;

    [SerializeField]
    private string[] hexCodes = new string[8]; // Array to store the hex codes

    public ColorPaletteSerializable(string name, string[] hexCodes)
    {
        this.name = name;
        this.hexCodes = hexCodes;

    }
    public string GetName()
    {
        return name;

    }
    public string[] GetHexCodes()
    {
        return hexCodes;
    }


}