using UnityEngine;

public class Behavior_Loot : MonoBehaviour
{
    [SerializeField]
    private string itemName = "DefaultLoot";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter_Inventory playerInventory = collision.gameObject.GetComponent<PlayerCharacter_Inventory>();
            if (playerInventory)
            {
                playerInventory.AddItem(itemName);
                Destroy(gameObject); // Destroys the loot object after it's collected
            }
        }
    }
}
