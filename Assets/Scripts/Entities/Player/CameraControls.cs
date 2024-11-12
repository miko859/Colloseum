using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float mouseSensitivity = 130f;

    float mouse_x;
    float mouse_y;
    public Transform playerBodyYRotation;
    public Transform bodyXRotation;
    float rotation_x = 0f;
    float rotation_y = 0f;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        
        mouse_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouse_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotation_x -= mouse_y;
        rotation_x = Mathf.Clamp(rotation_x, -90f, 90f);
        rotation_y += mouse_x;

        bodyXRotation.transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        playerBodyYRotation.transform.localRotation = Quaternion.Euler(0f, rotation_y, 0f);
    }
}
