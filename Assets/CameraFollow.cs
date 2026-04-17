using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // The player object to follow
    public float smoothSpeed = 0.125f; 
    public Vector3 offset;  

    private float initialY;  // Store the initial Y position of the camera

    void Start()
    {
        // init camera y position
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
      
        float desiredY = Mathf.Max(player.position.y, initialY);  

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 desiredPosition = new Vector3(0f, desiredY, transform.position.z) + offset;  // inputs are x and z are fixed, y is player start then upwards
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); //the inputs are current position, the desired position, and then the interpspeed.

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}