using UnityEngine;

public abstract class Ball : Spell
{
    public float speed = 10f;
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
        
        Vector3 spawnPosition = playerController.transform.position + playerController.transform.forward * 1.5f;

        Rigidbody fireballInstance = Instantiate(spellRb, spawnPosition, Quaternion.LookRotation(playerController.transform.forward));

        fireballInstance.velocity = playerController.transform.forward * speed;
    }
}
