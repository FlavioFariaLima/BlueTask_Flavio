using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Character_Inventory playerInventory;
    public Character_Economy playerEconomy;

    [Header("Store UI")]
    public Transform storeItemsParent;
    private List<Inventory_Slot> storeSlots = new List<Inventory_Slot>();

    [Header("Initial Store Items")]
    public List<Inventory_ItemBlueprint> initialStoreItems; // Lista de itens iniciais

    private void Start()
    {
        storeSlots.AddRange(storeItemsParent.GetComponentsInChildren<Inventory_Slot>());
        
        
        for (int i = 0; i < initialStoreItems.Count && i < storeSlots.Count; i++)
        {
            storeSlots[i].AddItem(initialStoreItems[i]);
        }
    }

    public void Drop(Inventory_ItemBlueprint item)
    {
        playerEconomy.AddCoins(item.value);
        playerInventory.Remove(item);
        AddItemToStore(item);
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

    internal void RemoveItemFromStore(Inventory_Slot slotToRemove)
    {
        slotToRemove.ClearSlot();
    }
}
