using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            float scale = rectTransform.localScale.x;
            if (scale != 0)
            {
                rectTransform.anchoredPosition += eventData.delta / scale;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
