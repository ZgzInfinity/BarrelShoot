
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
 * Script that controls the behaviour of the player's gun
 */

public class Gun : MonoBehaviour
{

    // Reference to the position of the camera
    public Transform playerCamera;

    // Distance of the raycast
    public float shootDistance = 10f;

    // Impact force of the bullet 
    public float impactForce = 5f;

    // Object data with the raycast crashes
    private RaycastHit shootRayCastHit;

    // Layers of the collisionable objects
    public LayerMask shootMask;

    // Particle effects to simulate the shoot
    public ParticleSystem shootParticles;

    // Bullet signal of the shoot
    public GameObject hitEffect;

    // Explosion animation of the shoot
    public GameObject destroyEffect;

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the button to shoot
        if (Input.GetButtonDown("Fire1"))
        {
            // Executes the shoot
            Shoot();
        }
    }

    // Shooting of the player
    private void Shoot()
    {
        // Check if the raycast detects a shootable object
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out shootRayCastHit, shootDistance, shootMask))
        {
            // Play the shooting animation and draw the bullet signal in its surface
            shootParticles.Play();
            Instantiate(hitEffect, shootRayCastHit.point, Quaternion.LookRotation(shootRayCastHit.normal), shootRayCastHit.transform);

            // Check if the collisionable object has rigidbody to apply a force
            if (shootRayCastHit.collider.GetComponent<Rigidbody>() != null)
            {
                // Apply force to move the object in the same direction of the shoot
                shootRayCastHit.collider.GetComponent<Rigidbody>().AddForce(impactForce * -shootRayCastHit.normal);
            }

            // Check if the collisionable object is a explosive barrel
            if (shootRayCastHit.collider.CompareTag("Barrel")){
                // Destroy the barrel with an explosion
                Instantiate(destroyEffect, shootRayCastHit.point, Quaternion.LookRotation(shootRayCastHit.normal));
                Destroy(shootRayCastHit.collider.gameObject);

                // Increment the score
                LevelManager.Instance.levelScore++;
            }
        }
    }
}
