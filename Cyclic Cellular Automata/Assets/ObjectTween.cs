using UnityEngine;
using DG.Tweening;

public class ObjectTween : MonoBehaviour
{
    Transform targetObject;
    [SerializeField]
     Vector3 hiddenPosition;

    [SerializeField]
    Vector3 shownPosition;

    public  bool isObjectShown = true;
    public float duration = 0.5f;
    private void Start()
    {
        targetObject = transform;

    }
    public void ToggleObject()
    {
        if (isObjectShown)
        {
            // Hide the object
            targetObject.DOMove(hiddenPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isObjectShown = false);
        }
        else
        {
            // Show the object
            targetObject.DOMove(shownPosition, duration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => isObjectShown = true);
        }
    }
    public void CalculateCamPosition(Vector2Int gridSize)
    {
        hiddenPosition = new Vector3(gridSize.x * 0.85f, gridSize.y / 2,0);
        shownPosition = new Vector3(gridSize.x /2, gridSize.y / 2,0);
    }
    public void TeleportToHiddenPosition()
    {
        isObjectShown = false;
        targetObject.transform.position = hiddenPosition;
    }
}

