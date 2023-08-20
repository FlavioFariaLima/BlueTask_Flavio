// InventoryUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Character_Inventory characterInventory;

    [Space]
    public Transform equipmentParent;

    Inventory_Slot[] slots;
    Inventory_Slot[] equipmentSlots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
        characterInventory.slots = new List<Inventory_Slot>(slots);
        equipmentSlots = equipmentParent.GetComponentsInChildren<Inventory_Slot>();

        UpdateUI();
    }

    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < characterInventory.slots.Count && characterInventory.slots[i].item)
            {
                slots[i].AddItem(characterInventory.slots[i].item);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        foreach (var item in characterInventory.equippedItems)
        {
            int slotIndex = (int)item.Key;
            if (slotIndex < equipmentSlots.Length)
            {
                equipmentSlots[slotIndex].AddItem(item.Value);
            }
        }
    }
}
