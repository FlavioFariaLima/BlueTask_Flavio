using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character_Inventory_UIPanel : MonoBehaviour
{
    public Character_Inventory inventory;
    public Character_Economy economy;
    public Store store;

    internal void Drop(Inventory_ItemBlueprint item, Inventory_Slot storeSlot)
    {
        if (economy.CanAfford(item.value))
        {
            economy.DeductCoins(item.value);
            inventory.Add(item);
            store.RemoveItemFromStore(storeSlot);
        }

    }
}
