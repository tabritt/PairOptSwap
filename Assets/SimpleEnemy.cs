using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;        // Speed of movement
    public float moveDistance = 3f;     // Distance to walk left/right from start position

    private Vector2 startPos;
    private int direction = 1;         

    [Header("Damage")]
    public int damageToPlayer = 25;

    private void Start()
    {
        startPos = transform.position;  // Save the starting position
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move enemy horizontally
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Check distance from startPos
        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1; // Reverse direction

            
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

         
            startPos = transform.position;
        }
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.AdjustHealth(damageToPlayer, true); // Subtract health from player

            Destroy(gameObject);
        }
    }
}