using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ColorGridPanel : MonoBehaviour
{
    public int currentStateCell;
    public ColorsPanel colorPanel;
    RectTransform rt;
    public bool shown = false;
    private void Awake()
    {
        shown = false;
        rt = GetComponent<RectTransform>();
    }
    public void PressedColor(Color color)
    {
        colorPanel.colorCells[currentStateCell].InputColor(color);
        HideGrid();
    }
    public void HideGrid()
    {
        if (shown)
        {

            rt.DOLocalMove(new Vector3(-rt.offsetMax.x, 0, 0f), 0.2f).SetEase(Ease.OutExpo);
            shown = false;
        }

    }

    public void SetCurrentStateCell(int cell)
    {
        currentStateCell = cell;
    }
}
