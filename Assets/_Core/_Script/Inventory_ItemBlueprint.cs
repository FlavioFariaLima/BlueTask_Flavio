using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Inventory_ItemBlueprint : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int value;
}