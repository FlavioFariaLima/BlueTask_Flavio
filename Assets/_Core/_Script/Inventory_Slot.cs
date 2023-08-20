// InventorySlot.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Inventory_ItemBlueprint item;
    public Image icon;

    public void OnDrag(PointerEventData eventData)
    {
        if (item)
        {
            icon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsPointerInUIObject())
        {
            // Largue o item se estiver fora do inventário
            ClearSlot();
        }

        icon.transform.localPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Implementar se necessário
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Implementar se necessário
    }

    private bool IsPointerInUIObject()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void AddItem(Inventory_ItemBlueprint newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
