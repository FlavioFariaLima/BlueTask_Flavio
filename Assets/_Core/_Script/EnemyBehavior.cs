using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float movementRadius = 5f;  // Radius within which the monster can move
    [SerializeField] private Collider2D myTrigger;
    [SerializeField] private Transform castPoint;

    [Space]
    [SerializeField] private GameObject magicProjectile;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private int maxHealth = 100;

    private Transform player;
    private float timeSinceLastShot = 0f;
    private int currentHealth;
    private Vector2 initialPosition;  // Store the initial position of the monster
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
        
        ProjectileBehavior spawnedMagic = Instantiate(magicProjectile, castPoint.position , Quaternion.identity).GetComponent<ProjectileBehavior>();
        StartCoroutine(spawnedMagic.Init(myTrigger, direction, moveSpeed));
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
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
