using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;  
    public Text healthText;
    public Slider healthSlider;

    void Start()
    {
        if (playerHealth != null)
        {
            //
            playerHealth.healthText = healthText;
            playerHealth.healthSlider = healthSlider;
        }
    }
}