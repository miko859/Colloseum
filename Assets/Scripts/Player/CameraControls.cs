using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    float mouse_x;
    float mouse_y;
    public Transform playerBody;
    float rotation_x = 0f;
    void Start(){
     Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        mouse_x = Input.GetAxis("Mouse X")* 130f * Time.deltaTime;
        mouse_y = Input.GetAxis("Mouse Y")* 130f * Time.deltaTime;
        rotation_x -= mouse_y;
        rotation_x = Mathf.Clamp(rotation_x, -90f, 90f);
        /*transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouse_x);*/
    }

    /*LateUpdate is called after all Update functions have been called. This is useful to order script execution. 
    For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.*/
    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouse_x);
    }
}
