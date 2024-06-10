using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    


    void Update()
    {
        if (health <= 0)
        {
            animator.SetBool("EnemyDeath", true);
        }
        Debug.Log(health);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blade")
        {
            health--;
        }
    }
}