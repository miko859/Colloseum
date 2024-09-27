using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float mouseSensitivity = 130f;

    float mouse_x;
    float mouse_y;
    public Transform playerBody;
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

        playerBody.transform.localRotation = Quaternion.Euler(rotation_x, rotation_y, 0f);
    }
}
