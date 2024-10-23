

using UnityEngine;

public class AniTrap : Trap
{
    [Header("Trap Animation")]
    public Animator animator;
    

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

    private int frame = 0;
    private int count = 0;
    public override void RunningTrap(Collider other)
    {
        if (dmgPerTick)
        {
            frame++;
            if (frame == 60)
            {
                count++;
                frame = 0;
                Debug.Log($"Frame: {count}");
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
            other.GetComponent<Health>().DealDamage(damage);
            /*
             *  can also apply debuf, debuf will be in list, 
             *  that will be applied on entity
             *  will need create new class Buf, Debuf
             */
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
    }
}