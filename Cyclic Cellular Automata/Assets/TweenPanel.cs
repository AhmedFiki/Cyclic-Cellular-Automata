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
    public float duration = 0.5f;
    private void Awake()
    {
        panel = GetComponent<RectTransform>();
    }
    public void TogglePanel()
    {
        if (isPanelShown)
        {
            // Hide the panel
            panel.DOAnchorPos(hiddenPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = false);
        }
        else
        {
            // Show the panel
            panel.DOAnchorPos(shownPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isPanelShown = true);
        }
    }


}
