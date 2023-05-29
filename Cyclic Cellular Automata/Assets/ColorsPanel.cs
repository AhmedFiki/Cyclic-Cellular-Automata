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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        hiddenPosition = new Vector3(rectTransform.offsetMax.x, y, 0f);

        shownPosition = new Vector3(rectTransform.offsetMax.x, 0, 0f);

        rectTransform.localPosition = hiddenPosition;
    }
    private void Start()
    {

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

}







