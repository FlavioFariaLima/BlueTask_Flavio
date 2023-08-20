using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour, IDropHandler
{
    public Character_Inventory playerInventory;
    public Character_Economy playerEconomy;

    [Header("Store UI")]
    public Transform storeItemsParent;
    private List<Inventory_Slot> storeSlots = new List<Inventory_Slot>();

    private void Start()
    {
        storeSlots.AddRange(storeItemsParent.GetComponentsInChildren<Inventory_Slot>());
    }

    public void OnDrop(PointerEventData eventData)
    {
        Inventory_Slot droppedItemSlot = eventData.pointerDrag.GetComponent<Inventory_Slot>();

        if (droppedItemSlot && droppedItemSlot.item)
        {
            // Player selling to the store
            if (playerInventory.slots.Contains(droppedItemSlot))
            {
                playerEconomy.AddCoins(droppedItemSlot.item.value);
                playerInventory.Remove(droppedItemSlot.item);
                AddItemToStore(droppedItemSlot.item);
            }
            // Player buying from the store
            else if (storeSlots.Contains(droppedItemSlot))
            {
                if (playerEconomy.CanAfford(droppedItemSlot.item.value))
                {
                    playerEconomy.DeductCoins(droppedItemSlot.item.value);
                    playerInventory.Add(droppedItemSlot.item);
                    RemoveItemFromStore(droppedItemSlot);
                }
            }
        }
    }

    private void AddItemToStore(Inventory_ItemBlueprint item)
    {
        foreach (var slot in storeSlots)
        {
            if (!slot.item)
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    private void RemoveItemFromStore(Inventory_Slot slotToRemove)
    {
        slotToRemove.ClearSlot();
    }
}
