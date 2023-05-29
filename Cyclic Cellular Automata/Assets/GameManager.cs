using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MyGrid grid;

    //area1
    public TMP_Text gridSizeText;
    public Button generateGridButton;
    public Slider gridSizeSlider;

    //area2
    public Button playButton;
    public Button pauseButton;
    public Button nextFrameButton;
    public Slider speedSlider;
    public TMP_Text speedText;

    //area3
    public TMP_Text statesText;
    public Slider statesSlider;
    public TMP_Text thresholdText;
    public Slider thresholdSlider;
    public TMP_Text rangeText;
    public Slider rangeSlider;
    public TMP_Dropdown neighborhoodDropdown;
    public bool warping = false;
    public Button colorSelectorButton;




}
