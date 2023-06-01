using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipGameObject : MonoBehaviour
{
    public TMP_Text contentField;

    RectTransform rectTransform;

    public Vector3 offset;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string text)
    {
        contentField.text = text;
        HandlePivot();

        transform.position = Input.mousePosition+offset;
    }

    public void HandlePivot()
    {
        float pivotX=Input.mousePosition.x/Screen.width;
        float pivotY= Input.mousePosition.y/Screen.height;

        rectTransform.pivot=new Vector2 (pivotX, pivotY);
    }


}
