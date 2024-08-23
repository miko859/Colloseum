using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth; 
    private Animator animator;
    public HealthBar healthBar;
    private Weapon weapon;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; 
        healthBar.SetMaxHealth(maxHealth); 
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
            //animator.SetBool("EnemyDeath", true);
            transform.tag = "DeadEnemy";

            animator.enabled = false;
            
            /*foreach (Rigidbody rb in GetComponentInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }*/

            if (weapon != null)
            {
                weapon.transform.SetParent(null);
                Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
                if (weaponRb != null)
                {
                    weaponRb.isKinematic = false;
                    weaponRb.useGravity = true;
                }
            }
            
        }
       // Debug.Log(currentHealth);
    }


    public void DealDamage(int damage)
    {
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth); 
        //Debug.Log("Damage taken: " + damage + ". Current Health: " + currentHealth);
    }
}
