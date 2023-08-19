using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float lifespan = 3f;  // How long before the projectile is auto-destroyed
    private Collider2D myTrigger;
    private Rigidbody2D myRig;
    private Vector3 velocity = Vector3.zero;

    // Methods
    public IEnumerator Init(Collider2D _myTrigger, Vector2 direction, float moveSpeed)
    {
        myTrigger = _myTrigger;
        myRig = GetComponent<Rigidbody2D>();

        yield return new WaitUntil(() => myTrigger != null && myRig != null);

        velocity = direction * moveSpeed;
        StartCoroutine(SelfDestruct());
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
        if (collision != myTrigger)
        {

            Destroy(gameObject);

            if (collision.gameObject.CompareTag("Player"))
            {
                // Detect player and call the TakeDamage method
                PlayerCharacter_Controller player = collision.gameObject.GetComponent<PlayerCharacter_Controller>();
                if (player != null)
                {
                    player.TakeDamage(10);
                }
            }
            else if (collision.gameObject.CompareTag("Monster") && collision != myTrigger)
            {
                // Detect monster and call the TakeDamage method
                EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10);
                }

                Debug.Log("Monster");
            }
        }
    }

}
