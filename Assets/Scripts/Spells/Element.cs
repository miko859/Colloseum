using UnityEngine;
using System.Collections;

public abstract class Element : Spell
{
    protected bool isElementActive = false;
    private Coroutine manaSpendingCoroutine;
    public int damagePerSecond; 
    public float damageInterval = 0.5f; 
    public float range = 5f; // How far does the dmg travel
    private Coroutine damageCoroutine;
    public ParticleSystem particleSystem;

    public override void Activate()
    {
        base.Activate();
        if (!isElementActive)
        {
            isElementActive = true;
            particleSystem.Play();
            StartManaSpending();
            damageCoroutine = StartCoroutine(DealDamageOverTime());
        }
        
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (isElementActive)
        {
            isElementActive = false;
            particleSystem.Stop();
            StopManaSpending();
            base.Deactivate();
            if (damageCoroutine != null) StopCoroutine(damageCoroutine);
        }
       
    }

    public override void StartManaSpending()
    {
        if (manaSpendingCoroutine == null)
        {
            manaSpendingCoroutine = StartCoroutine(SpendManaContinuously());
        }
    }

    private void StopManaSpending()
    {
        if (manaSpendingCoroutine != null)
        {
            StopCoroutine(manaSpendingCoroutine);
            manaSpendingCoroutine = null;
        }
    }

    private IEnumerator SpendManaContinuously()
    {
        while (isElementActive) 
        {
            if (!manaSystem.TrySpendMana(manaCost))
            {
                Debug.Log("Not enough mana!");
                Deactivate();
                yield break; 
            }

            yield return new WaitForSeconds(0.5f); 
        }
    }

    protected virtual IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            // Detetcs enemies in range and deal damage
            Collider[] hits = Physics.OverlapSphere(transform.position, range);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Health enemyHealth = hit.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.DealDamage(damagePerSecond);
                        Debug.Log("Element dealt damage to: " + hit.name);
                    }
                }
            }

            yield return new WaitForSeconds(damageInterval); 
        }
    }
}
