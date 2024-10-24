using UnityEngine;

public class ParticleTrap : Trap
{
    [Header("Particles to activate")]
    public ParticleSystem[] particleObjects;
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
        
    }

    private int frame = 0;
    public override void RunningTrap(Collider other)
    {
        if (dmgPerTick)
        {
            frame++;
            if (frame == 60)
            {
                frame = 0;
                other.GetComponent<Health>().DealDamage(damage);
                /*
                 *  can also apply debuf, debuf will be in list, 
                 *  that will be applied on entity
                 *  will need create new class Buf, Debuf
                 */
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
            }

        }

    }
    public override void StopTrap()
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