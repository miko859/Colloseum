using UnityEngine;

public class DoubleDoorController : MonoBehaviour
{
    public Transform doorLeft;      // The left door object
    public Transform doorRight;     // The right door object

    public Vector3 leftOpenPosition;  // The position where the left door should move when opened
    public Vector3 rightOpenPosition; // The position where the right door should move when opened
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

        // Move the doors to their target positions
        Vector3 leftTargetPosition = isOpen ? leftOpenPosition : leftClosedPosition;
        Vector3 rightTargetPosition = isOpen ? rightOpenPosition : rightClosedPosition;

        doorLeft.position = Vector3.Lerp(doorLeft.position, leftTargetPosition, Time.deltaTime * speed);
        doorRight.position = Vector3.Lerp(doorRight.position, rightTargetPosition, Time.deltaTime * speed);
    }
}
