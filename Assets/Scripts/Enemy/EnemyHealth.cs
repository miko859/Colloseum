using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth; 
    private Animator animator;
    public HealthBar healthBar; 

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; 
        healthBar.SetMaxHealth(maxHealth); 
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
            animator.SetBool("EnemyDeath", true);
        }
       // Debug.Log(currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blade")
        {
            TakeDamage(1); 
        }
        else if (other.gameObject.tag == "HeavyBlade")
        {
            TakeDamage(3);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        healthBar.SetHealth(currentHealth); 
        Debug.Log("Damage taken: " + damage + ". Current Health: " + currentHealth);
    }
}
