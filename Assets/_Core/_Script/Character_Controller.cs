using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Character_Inventory))]
public class Character_Controller : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 6;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private string flipKeywords = "face,hood,torso";
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [Header("Spell")]
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private float magicSpeed = 10f;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Inventory")]
    private Character_Inventory playerInventory;

    // Hidden
    private bool wasFacingLeft = false;
    private Animator animator;
    private List<SpriteRenderer> spriteRenderers;
    private HashSet<string> flipKeywordSet;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float attackCooldownTimer = 0;
    private GameObject currentMagic;

    // Internals
    internal bool isMoving = false;
    internal bool isRunning = false;
    internal bool isHit = false;
    internal bool isDead = false;

    // Unity Methods
    private void Start()
    {
        playerInventory = GetComponent<Character_Inventory>();

        if (!playerInventory)
        {
            Debug.LogError("Inventory script not found! Please attach it to the Player.");
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collision is Loot?
        if (collision.gameObject.CompareTag("Loot"))
        {
            Behavior_Loot loot = collision.gameObject.GetComponent<Behavior_Loot>();

            if (loot.canLoot)
            {
                loot.canLoot = false;
                var items = loot.itens;

                if (playerInventory)
                {
                    foreach (Inventory_ItemBlueprint item in items)
                    {
                        playerInventory.Add(item);
                    }

                    loot.SelfDestruct(); // Destroys the loot object after it's collected
                }
            }
        }
    }


    // Methods
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

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - transform.position.z));
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        Behavior_Projectile spawnedMagic = Instantiate(magicPrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Behavior_Projectile>();
        StartCoroutine(spawnedMagic.Init(direction, magicSpeed));

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
            UpdateHealthUI();

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
        isMoving = false;
        isRunning = false;

        rb.velocity = Vector2.zero;
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

    private void UpdateHealthUI()
    {
        if (healthSlider)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }
}
