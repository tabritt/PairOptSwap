using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // How fast the platform moves
    public float distance = 5f; // How far the platform will move before teleporting back
    private Vector2 startPos; // Initial position of the platform
    private Vector2 targetPos; // The position where the platform will stop and teleport back

    private bool movingToTarget = true; // Flag to determine the direction of movement

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;  // Save the starting position

        // Calculate target position based on speed direction
        targetPos = startPos + (speed > 0 ? Vector2.right * distance : Vector2.left * distance);
    }

    void FixedUpdate()
    {
        if (movingToTarget)
        {
            // Move towards the target position
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, Mathf.Abs(speed) * Time.fixedDeltaTime));

            // Check if the platform has reached the target position
            if (Vector2.Distance(rb.position, targetPos) < 0.1f)
            {
                movingToTarget = false; // Start moving back to start
                // Recalculate target position after direction change
                targetPos = startPos - (targetPos - startPos);
            }
        }
        else
        {
            // Move towards the start position (teleport back logic)
            rb.MovePosition(Vector2.MoveTowards(rb.position, startPos, Mathf.Abs(speed) * Time.fixedDeltaTime));

            // Check if the platform has reached the starting position
            if (Vector2.Distance(rb.position, startPos) < 0.1f)
            {
                movingToTarget = true; // Start moving to the target again
                // Recalculate target position after direction change
                targetPos = startPos + (speed > 0 ? Vector2.right * distance : Vector2.left * distance);
            }
        }
    }
}