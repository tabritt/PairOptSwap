using UnityEngine;

public class PlatformVelocity : MonoBehaviour
{
    public Vector2 velocity;
    private Vector2 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Update velocity based on movement
        velocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}