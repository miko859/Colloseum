using UnityEngine;

public class DoubleDoorController : MonoBehaviour
{
    public Transform doorLeft;      // The left door object
    public Transform doorRight;     // The right door object

    public float leftOpenZPosition;  // The z position where the left door should move when opened
    public float rightOpenZPosition; // The z position where the right door should move when opened
    private Vector3 leftClosedPosition;  // The initial position of the left door
    private Vector3 rightClosedPosition; // The initial position of the right door

    public float speed = 2.0f;       // Speed at which the doors move
    private bool isOpen = false;     // Condition to check if the doors are open

    void Start()
    {
        // Initialize the doors to their closed positions
        leftClosedPosition = doorLeft.position;
        rightClosedPosition = doorRight.position;
    }

    void Update()
    {
        // Check if the condition to open the doors is met
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Toggle the door state
            isOpen = !isOpen;
        }

        // Determine the target z positions
        float leftTargetZ = isOpen ? leftOpenZPosition : leftClosedPosition.z;
        float rightTargetZ = isOpen ? rightOpenZPosition : rightClosedPosition.z;

        // Move the doors to their target positions, keeping x and y constant
        doorLeft.position = new Vector3(doorLeft.position.x, doorLeft.position.y, Mathf.Lerp(doorLeft.position.z, leftTargetZ, Time.deltaTime * speed));
        doorRight.position = new Vector3(doorRight.position.x, doorRight.position.y, Mathf.Lerp(doorRight.position.z, rightTargetZ, Time.deltaTime * speed));
    }
}
