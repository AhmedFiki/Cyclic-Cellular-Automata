using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    Slider slider;
    public IntEvent onSliderValueChanged;

    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);

    }

    private void OnSliderValueChanged(float value)
    {
        int intValue = Mathf.RoundToInt(value);
        onSliderValueChanged.Invoke(intValue);
    }

}
