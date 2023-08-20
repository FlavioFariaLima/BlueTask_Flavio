using System.Collections;
using UnityEngine;

public class Behavior_Enemy : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float movementRadius = 5f;
    [SerializeField] private int maxHealth = 100;

    [Header("Loot")]
    [SerializeField] private GameObject lootPrefab;

    [Header("Spell")]
    [SerializeField] private GameObject magicProjectile;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float fireSpeed = 2f;
    [SerializeField] private Transform castPoint;

    private Transform player;
    private float timeSinceLastShot = 0f;
    private int currentHealth;
    private Vector2 initialPosition; 
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        initialPosition = transform.position;  // Store the initial position
        StartCoroutine(RandomMove());
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Check the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Debug Line
        Debug.DrawLine(transform.position, player.position, Color.red);

        if (distanceToPlayer <= detectionRange && timeSinceLastShot >= fireRate)
        {
            Debug.Log("Shooting at player!"); // Log message
            ShootMagic();
            timeSinceLastShot = 0f;
        }
    }

    private void ShootMagic()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is null!");
            return;
        }
        if (magicProjectile == null)
        {
            Debug.LogError("Magic Projectile is not assigned!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        
        Behavior_Projectile spawnedMagic = Instantiate(magicProjectile, castPoint.position , Quaternion.identity).GetComponent<Behavior_Projectile>();
        StartCoroutine(spawnedMagic.Init(direction, fireSpeed, true));
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 proposedPosition = (Vector2)transform.position + randomDirection * moveSpeed;

            // Check if the proposed position is within the movement radius
            if (Vector2.Distance(proposedPosition, initialPosition) <= movementRadius)
            {
                rb.velocity = randomDirection * moveSpeed;
            }
            else
            {
                rb.velocity = -randomDirection * moveSpeed;  // Move in the opposite direction
            }

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        Gameplay_SoundLibrary.instance.PlaySound("HitEnemy");
    }

    private void Die()
    {
        DropLoot();
        Destroy(gameObject);

        Gameplay_SoundLibrary.instance.PlaySound("DieEnemy");
    }


    private void DropLoot()
    {
        if (lootPrefab)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);

            Gameplay_SoundLibrary.instance.PlaySound("Drop");
        }
    }
}
