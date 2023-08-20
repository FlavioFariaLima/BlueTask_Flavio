using UnityEngine;

public class Behavior_Crate : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private GameObject lootPrefab;

    // Hidden
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }

    private void DropLoot()
    {
        if (lootPrefab)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }
}
