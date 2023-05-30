using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ColorsPanel : MonoBehaviour
{
    public float x;
    public float y;
    public float duration = 0.5f;
    private RectTransform rectTransform;
    private Vector3 hiddenPosition;
    private Vector3 shownPosition;

    public ColorCell[] colorCells;
    public ColorPalette currentPalette;
    public ColorPalette defaultPalette;


    List<ColorPalette> colorPalettes = new List<ColorPalette>();
    public TMP_Dropdown paletteDropdown;

    private void Awake()
    {
        ColorPaletteSaveLoad.Initialize();

        rectTransform = GetComponent<RectTransform>();

        hiddenPosition = new Vector3(rectTransform.offsetMax.x, y, 0f);

        shownPosition = new Vector3(rectTransform.offsetMax.x, 0, 0f);

        rectTransform.localPosition = hiddenPosition;
    }
    private void Start()
    {
        //PopulateColorPalettesList();
        PopulatePaletteDropdown();
        //Debug.Log(colorPalettes[0]);
        LoadUpColorPalette(defaultPalette);
        paletteDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        Debug.Log("Data Path: " + Application.persistentDataPath);




    }
    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Q))
         {
             ColorPaletteSaveLoad.Instance.SaveColorPalette(defaultPalette, "dp");
             ColorPaletteSaveLoad.Instance.LoadColorPalette("dp");
         }*/
    }

    public void SaveColorPaletteOnDisk(ColorPalette palette,string name)
    {
        ColorPaletteSaveLoad.Instance.SaveColorPalette(palette, name);

    }
    public void LoadColorPalettesOnDisk()
    {
        ColorPalette[] palettes= ColorPaletteSaveLoad.Instance.LoadAllColorPalettes();

        //paletteDropdown.options.Add(palettes[0]);

    }

    public void PopulatePaletteDropdown()
    {
        paletteDropdown.options.Clear();
        foreach (var palette in colorPalettes)
        {
            paletteDropdown.options.Add(new TMP_Dropdown.OptionData() { text = palette.name });
        }
        if (paletteDropdown.options.Count > 0)
        {
            paletteDropdown.value = 0;

        }

    }

    public void OnDropdownValueChanged(int index)
    {
        string selectedOption = paletteDropdown.options[index].text;
        Debug.Log("Selected option: " + selectedOption);
        LoadUpColorPalette(selectedOption);
    }
    public ColorPalette CreateColorPalette(string name)
    {
        Color[] colors = new Color[colorCells.Length];

        for (int i = 0; i < colorCells.Length; i++)
        {
            colors[i] = colorCells[i].color;

        }

        ColorPalette newPalette = new ColorPalette(name, colors);

        return newPalette;
    }
    public void LoadUpColorPalette(ColorPalette palette)
    {
        palette.GenerateHexCodes();
        for (int i = 0; i < colorCells.Length; i++)
        {
            colorCells[i].SetCell(palette[i], palette.GetHexCode(i));

        }
    }
    public void LoadUpColorPalette(string paletteName)
    {
        ColorPalette paletteObject = colorPalettes.Find(x => x.name == paletteName);

        LoadUpColorPalette(paletteObject);
    }
    public ColorPalette UpdateCurrentPalette()
    {
        Color[] colors = new Color[colorCells.Length];

        for (int i = 0; i < colorCells.Length; i++)
        {
            colors[i] = colorCells[i].color;

        }
        currentPalette = new ColorPalette("CurrentPalette",colors);

        return currentPalette;
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
        string hex = "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
        return hex;
    }
    public Color[] GetColorArray()
    {
        Color[] array = new Color[colorCells.Length];

        for (int i = 0; i < colorCells.Length; i++)
        {

            array[i] = colorCells[i].color;

        }

        return array;

    }
    /*public void PopulateColorPalettesList()
    {

        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/ColorPalettes" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var ColorPaletteObject = AssetDatabase.LoadAssetAtPath<ColorPalette>(SOpath);
            colorPalettes.Add(ColorPaletteObject);
        }

    }*/

}







