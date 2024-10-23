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
    public bool hasDeathAnimation = false;

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
            if (transform.CompareTag("Enemy"))
            {
                if (hasDeathAnimation)
                {
                    animator.SetBool("death", true);
                }

                transform.tag = "Ground";
                animator.enabled = false;
                transform.GetComponent<NavMeshAgent>().enabled = false;
            }
            else if (transform.CompareTag("Player"))
            {
                Debug.Log("YOU HAVE DIED");
            }

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
    }


    public void DealDamage(int damage)
    {
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth); 
    }
}
