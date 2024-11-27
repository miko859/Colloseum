using UnityEngine;

public abstract class Ball : Spell
{
    public float speed = 10f;
    public Rigidbody spellRb;
    public int impactDamage = 3;
    public PlayerController playerController;

    protected void Start()
    {
        if (spellRb == null)
            spellRb = GetComponent<Rigidbody>();

        
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene! Please attach the script to the appropriate GameObject.");
        }
    }

    public override void ActiveBall()
    {
        if (manaSystem != null && manaSystem.TrySpendMana(manaCost))
        {
            CastFireball();
        }
        else
        {
            Debug.Log("Not enough mana to cast fireball!");
            StartCoroutine(manaSystem.VibrateManaBar(() => Debug.Log("Vibration Complete")));
        }
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
        if (playerController == null)
        {
            Debug.LogError("PlayerController is null. Cannot cast fireball!");
            return;
        }

        Vector3 spawnPosition = playerController.transform.position + playerController.transform.forward * 1.5f;

        Debug.Log($"Casting fireball at {spawnPosition}");
        Rigidbody fireballInstance = Instantiate(spellRb, spawnPosition, Quaternion.LookRotation(playerController.transform.forward));

        fireballInstance.velocity = playerController.transform.forward * speed;

        Debug.Log("Fireball cast successfully!");
    }
}
