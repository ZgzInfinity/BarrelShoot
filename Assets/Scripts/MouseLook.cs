
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
 * Script that controls the movements with the mouse
 */

public class MouseLook : MonoBehaviour
{

    // Sensiility of the mouse
    public float mouseSensibility = 100f;

    // Reference to the position of the player
    public Transform playerTransform;

    // Rotation in axis X
    private float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the cursor in game scene
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the movements of the player in axis X and Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        // Apply the rotation in axis X and Y
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
