using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkerScript : MonoBehaviour
{

    [SerializeField] float moveDistance = 0.5f; // Set the desired distance to move in the Inspector
    [SerializeField] float moveSpeed = 0.5f;
    private Vector3 initialPosition; // Store the initial position of the object
    private bool reachedTop = false, reachedBottom = true;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (reachedBottom)
        {
            // Move the object upwards
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // Check if the object has reached the desired top position
            if (transform.position.y >= initialPosition.y + moveDistance)
            {
                // If reached, set reachedBottom to false and reachedTop to true
                reachedBottom = false;
                reachedTop = true;
            }
        }
        else if (reachedTop)
        {
            // Move the object downwards
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            // Check if the object has returned to the initial position
            if (transform.position.y <= initialPosition.y)
            {
                // If reached, set reachedTop to false
                reachedTop = false;
                reachedBottom = true;
            }
        }
    }
}
