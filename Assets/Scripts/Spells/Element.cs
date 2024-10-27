using UnityEngine;
using System.Collections;

public abstract class Element : Spell
{
    public ParticleSystem effect;
    public int damagePerSecond; 
    public float damageInterval = 0.5f; 
    public float range = 5f; // How far does the dmg travel
    private Coroutine damageCoroutine;

    public override void Activate()
    {
        base.Activate();
        if (effect != null) effect.Play();
        damageCoroutine = StartCoroutine(DealDamageOverTime());
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (effect != null) effect.Stop();
        if (damageCoroutine != null) StopCoroutine(damageCoroutine);
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
