using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Offset from the player
    public float smoothSpeed = 0.125f; // Speed of camera follow

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate desired position based on player's position and offset
        Vector3 desiredPosition = target.position + offset;
        // Smoothly move the camera to desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player's position
        transform.LookAt(target.position);

    }
}
