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
    public float airControl = 0.1f;

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
            target_speed *= 0.6f; // Reduce target speed by 40%
        }

        // AIR INERTIA
        //lerp = smooth transmission from current move speed (movement_speed) into target_speed.
        //deltaTime * 5f --> speed of the change.
        movement_speed = Mathf.Lerp(movement_speed, target_speed, Time.deltaTime * 5f);


        movement_speed = Mathf.Lerp(movement_speed, target_speed, Time.deltaTime * 5f);

        if (isGrounded)
        {
            movement_direction = input_direction;
        }
        else
        {
            // Smoothly change direction in the air
            movement_direction = Vector3.Lerp(movement_direction, input_direction, Time.deltaTime * airControl);
        }

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
