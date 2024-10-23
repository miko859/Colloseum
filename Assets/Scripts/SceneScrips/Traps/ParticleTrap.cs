

using UnityEngine;

public class ParticleTrap : Trap
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