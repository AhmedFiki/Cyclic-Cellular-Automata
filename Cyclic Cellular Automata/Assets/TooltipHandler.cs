using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class TooltipHandler : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TMP_Text tooltipText;
    public float delayDuration = 1f;
    public float panelTweenDuration = 0.2f;
    private Coroutine tooltipCoroutine;
    public bool tooltipsActive = true;
    public float shift = 230+85;

    private void Awake()
    {
        tooltipPanel.SetActive(false);
        tooltipPanel.transform.DOLocalMove(new Vector3(shift, transform.localPosition.y, 0), panelTweenDuration).SetEase(Ease.OutExpo).OnComplete(() => DeactivateTooltipPanel());


    }
    private void Start()
    {
        

    }



    public void OnPointerEnter()
    {
        //Debug.Log("Entered");
        if(tooltipsActive ) { 
        tooltipCoroutine = StartCoroutine(ShowTooltipAfterDelay());
        }
    }

    public void OnPointerExit()
    {
        //Debug.Log("Exited");
        if (tooltipsActive)
        {

            if (tooltipCoroutine != null)
                StopCoroutine(tooltipCoroutine);

            HideTooltip();
        }
    }

    private IEnumerator ShowTooltipAfterDelay()
    {
        yield return new WaitForSeconds(delayDuration);

        ShowTooltip();
    }

    private void ShowTooltip()
    {

        // Activate the tooltip panel
        tooltipPanel.SetActive(true);

        //tooltipPanel.transform.DOMove(transform.position - new Vector3(shift, 0,0), panelTweenDuration).SetEase(Ease.OutExpo);
        tooltipPanel.transform.DOLocalMove(-new Vector3(shift, -transform.localPosition.y, 0), panelTweenDuration).SetEase(Ease.OutExpo);

    }

    private void HideTooltip()
    {

        //tooltipPanel.transform.DOMove(transform.position + new Vector3(shift, 0, 0), panelTweenDuration).SetEase(Ease.OutExpo).OnComplete(() => DeactivateTooltipPanel());
        tooltipPanel.transform.DOLocalMove(new Vector3(shift, transform.localPosition.y, 0), panelTweenDuration).SetEase(Ease.OutExpo).OnComplete(() => DeactivateTooltipPanel());

    }

    private void DeactivateTooltipPanel()
    {
        // Deactivate the tooltip panel when the animation is complete
        //tooltipPanel.SetActive(false);
    }

    private void ResizePanel()
    {
        float preferredWidth = tooltipText.preferredWidth/5;
        float preferredHeight = tooltipText.preferredHeight;

        Vector2 newSize = new Vector2(preferredWidth, preferredHeight);

        tooltipPanel.GetComponent<RectTransform>().sizeDelta = newSize;

    }
}
