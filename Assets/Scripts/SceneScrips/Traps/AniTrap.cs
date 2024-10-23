

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
    public override void RunningTrap()
    {
        Debug.Log("RunningTrap");
        frame++;
        if (frame == 60)
        {
            count++;
            frame = 0;
            Debug.Log($"Frame: {count}");
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