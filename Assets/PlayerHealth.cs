using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //simple game but i still seperated health from the player controller script
    public int maxHealth = 50;
    public int currentHealth;

    public Text healthText;
    public Slider healthSlider;

    private ResetPlayer resetPlayer;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        resetPlayer = GetComponent<ResetPlayer>();

        UpdateHealthUI();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            resetPlayer.ResetPosition(); // Reset the player's position to the starting position

            // Die();
        }
    }
    // Restore health when picking up a health item
    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);  // Ensure health doesn't exceed max
    }

    // Central health adjustment method to handle both damage and healing
    public void AdjustHealth(int amount, bool isDamage)
    {
        if (isDamage)
            TakeDamage(amount);
        else
            RestoreHealth(amount); // Positive for healing

        healthSlider.value = currentHealth;  // Update the slider
        UpdateHealthUI();  // Update the text display

    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        }
    }
    // void Die()
    // {
    //     Destroy(gameObject);
    // }
}