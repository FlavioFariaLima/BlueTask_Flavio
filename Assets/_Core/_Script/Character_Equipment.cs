using System.Collections.Generic;
using UnityEngine;

public class Character_Equipment : MonoBehaviour
{
    public List<SpriteRenderer> bootsRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> elbowsRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> torsoRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> headRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> faceRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> legsRenderers = new List<SpriteRenderer>();
    public List<SpriteRenderer> wristRenderers = new List<SpriteRenderer>();

    private Dictionary<ItemType, Inventory_ItemBlueprint> equippedItems = new Dictionary<ItemType, Inventory_ItemBlueprint>();

    internal Character_Economy playerEconomy;
    internal Character_Inventory playerInventory;

    private void Start()
    {
        playerEconomy = GetComponent<Character_Economy>();
        playerInventory = GetComponent<Character_Inventory>();
    }

    public void EquipItem(Inventory_ItemBlueprint item)
    {
        if (item && item.itemType != ItemType.Generic)
        {
            equippedItems[item.itemType] = item;
            UpdateEquipRenderers(item.itemType, item.icon);
        }
    }

    public void UnequipItem(ItemType itemType)
    {
        if (equippedItems.ContainsKey(itemType))
        {
            equippedItems.Remove(itemType);
            UpdateEquipRenderers(itemType, null);
        }
    }

    private void UpdateEquipRenderers(ItemType itemType, Sprite sprite)
    {
        List<SpriteRenderer> targetRenderers = null;

        switch (itemType)
        {
            case ItemType.Boots:
                targetRenderers = bootsRenderers;
                break;
            case ItemType.Elbows:
                targetRenderers = elbowsRenderers;
                break;
            case ItemType.Torso:
                targetRenderers = torsoRenderers;
                break;
            case ItemType.Head:
                targetRenderers = headRenderers;
                break;
            case ItemType.Face:
                targetRenderers = faceRenderers;
                break;
            case ItemType.Legs:
                targetRenderers = legsRenderers;
                break;
            case ItemType.Wrist:
                targetRenderers = wristRenderers;
                break;
        }

        if (targetRenderers != null)
        {
            foreach (var renderer in targetRenderers)
            {
                renderer.sprite = sprite;
            }
        }
    }
}
