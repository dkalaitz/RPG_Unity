using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController;
    private Animator animator;

    // Speed of character movement
    public float moveSpeed = 10f;
    public float rotationSpeed = 500f;

    public float groundCheckDistance = 2f; // Distance to check for ground
    public LayerMask groundLayer; // Layer mask for the ground
    CharacterCombat combat;

    void Start()
    {

        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        // Check if the Animator component is assigned
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

    }

    void Update()
    {
        if (!combat.isDead)
        {
            HandleMovement();
        }
        HandleGravity();

    }


    // Update is called once per frame
    void HandleMovement()
    {
        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }


        // Check if there's any movement input
        if (movementDirection != Vector3.zero)
        {
            // Calculate target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the character using the Character Controller
            characterController.Move(movementDirection * moveSpeed * Time.deltaTime);

            // Set the "isMoving" parameter in the animator to trigger the run animation
            animator.SetBool("isMoving", true);

        }
        else
        {
            // If there's no movement input, stop the run animation
            animator.SetBool("isMoving", false);
        }
    }

    void HandleGravity()
    {
        // Perform ground check
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        // If character is grounded, apply gravity
        if (!isGrounded && !characterController.isGrounded)
        {
            characterController.Move(Vector3.down * Time.deltaTime * 9.81f); // Apply gravity
        }

    }



}
