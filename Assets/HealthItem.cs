using UnityEngine;

public class HealthItem : MonoBehaviour
{
    // Amount of health the item restores
    public int healthAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerHealth script attached
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthAmount);
                Destroy(gameObject);  // Destroy the health item after it is picked up
            }
        }
    }
}