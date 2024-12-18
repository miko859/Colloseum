using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;
    public AudioSource doorSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.Play("open", 0);
        doorSound.Play();
    }
}