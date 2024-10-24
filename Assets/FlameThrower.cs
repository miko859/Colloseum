using System.Collections;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public ParticleSystem flameEffect;  
    public int damagePerSecond = 5;      
    public float damageInterval = 0.5f;  // Damage tick
    public float range = 10f;            // Range 
    public float swaySpeed = 5f;         
    private bool isFlameActive = false;
    private Quaternion targetRotation;
    private Coroutine manaCoroutine;
    public ManaSystem manaSystem;

    private void Start()
    {
        flameEffect.Stop();
    }
    void Update()
    {


        if (Input.GetKey(KeyCode.F))  // Use GetKey for continuous input detection
        {
            if (manaSystem.currentMana >= manaSystem.flamethrowerCost)
            {
                StartFlameThrower();
            }
            else
            {
                StopFlameThrower();  // Stop the flamethrower if there's no mana
            }
        }

        if (Input.GetKeyUp(KeyCode.F))  // Detect when the F key is released
        {
            StopFlameThrower();
        }
    }

    void StartFlameThrower()
    {
        isFlameActive = true;
        flameEffect.Play();
        StartCoroutine(DealDamageOverTime());
        manaCoroutine = StartCoroutine(manaSystem.SpendManaPerSecond());
        Debug.Log("Flamethrower started, mana spending coroutine started.");

    }

    public void StopFlameThrower()
    {
        isFlameActive = false;
        flameEffect.Stop();
        if (manaCoroutine != null)
        {
            StopCoroutine(manaCoroutine);
            manaCoroutine = null;  
        }

    }


    IEnumerator DealDamageOverTime()
    {
        while (isFlameActive)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.forward, range);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Flamethrower hit enemy: " + hit.collider.name);
                    hit.collider.GetComponent<Health>()?.DealDamage(damagePerSecond);
                }
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
