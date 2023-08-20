using System.Collections;
using UnityEngine;

public class Behavior_Projectile : MonoBehaviour
{
    [SerializeField] private float lifespan = 3f;
    private Rigidbody2D myRig;
    private Vector3 velocity = Vector3.zero;
    private bool isMonster = false;

    // Methods
    public IEnumerator Init(Vector2 direction, float moveSpeed, bool _isMonster = false)
    {
        isMonster = _isMonster;

        myRig = GetComponent<Rigidbody2D>();

        yield return new WaitUntil(() => myRig != null);

        velocity = direction * moveSpeed;
        StartCoroutine(SelfDestruct());

        Gameplay_SoundLibrary.instance.PlaySound("PlayerCast");
    }

    private void Update()
    {
        if (velocity != Vector3.zero)
        {
            myRig.velocity = velocity;
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifespan);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isMonster)
        {
            // Detect player and call the TakeDamage method
            Character_Controller player = collision.gameObject.GetComponent<Character_Controller>();
            if (player != null)
            {
                player.TakeDamage(10);
            }

            Destroy(gameObject);
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("Monster") && !isMonster)
        {
            // Detect monster and call the TakeDamage method
            Behavior_Enemy enemy = collision.gameObject.GetComponent<Behavior_Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }

            Debug.Log("Monster");

            Destroy(gameObject);
            StopAllCoroutines();
        }
        else if (collision.gameObject.CompareTag("Crate"))
        {
            Behavior_Crate crate = collision.gameObject.GetComponent<Behavior_Crate>();
            if (crate != null)
            {
                crate.TakeDamage(10);
            }

            Debug.Log("Crate");

            Destroy(gameObject);
            StopAllCoroutines();
        }

    }
}
