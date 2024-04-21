using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the character's transform
    public Vector3 offset; // Offset of the camera relative to the character

    void LateUpdate()
    {
        // Check if the target (character) exists
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}