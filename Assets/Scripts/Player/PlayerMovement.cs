using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movement_x;
    float movement_z;
    float sprint_speed = 20f;
    bool sprint = false;
    public CharacterController playerController;
    Vector3 movement_direction;
    float movement_speed;
    public float base_move_speed = 10f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    bool isGrounded;
    float target_speed;
    float gravity_acceleration = 2.4f;


    public float jumpHeight = 1.7f;
    public float decelerationFactor = 5f;
    public float airControl = 10;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        movement_x = Input.GetAxisRaw("Horizontal");
        movement_z = Input.GetAxisRaw("Vertical");
        Vector3 input_direction = (transform.right * movement_x + transform.forward * movement_z).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            target_speed = sprint_speed;
        }
        else
        {
            target_speed = base_move_speed;
        }

        if (!isGrounded)
        {
            target_speed *= 0.8f; // Reduce target speed by 20%
        }

        // AIR INERTIA
        if (!isGrounded)
        {
            // air drag
            movement_speed -= decelerationFactor * Time.deltaTime; // slow down by decelerationFactor per second
            movement_speed = Mathf.Max(movement_speed, 5);
        }
        else
        {
            movement_speed = target_speed;
        }

        movement_direction = input_direction; // you don't need to modify direction for air control, just speed

        playerController.Move(movement_direction * movement_speed * Time.deltaTime);
        // GRAVITY
        if (isGrounded)
        {
            if (velocity.y < 0)
            {

                velocity.y = -2f; // keeps the player on the ground
            }
        }
        else
        {
            // GRAVITY ACCELERATION
            velocity.y += gravity * Time.deltaTime;
        }

        playerController.Move(velocity * Time.deltaTime * gravity_acceleration);

        // JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}