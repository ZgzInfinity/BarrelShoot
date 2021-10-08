
/*
 * ------------------------------------------
 * -- Project: First Person Shooter ---------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 8/10/2021 -----------------------
 * ------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that controls the movement of the player
 */

public class PlayerMovement : MonoBehaviour
{

    // Speed of the player
    public float speed = 10f;

    // Gravity force
    public float gravity = 9.8f;

    // Force of the player to make a jump
    public float jumpHeight = 3f;

    // Reference to the ground checker position
    public Transform groundCheck;

    // Detection of movement in axis X and Y
    private float horizontalInput;
    private float verticalInput;

    // Distance to considerate that the player is on the floor
    private float groundDistance = 0.35f;

    // Resultant vector to store the speed of the player
    private Vector3 velocity;

    // Controls if the player is on the floor
    private bool isGrounded;

    // Controls if the player can jump
    private bool isAbleToJump;

	// Layer of the floor
    public LayerMask groundMask;

    // Make the movement of the player
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the data
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check the input of the player
        ReadInputs();
        // Check if the player is on the floor or jumping
        CheckGround();
        // Make the movement of the player
        Movement();
    }

    // Check if the player is on the floor
    private void CheckGround()
    {
        // Get if the player is on the floor
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }


    // Read the inputs
    private void ReadInputs()
    {

        // Check the movement in both axis X and Y
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check if the player presses the key to kump
        if (Input.GetButtonDown("Jump"))
        {
            // Can jump
            isAbleToJump = true;
        }
        else
        {
            // Cannot jump
            isAbleToJump = false;
        }
    }

    // Make the movement of the player
    private void Movement()
    {
        // Check if the player is jumping or falling to the floor
        if (isGrounded && velocity.y < 0f)
        {
            // Player on the floor
            velocity.y = 0f;
        }

        // Calculate the movement directions in both axis
        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;

        // Move the player with a normalized module
        Vector3 movementDirection = Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f);
        characterController.Move(movementDirection * speed * Time.deltaTime);

        // Make the jump because key was pressed and is on the floor
        if (isAbleToJump && isGrounded)
        {
            // Calculate the velocity to jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        }

        // Decrease the height due to the gravity force and moe the player
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
