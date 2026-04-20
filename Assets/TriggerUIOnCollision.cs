using UnityEngine;
using UnityEngine.UI;  // For UI Button
using UnityEngine.SceneManagement;  // To reload the current scene

public class TriggerUIOnCollision : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI Panel that will appear
    public Button restartButton; // Reference to the button that will restart the level

    private bool isPlayerInTrigger = false; // To check if the player is inside the collider
    private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        // Find the PlayerController component attached to the player
        playerController = FindObjectOfType<PlayerController>();

        // Make sure the UI is initially hidden
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        // Ensure the restart button is hooked up and has a listener
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartLevel); // Bind the restart function to the button
        }
    }

    void Update()
    {
        // If the player is in the trigger, listen for the 'interact' key (optional, if you want other behavior)
        if (isPlayerInTrigger && playerController != null)
        {
            // For example, you can unfreeze and close the UI on a button press (like the "E" key or any other key)
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseUI();
            }
        }
    }

    // This will be called when the player enters the trigger area (Box Collider set to Trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the collider (you can also check for a specific tag here)
        if (collision.CompareTag("Player"))
        {
            // Show the UI
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
            }

            // Freeze player movement (by disabling movement inputs)
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            isPlayerInTrigger = true;
        }
    }

    // This will be called when the player exits the trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide the UI
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);
            }

            // Unfreeze player movement (by enabling movement inputs again)
            if (playerController != null)
            {
                playerController.enabled = true;
            }

            isPlayerInTrigger = false;
        }
    }

    // Method to restart the level (reload the current scene)
    private void RestartLevel()
    {
        // Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to close the UI manually (e.g., with a button press)
    private void CloseUI()
    {
        // Hide the UI
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

        // Unfreeze player movement
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        isPlayerInTrigger = false;
    }
}