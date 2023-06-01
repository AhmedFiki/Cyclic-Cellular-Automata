using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipHandler : MonoBehaviour
{
 public static TooltipHandler Instance;

    public TooltipGameObject tooltip;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show(string content)
    {
        Instance.tooltip.SetText(content);
        Instance.tooltip.gameObject.SetActive(true);

    }
    public static void Hide()
    {
        Instance?.tooltip.gameObject.SetActive(false);
    }
}
