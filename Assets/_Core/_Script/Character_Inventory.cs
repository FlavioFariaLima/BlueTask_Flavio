using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Inventory : MonoBehaviour
{
    public Canvas canvas; 
    public int space = 10;
    public List<Inventory_Slot> slots = new List<Inventory_Slot>();

    [Space]
    public Dictionary<ItemType, Inventory_ItemBlueprint> equippedItems = new Dictionary<ItemType, Inventory_ItemBlueprint>();


    // Methods
    public bool Add(Inventory_ItemBlueprint item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].item)
            {
                slots[i].AddItem(item);
                return true;
            }
        }
        return false;
    }

    public void Remove(Inventory_ItemBlueprint item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].ClearSlot();
                break;
            }
        }
    }

    public void EquipItem(Inventory_ItemBlueprint itemToEquip)
    {
        if (itemToEquip.itemType != ItemType.Generic)
        {
            
            if (equippedItems.ContainsKey(itemToEquip.itemType))
            {
                Add(equippedItems[itemToEquip.itemType]);
                equippedItems.Remove(itemToEquip.itemType);
            }

            equippedItems[itemToEquip.itemType] = itemToEquip;
            Remove(itemToEquip);
        }
    }
}
