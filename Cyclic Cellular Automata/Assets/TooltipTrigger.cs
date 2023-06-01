using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [TextArea(3, 10)]

    public string content;

   public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipHandler.Show(content);

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        TooltipHandler.Hide();

    }
}
