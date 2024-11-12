using UnityEngine;

public class ParticleTrap : Trap
{
    private float elapsedTime = 0;
    [Header("Particles to activate")]
    public ParticleSystem[] particleObjects;
    [SerializeField] public bool timer = false;
    //[ShowIf("timer", true, false)][SerializeField]
    float timeTillEnd = 0f;
    public override void StartTrap()
    {
        SetDetectionCollidor(false, false);
        SetDamageCollidor(true, true);

        SetIsActived(true);
        if (particleObjects != null)
        {
            foreach (var particle in particleObjects)
            {
                particle.Play();
            }
        }

        if (animator != null)
        {
            animator.Play("trap", 0, 0f);
        }

        if (timer)
        {
            StartCoroutine(SetTimerToEndTrap(timeTillEnd));
        }

    }

    public override void RunningTrap(Collider other)
    {
        if (dmgPerTick)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1)
            {
                elapsedTime = 0;
                other.GetComponent<Health>().DealDamage(damage);
                /*
                 *  can also apply debuf, debuf will be in list, 
                 *  that will be applied on entity
                 *  will need create new class Buf, Debuf
                 */
                if (listOfEffects.Count > 0)
                {
                    ApplyEffect(other);
                }
            }
        }
        else
        {
            if (!GetDealtInstaDmg())
            {
                SetDealtInstaDmg(true);
                other.GetComponent<Health>().DealDamage(damage);
                /*
                 *  can also apply debuf, debuf will be in list, 
                 *  that will be applied on entity
                 *  will need create new class Buf, Debuf
                 */
                if (listOfEffects.Count > 0)
                {
                    ApplyEffect(other);
                }
            }

        }

    }
    public override void StopTrap()
    {
        if (!timer) 
        {
            SetDamageCollidor(false, false);

            if (!singleUse)
            {
                SetDetectionCollidor(true, true);
            }

            foreach (var particle in particleObjects)
            {
                particle.Stop();
            }

            SetIsActived(false);
            SetDealtInstaDmg(false);
        }
    }
}