using UnityEngine;

public enum ItemType
{
    Generic,
    Boots,
    Elbows,
    Torso,
    Head,
    Face,
    Legs,
    Wrist
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Inventory_ItemBlueprint : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public Sprite itemSprite;
    public int value;
}