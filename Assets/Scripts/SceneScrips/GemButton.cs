using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemButton : Interactable
{
    [SerializeField] private bool status = false;
    [SerializeField] private SecretRoomLogic secretRoomLogic;
    [SerializeField] private Animator animator;

    private void Start()
    {
        secretRoomLogic = GetComponentInParent<SecretRoomLogic>();
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        status = !status;
        secretRoomLogic.OnChanged();

        if (status)
        {
            animator.Play("push", 0);
        }
        else
        {
            animator.Play("back", 0);
        }
    }

    public bool GetStatus()
    {
        return status;
    }
}
