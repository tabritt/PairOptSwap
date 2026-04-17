using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position; // Store the initial position of the player
    }

    public void ResetPosition()
    {
        transform.position = startingPosition; // Reset the player's position to the starting position
    }
}
