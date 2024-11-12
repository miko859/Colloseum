using UnityEngine;

public class AniTrap : Trap
{
    public override void StartTrap()
    {
        SetDetectionCollidor(false, false);
        SetDamageCollidor(true, true);

        SetIsActived(true);

        if (animator != null)
        {
            animator.Play("trap", 0, 0f);
        }
    }

    private float elapsedTime = 0;
    public override void RunningTrap(Collider other)
    {
        if (dmgPerTick)
        {
            elapsedTime = Time.deltaTime;

            if (elapsedTime >= 1)
            {
                elapsedTime = 0;;
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
        SetDamageCollidor(false, false);
        
        if (!singleUse)
        {
            SetDetectionCollidor(true, true);
        }

        SetIsActived(false);
        SetDealtInstaDmg(false);
    }
}