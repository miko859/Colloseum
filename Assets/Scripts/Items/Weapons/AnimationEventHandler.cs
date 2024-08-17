using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{

    public GameObject weapon;

    private WeaponManager weaponManager;
    private Collider collider;

    private void Start()
    {
        weaponManager = weapon.GetComponent<WeaponManager>();
        collider = weapon.GetComponent<Collider>();
    }

    private void AnimationFinishedTrigger()
    {
        weaponManager.SetHit(false);
        collider.isTrigger = false;
    }

    private void AnimationStartedTrigger()
    {
        collider.isTrigger = true;
    }


}
