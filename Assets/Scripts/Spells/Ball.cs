using UnityEngine;

/// <summary>
/// Abstract base class for all ball type spells.
/// </summary>
public abstract class Ball : Spell
{
    public float speed = 10f; // Default speed for all balls ;)
    public Rigidbody spellRb;
    public int impactDamage = 3;
    public PlayerController playerController;
    protected virtual void Start() 
    {
        
        if (spellRb == null) spellRb = GetComponent<Rigidbody>();
    }

    public override void ActiveBall()
    {
        CastFireball();
        base.Activate();
        spellRb.velocity = transform.forward * speed; 
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
            
        }
            Destroy(gameObject);
    }
    private void CastFireball()
    {
        Rigidbody fireballInstance = Instantiate(spellRb, playerController.transform.localPosition + transform.forward, Quaternion.LookRotation(transform.forward));
        fireballInstance.velocity = transform.forward * speed; 
    }
}


