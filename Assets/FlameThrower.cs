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
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartFlameThrower();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            StopFlameThrower();
        }
    }

    void StartFlameThrower()
    {
        isFlameActive = true;
        flameEffect.Play();
        StartCoroutine(DealDamageOverTime());
    }

    void StopFlameThrower()
    {
        isFlameActive = false;
        flameEffect.Stop();
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
