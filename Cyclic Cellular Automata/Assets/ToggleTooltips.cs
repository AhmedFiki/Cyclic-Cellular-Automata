using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTooltips : MonoBehaviour
{
public List<GameObject> tooltips = new List<GameObject>();

    public Toggle toggle;

    public bool showTooltips = true;


    public void ToggleActive()
    {
        if(showTooltips)
        {
            HideToolTips();
        }
        else
        {
            HideToolTips();
        }
    }

    public void ShowTooltips()
    {
        showTooltips = true;
        foreach (GameObject go in tooltips)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
    }
    public void HideToolTips()
    {
        showTooltips = false;
        foreach (GameObject go in tooltips)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }
}
