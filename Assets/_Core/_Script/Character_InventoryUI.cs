// InventoryUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Character_Inventory characterInventory;

    Inventory_Slot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
        characterInventory.slots = new List<Inventory_Slot>(slots);
        UpdateUI();
    }

    void Update()
    {
        // Atualiza a UI do inventário, se necessário.
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
    }
}
