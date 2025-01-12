using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public double maxHealth = 3;
    private double currentHealth;
    private Animator animator;
    public HealthBar healthBar;
    private Weapon weapon;
    public bool hasDeathAnimation = false;
    private WeaponManager weaponManager;
    private bool isAlive = false;

    private Collider entityCollider;

    public double GetCurrentHealth() {return currentHealth;  }
    public void SetCurrentHealth(double currentHealth) {this.currentHealth=currentHealth;}
    public void SetIsAlive(bool value) { this.isAlive=value;}
    public void Start()
    {
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        entityCollider = GetComponent<CapsuleCollider>();
        weaponManager = GetComponentInChildren<WeaponManager>();

        if (entityCollider == null && !transform.CompareTag("Player"))
        {
            Debug.LogWarning("No collider found on entity: " + gameObject.name);
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        

        if (transform.CompareTag("Enemy"))
        {
            maxHealth = maxHealth * FindObjectOfType<DifficultyManager>().GetEnemyDamageMultiplier();
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        if (currentHealth <= 0)
        {
            if (transform.CompareTag("Enemy"))
            {
                if (hasDeathAnimation)
                {
                    animator.SetBool("death", true);
                    transform.GetComponent<Loot>().enabled = true;
                }
                else
                {
                    animator.enabled = false;
                    transform.GetComponentInChildren<Loot>().enabled = true;
                }

                transform.tag = "DeadEnemy";
                transform.GetComponent<NavMeshAgent>().enabled = false;
                transform.GetComponent<AIController>().enabled = false;
                transform.GetComponent<Patrolling>().enabled = false;
                transform.GetComponent<Patrolling>().enabled = false;
                transform.GetComponent<AnimationEventHandler>().enabled = false;
                transform.GetComponent<EffectStatus>().enabled = false;
                
                StartCoroutine(DestroyObjectAfterTime());

                if (entityCollider != null)
                {
                    //entityCollider.enabled = false;
                    entityCollider.isTrigger = true;
                    entityCollider.providesContacts = false;
                    Debug.Log("Entity collider disabled: " + entityCollider.name);
                }

                // Disable the weapon collider for the same reason we disabled the boss collider 
                if (weaponManager != null)
                {
                    Collider weaponCollider = weaponManager.GetComponent<Collider>();
                    if (weaponCollider != null)
                    {
                        weaponCollider.enabled = false;
                        Debug.Log("Weapon collider disabled: " + weaponCollider.name);
                    }
                    else
                    {
                        Debug.LogWarning("No collider found on weapon: " + weaponManager.name);
                    }
                }
            }
            else if (transform.CompareTag("Player"))
            {
                if (!isAlive)
                {
                    isAlive = !isAlive;
                    FindAnyObjectByType<DeathScreenFade>().TriggerDeathFade();
                }
            }

            
        }
    }

    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Heal(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            healthBar.SetHealth(maxHealth);
            currentHealth = maxHealth;
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private IEnumerator DestroyObjectAfterTime()
    {
        yield return new WaitForSeconds(5);
        transform.gameObject.isStatic = true;
        //transform.GetComponent<Rigidbody>().useGravity = false;
    }
}
