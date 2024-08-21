using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movement_x;
    float movement_z;
    float sprint_speed = 20f;
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
    bool isJumping = false;
    float target_speed;
    float gravity_acceleration = 2.4f;

    public float jumpHeight = 1.7f;
    public float decelerationFactor = 5f;
    public float airControl = 10;
    public AudioSource movementSound;
    public AudioSource landSound;
    public AudioSource jumpSound;
    bool startedSound = false;
    bool wasGrounded;
    public float range;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isJumping)
        {
            movement_x = Input.GetAxisRaw("Horizontal");
            movement_z = Input.GetAxisRaw("Vertical");

            animator.SetFloat("VelocityX", movement_x);
            animator.SetFloat("VelocityZ", movement_z);
        }

        Vector3 input_direction = (transform.right * movement_x + transform.forward * movement_z).normalized;

        if ((movement_x != 0 || movement_z != 0) && isGrounded)
        {
            if (!startedSound || wasGrounded)
            {
                startedSound = true;
                StartCoroutine(StartMovementSoundWithDelay(0.2f)); // Start the sound with a delay
            }
        }
        else if (startedSound)
        {
            // Stop movement
            StartCoroutine(StopMovementSoundAfterFinish());
            startedSound = false;
        }

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
            target_speed *= 0.8f;
            StopFootstepSound();
            wasGrounded = true;
        }

        if (!isGrounded && wasGrounded == true)
        {
            PlayLandSound(); // plays the sound when the player reaches the ground 
            wasGrounded = false;
            
        }

        if (!isGrounded)
        {
            movement_speed -= decelerationFactor * Time.deltaTime;
            movement_speed = Mathf.Max(movement_speed, 5);
        }
        else
        {
            movement_speed = target_speed;
        }

        movement_direction = input_direction;

        playerController.Move(movement_direction * movement_speed * Time.deltaTime);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        playerController.Move(velocity * Time.deltaTime * gravity_acceleration);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayJumpSound(); // Play jump sound
            animator.SetBool("Jump", true);
            isJumping = true;
        }
        else if (isGrounded)
        {
            animator.SetBool("Jump", false);
            isJumping = false;
        }

        if (isGrounded && (movement_x != 0 || movement_z != 0))
        {
            PlayFootstepSound();
        }
    }

    IEnumerator StartMovementSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (startedSound) // Ensure the sound starts only if the player is still moving
        {
            movementSound.enabled = true;
            PlayFootstepSound();
        }
    }

    IEnumerator StopMovementSoundAfterFinish()
    {
        yield return new WaitWhile(() => movementSound.isPlaying); // Wait until the sound finishes playing
        movementSound.Stop(); // Stop the sound
        movementSound.enabled = false; //reset the delay
    }

    void PlayFootstepSound()
    {
        if (movementSound == null) return;
        movementSound.pitch = Random.Range(0.8f, 1f); // Adjust pitch randomly
        if (!movementSound.isPlaying)
        {
            movementSound.Play(); // Play the sound if not playing already 
        }
    }

    void StopFootstepSound()
    {
        if (movementSound == null) return;
        if (movementSound.isPlaying)
        {
            movementSound.Stop(); // Stop the sound if playing
        }
    }

    void PlayJumpSound()
    {
        if (jumpSound == null) return;
        jumpSound.Play(); 
    }

    void PlayLandSound()
    {
        if (landSound == null) return;
        landSound.Play();
        {
            
        }
    }
}
