using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera follow
    public Vector3 offset; // Offset from the player 

    void LateUpdate()
    {
        // target position for the camera to follow
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y + offset.y, transform.position.z);

        // Smoothly interpolate 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // camera's position to the new smoothed position
        transform.position = smoothedPosition;
    }
}
