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
    public float gravity = -15f;
    bool isGrounded;

    public float jumpHeight = 2f;
    void Update(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        movement_x = Input.GetAxisRaw("Horizontal");
        movement_z = Input.GetAxisRaw("Vertical");
        movement_direction = (transform.right * movement_x + transform.forward * movement_z).normalized;
        playerController.Move(movement_direction * movement_speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift)){
            movement_speed = sprint_speed;

        }else {
            movement_speed = base_move_speed;
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
        //jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
