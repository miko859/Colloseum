

using UnityEngine;

public class AniTrap : Trap
{
    [Header("Trap Animation")]
    public Animator animator;
    

    public override void StartTrap()
    {
        SetIsActived(true);

        if (singleUse)
        {
            TurnOffTrap();
        }

        //animator.Play("trap", 0, 0f);

        SetDetectionCollidor(false, false);
        SetDamageCollidor(true, true);
    }

    public override void RunningTrap()
    {
        
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