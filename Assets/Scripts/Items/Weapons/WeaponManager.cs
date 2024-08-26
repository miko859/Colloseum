using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string target;
    private Collider blade;
    private bool hit = false;
    private WeaponAnimations weaponAnimations;

    /// <summary>
    /// Set blade for hitboxes and weaponAnimations to get DMG of actual attack
    /// </summary>
    void Start()
    {
        blade = GetComponent<Collider>();
        weaponAnimations = GetComponent<WeaponAnimations>();
    }

    /// <summary>
    /// Check if entity was hit by weapon and dealing dmg by attack, enemy <=> player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target) & !hit)
        {
            other.GetComponent<Health>().DealDamage(weaponAnimations.getDamage());
            Debug.Log("you hit " + target + " by damage " + weaponAnimations.getDamage());
            hit = true;
            
        }
    }

    public void SetHit(bool hit)
    {
        this.hit = hit;
    }
}
