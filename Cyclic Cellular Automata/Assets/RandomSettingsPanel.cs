using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSettingsPanel : MonoBehaviour
{
    public MyGrid grid;
    RectTransform panel;
    [SerializeField]
    Vector2 hiddenPosition;
    [SerializeField]
    Vector2 shownPosition;

    private bool isPanelShown = false;
    public float duration = 0.5f;

    [Header("UIElements")]
    public Toggle statesToggle;
    public Toggle thresholdToggle;
    public Toggle rangeToggle;
    public Toggle neighborhoodToggle;
    public Toggle warpToggle;
    public Toggle colorsToggle;

    public Slider statesMin;
    public Slider statesMax;
    public Slider thresholdMin;
    public Slider thresholdMax;
    public Slider rangeMin;
    public Slider rangeMax;

    public TMP_Text statesTextMin;
    public TMP_Text statesTextMax;

    public TMP_Text thresholdTextMin;
    public TMP_Text thresholdTextMax;

    public TMP_Text rangeTextMin;  
    public TMP_Text rangeTextMax;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        hiddenPosition = panel.localPosition;
        shownPosition = panel.localPosition + new Vector3(0, panel.rect.height, 0);
    }
    private void Start()
    {
        statesMin.onValueChanged.AddListener(SetStatesMin);
        statesMax.onValueChanged.AddListener(SetStatesMax);
        thresholdMin.onValueChanged.AddListener(SetThresholdMin);
        thresholdMax.onValueChanged.AddListener(SetThresholdMax);
        rangeMin.onValueChanged.AddListener(SetRangeMin);
        rangeMax.onValueChanged.AddListener(SetRangeMax);
        InitUI();
    }
    public void InitUI()
    {
        statesTextMin.text = "" + statesMin.value;
        statesTextMax.text = "" + statesMax.value;
        thresholdTextMin.text = "" + thresholdMin.value;
        thresholdTextMax.text = "" + thresholdMax.value;
        rangeTextMin.text = "" + rangeMin.value;
        rangeTextMax.text = "" + rangeMax.value;

        statesMin.minValue = 1;
        statesMin.maxValue = grid.maxRandState;
        statesMax.minValue = 1;
        statesMax.maxValue = grid.maxRandState;
        statesMax.value = statesMax.maxValue;

        thresholdMin.minValue = 1;
        thresholdMin.maxValue = grid.maxThreshold;
        thresholdMax.minValue = 1;
        thresholdMax.maxValue = grid.maxThreshold;
        thresholdMax.value = thresholdMax.maxValue;

        rangeMin.minValue = 1;
        rangeMin.maxValue = grid.maxRange;
        rangeMax.minValue = 1;
        rangeMax.maxValue = grid.maxRange;
        rangeMax.value = rangeMax.maxValue;


    }
    public void SetStatesMin(float f)
    {
        statesMin.value = f;
        statesTextMin.text = ""+(int)f;
        grid.minRandState = (int)f-1;
    }
    public void SetStatesMax(float f)
    {
        statesMax.value = f;
        statesTextMax.text = "" + (int)f;
        grid.maxRandState = (int)f-1;

    }
    public void SetThresholdMin(float f)
    {
        thresholdMin.value = f;
        thresholdTextMin.text = "" + (int)f;
        grid.minThreshold = (int)f;

    }
    public void SetThresholdMax(float f)
    {
        thresholdMax.value = f; 
        thresholdTextMax.text = "" + (int)f;
        grid.maxThreshold = (int)f;
    }
    public void SetRangeMin(float f)
    {
        rangeMin.value = f; 
        rangeTextMin.text = "" + (int)f;
        grid.minRange = (int)f;
    }
    public void SetRangeMax(float f)
    {
        rangeMax.value = f; 
        rangeTextMax.text = "" + (int)f;
        grid.maxRange = (int)f;
    }

    public void ToggleStates()
    {
        grid.changeState = !grid.changeState;
    }
    public void ToggleThreshold()
    {
        grid.changeThreshold = !grid.changeThreshold;
    }
    public void ToggleRange()
    {
        grid.changeRange = !grid.changeRange;
    }
    public void ToggleNeighbor()
    {
        grid.changeNeighborhood = !grid.changeNeighborhood;
    }
    public void ToggleWarp()
    {
        grid.changeWarp = !grid.changeWarp;
    }
    public void ToggleColors()
    {
        grid.changeColor = !grid.changeColor;
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
