using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter_Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Item {itemName} added to inventory!");
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}
