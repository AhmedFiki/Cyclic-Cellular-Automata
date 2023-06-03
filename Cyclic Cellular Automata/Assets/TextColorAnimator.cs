using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextColorAnimator : MonoBehaviour
{
    TMP_Text textComponent;
    public ColorPalette palette;
    public float colorChangeInterval = 1f;
    public ColorsPanel panel;
    private Coroutine colorChangeCoroutine;

    public TextColorAnimator tca2;
    public TextColorAnimator tca3;
    private void Start()
    {
        // Start the color change coroutine
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(AnimateTextColor());
    }
    public void SetPalette(ColorPalette p)
    {
        palette = p;
        tca2.palette = p;
        tca3.palette = p;

    }
    public IEnumerator AnimateTextColor()
    {
        int colorIndex = 0;

        while (true)
        {
            // Get the current color
            Color currentColor = palette[colorIndex];

            // Assign the color to the text component
            textComponent.color = currentColor;

            // Increment the color index
            colorIndex = (colorIndex + 1) % palette.GetColors().Length;

            // Wait for the specified interval before changing the color again
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }
}
