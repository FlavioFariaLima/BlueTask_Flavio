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
            // Drop item
            ClearSlot();
        }

        icon.transform.localPosition = Vector3.zero;

        // Sell
        List<RaycastResult> hitObjects = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, hitObjects);

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject.CompareTag("Store"))
            {
                Store store = hit.gameObject.GetComponent<Store>();
                if (store)
                {
                    store.OnDrop(eventData);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       //
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
