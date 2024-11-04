using System.Collections;
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
    public float base_move_speed = 12f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    bool isGrounded;
    bool isJumping = false;
    bool hasJumped = false;
    float target_speed;
    float gravity_acceleration = 2.4f;

    public float jumpHeight = 1.7f;
    public float decelerationFactor = 5f;
    public float airControl = 10;
    
    bool startedSound = false;
    bool wasGrounded;
    public Animator animator;
    public float range;

    public Transform player;

    public float speedMultiplier = 1f;

    private bool holdShift = false;
    private float runElapsedTime;
    private StaminaBar staminaBar;

    [Header("Sound effects")]
    public AudioSource movementSound;
    public AudioSource landSound;
    public AudioSource jumpSound;

    private void Start()
    {
        staminaBar = GetComponent<PlayerController>().GetStaminaBar();
    }

    /// <summary>
    /// Sets data from Input to values to move player object
    /// </summary>
    /// <param name="value">2D Vector (x,y(z))</param>
    public void Move(Vector2 value)
    {
        if (!isJumping)
        {
            movement_x = value.x;
            movement_z = value.y;
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 input_direction = (player.transform.right * movement_x + player.transform.forward * movement_z).normalized;
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

        if (holdShift)
        {
            target_speed = sprint_speed * speedMultiplier;

            if (staminaBar.GetCurrentStamina() > 0.2)
            {
                staminaBar.ReduceStamina(0.2);
            }
        }
        else
        {
            target_speed = base_move_speed;
        }



        if (isGrounded && wasGrounded && hasJumped == true) // Landing after a jump
        {
            PlayLandSound(); // Play the landing sound
            wasGrounded = false;
            hasJumped = false; // Reset the jump after landing
            
        }

        if (!isGrounded) // Reduce target speed by 20%
        {
            target_speed *= 0.8f;
            StopFootstepSound();
            wasGrounded = true;
        }

        if (!isGrounded)
        {
            movement_speed -= decelerationFactor * Time.deltaTime; // slow down by decelerationFactor per second
            movement_speed = Mathf.Max(movement_speed, 5);
        }
        else
        {
            movement_speed = target_speed * speedMultiplier;
        }

        movement_direction = input_direction; // you don't need to modify direction for air control, just speed
        
        playerController.Move(movement_direction * movement_speed * Time.deltaTime);

        if (isGrounded)
        {
            
            isJumping = false;
        }
        

        if (isGrounded)
        {
            wasGrounded = false;
            if (velocity.y < 0)
            {
                velocity.y = -2f; // keeps the player on the ground
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        playerController.Move(velocity * Time.deltaTime * gravity_acceleration);

        if (isGrounded && (movement_x != 0 || movement_z != 0))
        {
            PlayFootstepSound();
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayJumpSound(); // Play jump sound
            staminaBar.ReduceStamina(15);
            isJumping = true;
            hasJumped = true;
        }
    }

    public void Run()
    {
        holdShift = !holdShift;
    }

    public void RestoreMovementSpeed()
    {
        speedMultiplier = 1f;
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
    }
}
