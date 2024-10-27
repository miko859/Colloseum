using UnityEngine;

/// <summary>
/// Abstract base class for all ball type spells.
/// </summary>
public abstract class Ball : Spell
{
    public float speed = 10f; // Default speed for all balls ;)
    public Rigidbody rb;
    public int impactDamage = 3;

    protected virtual void Start() 
    {
        
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    public override void Activate()
    {
        base.Activate();
        rb.velocity = transform.forward * speed; 
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.DealDamage(impactDamage);
            }
            Destroy(gameObject);
        }
    }
}
