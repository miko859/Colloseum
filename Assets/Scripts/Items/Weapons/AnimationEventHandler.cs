using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    public GameObject weapon;

    private WeaponManager weaponManager;
    public Collider collider;

    private void Start()
    {
        weaponManager = weapon.GetComponent<WeaponManager>();
        if (collider == null)
        {
            collider = weapon.GetComponent<Collider>();
        }
    }

    private void AnimationFinishedTrigger()
    {
        Debug.Log("AnimationFinishedTrigger");
        weaponManager.SetHit(false);
        collider.isTrigger = false;
    }

    private void AnimationStartedTrigger()
    {
        Debug.Log("AnimationStartedTrigger");
        collider.isTrigger = true;
    }
}
