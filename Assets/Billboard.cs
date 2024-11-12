using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera camera;

    /// <summary>
    /// Health points bars of enemies are facing player
    /// </summary>
    void LateUpdate()
    {
        transform.LookAt(camera.transform);
        transform.Rotate(0, 180, 0);
    }
}