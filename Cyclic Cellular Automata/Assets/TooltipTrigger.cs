using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [TextArea(3, 10)]
    Tween delayTween;   
    Tween fadeTween;   
    public string content;
    public float fadeInDuration = 0.2f;
    public float fadeOutDuration = 0.2f;
     float delay=0.3f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        delayTween = DOTween.To(() => 0f, x => { }, 0f, delay).OnComplete(() => ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (delayTween != null && delayTween.IsActive())
            delayTween.Kill();

        HideTooltip();
    }

    private void ShowTooltip()
    {
        TooltipHandler.Show(content);
        fadeTween = TooltipHandler.Instance.canvasGroup.DOFade(1f, fadeInDuration);
    }

    private void HideTooltip()
    {
        fadeTween = TooltipHandler.Instance.canvasGroup.DOFade(0f, fadeOutDuration)
            .OnComplete(() => TooltipHandler.Hide());
    }
}
