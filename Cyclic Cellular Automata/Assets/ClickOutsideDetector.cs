using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOutsideDetector : MonoBehaviour
{
    GameObject grid;
    private void Awake()
    {
        grid = GetComponent<GameObject>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverGrid())
        {
            // Call the HideGrid function or perform the desired actions
            GetComponent<ColorGridPanel>().HideGrid();
        }
    }

    private bool IsPointerOverGrid()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Create a list to store raycast results
        var results = new List<RaycastResult>();

        // Raycast using the current event data
        EventSystem.current.RaycastAll(eventData, results);
        // Check if any of the raycast results are on the grid object
        foreach (var result in results)
        {
            if (result.gameObject == grid)
                return true;

            // Exclude buttons from being considered as clicks outside the grid
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
                return true;
        }
        return false;
    }
}

