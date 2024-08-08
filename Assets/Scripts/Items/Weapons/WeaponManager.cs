using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string target;
    public Collider blade;
    public bool hit = false;
    private int lightAttackDmg = 1;
    private int hardAttackDmg = 4;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target) & !hit)
        {
            other.GetComponent<Health>().DealDamage(lightAttackDmg);
            Debug.Log("you hit " + target + " by damage " +  lightAttackDmg);
            //blade.isTrigger = false;
            hit = true;
            
        }
    }

    public void SetHit(bool hit)
    {
        this.hit = hit;
    }
}
