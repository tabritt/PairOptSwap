using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //simple game but i still seperated health from the player controller script
    public int maxHealth = 50;
    public int currentHealth;

    public Text healthText;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        UpdateHealthUI();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Restore health when picking up a health item
    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);  // Ensure health doesn't exceed max
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
    void Die()
    {
        Destroy(gameObject);
    }
}