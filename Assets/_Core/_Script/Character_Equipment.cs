using System.Collections.Generic;
using UnityEngine;

public class Character_Equipment : MonoBehaviour
{
    public SpriteRenderer bootsRendererRight;
    public SpriteRenderer bootsRendererLeft;
    public SpriteRenderer elbowsRendererRight;
    public SpriteRenderer elbowsRendererLeft;
    public SpriteRenderer torsoRenderer;
    public SpriteRenderer headRenderer;
    public SpriteRenderer faceRenderer;
    public SpriteRenderer legsRendererRight;
    public SpriteRenderer legsRendererLeft;
    public SpriteRenderer wristRendererRight;
    public SpriteRenderer wristRendererLeft;

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
            UpdateEquipRenderers(item.itemType, item.spriteMain, item.spriteAlt);
        }
    }

    public void UnequipItem(ItemType itemType)
    {
        if (equippedItems.ContainsKey(itemType))
        {
            equippedItems.Remove(itemType);
            UpdateEquipRenderers(itemType, null, null);
        }
    }

    private void UpdateEquipRenderers(ItemType itemType, Sprite spriteMain, Sprite spriteAlt)
    {
        switch (itemType)
        {
            case ItemType.Boots:
                bootsRendererRight.sprite = spriteMain;
                bootsRendererLeft.sprite = spriteAlt;
                break;
            case ItemType.Elbows:
                elbowsRendererRight.sprite = spriteMain;
                elbowsRendererLeft.sprite = spriteAlt;
                break;
            case ItemType.Torso:
                torsoRenderer.sprite = spriteMain;
                break;
            case ItemType.Head:
                headRenderer.sprite = spriteMain;
                break;
            case ItemType.Face:
                faceRenderer.sprite = spriteMain;
                break;
            case ItemType.Legs:
                legsRendererRight.sprite = spriteMain;
                legsRendererLeft.sprite = spriteAlt;
                break;
            case ItemType.Wrist:
                wristRendererRight.sprite = spriteMain;
                wristRendererLeft.sprite = spriteAlt;
                break;
        }
    }
}

