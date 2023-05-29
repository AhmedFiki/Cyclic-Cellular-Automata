using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ColorPalette", menuName = "Color Palette")]
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

    // Generate the hex codes for the colors
    public void GenerateHexCodes()
    {
        hexCodes = new string[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
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


}

