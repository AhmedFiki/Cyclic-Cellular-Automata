using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{

    public Color color;
    public ColorGridPanel panel;

    private void Awake()
    {
        color.a = 1;
        Image[] childImages = GetComponentsInChildren<Image>(true); // Include inactive objects if needed

        foreach (Image childImage in childImages)
        {
            if (childImage.gameObject != gameObject) // Skip the current object
            {
                childImage.color = color;
            }
        }
    }

    public void OnClick()
    {
        panel.PressedColor(color);
    }


}
