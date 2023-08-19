using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter_Controller : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 6;
    [SerializeField] private float attackCooldown = 0.5f; // Cooldown time between attacks
    [SerializeField] private Transform characterTransform;
    [SerializeField] private string flipKeywords = "face,hood,torso"; // Add your keywords here
    [SerializeField] private int maxHealth = 100; // Maximum health of the character
    [SerializeField] private Transform projectileSpawnPoint; // New field for projectile starting point


    [Space]
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private float magicSpeed = 10f; // Speed of the lightning projectile

    // Hidden
    private bool wasFacingLeft = false; // New field to keep track of last direction faced
    private Animator animator;
    private List<SpriteRenderer> spriteRenderers;
    private HashSet<string> flipKeywordSet;
    private Rigidbody2D rb;
    private int currentHealth; // Current health of the character
    private bool isAttacking = false;
    private float attackCooldownTimer = 0; // Timer for attack cooldown
    private GameObject currentMagic;

    // Internals
    internal bool isMoving = false;
    internal bool isRunning = false;
    internal bool isHit = false; // Indicates if the character is hit by an enemy
    internal bool isDead = false;

    // Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderers = new List<SpriteRenderer>(characterTransform.GetComponentsInChildren<SpriteRenderer>());
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        currentMagic = Instantiate(magicPrefab, transform.position, Quaternion.identity);
        currentMagic.SetActive(false);

        // Create a HashSet of flip keywords
        string[] keywords = flipKeywords.Split(',');
        flipKeywordSet = new HashSet<string>(keywords);
    }

    private void Update()
    {
        if (!isDead)
        {
            MovementControl();

            if (Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0
                && !isMoving && !isRunning)
            {
                StartCoroutine(AttackControl());
            }
        }

        AnimatorControl();
    }

    private void MovementControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(horizontalInput, verticalInput).normalized;

        isMoving = dir.magnitude > 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isMoving = false;
        }
        else
        {
            isRunning = false;
        }

        // Flip the character sprites based on direction and keywords
        foreach (var renderer in spriteRenderers)
        {
            if (horizontalInput != 0) // Only change if there's horizontal input
            {
                wasFacingLeft = horizontalInput < 0;
            }

            bool shouldFlip = wasFacingLeft && HasFlipKeyword(renderer.name);
            renderer.flipX = shouldFlip;
        }

        // Update character velocity based on running state
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = currentSpeed * dir;
    }

    private IEnumerator AttackControl()
    {
        isAttacking = true;
        attackCooldownTimer = attackCooldown;

        GameObject spawnedLightning = Instantiate(magicPrefab, projectileSpawnPoint.position, Quaternion.identity); // Use the new projectile starting point

        // Calculate the direction to shoot the lightning based on mouse position
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        // Apply force to the lightning
        Rigidbody2D lightningRb = spawnedLightning.GetComponent<Rigidbody2D>();
        lightningRb.velocity = direction * magicSpeed;

        yield return new WaitForSeconds(0.5f);

        while (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
    }

    private bool HasFlipKeyword(string name)
    {
        foreach (string keyword in flipKeywordSet)
        {
            if (name.Contains(keyword))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                isHit = true;
                // Play hit animation or feedback
            }
        }
    }

    private void Die()
    {
        isDead = true;
        // Play death animation or feedback
    }

    private void AnimatorControl()
    {
        // Set animator parameters based on movement and actions
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsAttacking", isAttacking);
        animator.SetBool("IsHit", isHit);
        animator.SetBool("IsDead", isDead);

        // Reset the isHit flag after updating the animator
        isHit = false;
    }
}
