using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public int intValue = 123;

    private void Update()
    {
        // Map the integer to the range of 0-1
        float normalizedValue = intValue ;

        // Set the color channels based on the normalized value
        Color color = new Color(normalizedValue, normalizedValue * 0.5f, 255f - normalizedValue);

        GetComponent<SpriteRenderer>().color = color;
    }
}
