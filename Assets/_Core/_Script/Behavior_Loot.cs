using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Loot : MonoBehaviour
{
    [SerializeField] internal List<Inventory_ItemBlueprint> itens;
    internal bool canLoot = true;

    // Methods
    internal void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
