using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TweenPanel : MonoBehaviour
{
     RectTransform panel;
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;

    private bool isPanelShown = true;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
    }
    public void TogglePanel()
    {
        if (isPanelShown)
        {
            // Hide the panel
            panel.DOAnchorPos(hiddenPosition, 0.5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = false);
        }
        else
        {
            // Show the panel
            panel.DOAnchorPos(shownPosition, 0.5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = true);
        }
    }

}
