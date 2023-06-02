using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ColorsPanel : MonoBehaviour
{

    public MyGrid grid;

    public float duration = 0.5f;
    RectTransform rectTransform;
    public Vector3 hiddenPosition;
    public Vector3 shownPosition;
    public bool isPanelShown = false;

    public ColorCell[] colorCells;
    public ColorPalette currentPalette;
    public ColorPalette defaultPalette;


    List<ColorPalette> colorPalettes = new List<ColorPalette>();
    public TMP_Dropdown paletteDropdown;

    public GameObject loadingGif;
    public TextColorAnimator textColorAnimator;
    
    private void Awake()
    {
        ColorPaletteSaveLoad.Initialize();

        rectTransform = GetComponent<RectTransform>();
        loadingGif.SetActive(false);

    }
    private void Start()
    {
        PopulateColorPalettesList();
        PopulatePaletteDropdown();
        //LoadUpColorPalette(defaultPalette);
        RandomPalette();
        paletteDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        Debug.Log("Data Path: " + Application.persistentDataPath);


    }

    public void ShowLoadingGif()
    {
        loadingGif.SetActive(true);
    }
    public void HideLoadingGif()
    {
        loadingGif.SetActive(false);
    }

    public void SetPaletteDropdownSelection(int index)
    {
        // Update the dropdown's value and label
        paletteDropdown.value = index;
        paletteDropdown.RefreshShownValue();
    }
    public void RandomPalette()
    {

        int val = Random.Range(0, paletteDropdown.options.Count);
        //Debug.Log(paletteDropdown.options.Count);
        OnDropdownValueChanged(val);
    }
    public void TogglePanel()
    {
        if (isPanelShown)
        {
            // Hide the panel
            rectTransform.DOAnchorPos(hiddenPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = false);
        }
        else
        {
            // Show the panel
            rectTransform.DOAnchorPos(shownPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = true);
        }
    }

    public void SaveColorPaletteOnDisk(ColorPalette palette, string name)
    {
        ColorPaletteSaveLoad.Instance.SaveColorPalette(palette, name);

    }
    public void LoadColorPalettesOnDisk()
    {
        ColorPalette[] palettes = ColorPaletteSaveLoad.Instance.LoadAllColorPalettes();

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
        //Debug.Log("Selected option: " + selectedOption);
        LoadUpColorPalette(selectedOption);
        SetPaletteDropdownSelection(index);

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
        currentPalette = palette;
        textColorAnimator.SetPalette(currentPalette);

        grid.ResetCells();
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
        currentPalette = new ColorPalette("CurrentPalette", colors);

        return currentPalette;
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
    public void PopulateColorPalettesList()
    {
        ColorPalette[] colorPaletteObjects = Resources.LoadAll<ColorPalette>("ColorPalettes");

        foreach (ColorPalette colorPaletteObject in colorPaletteObjects)
        {
            colorPalettes.Add(colorPaletteObject);
        }
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







