using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string target;
    private int lightAttackDmg = 2;
    private int hardAttackDmg = 4;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(target))
        {
            other.GetComponent<Health>().DealDamage(lightAttackDmg);
            Debug.Log(target + " hit by damage " +  lightAttackDmg);
        }
    }
}
