

using System.Threading;
using UnityEngine;

public class ParticleTrap : Trap
{

    [Header("Trap Animation")]
    public Animator animator;
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

        foreach (var particle in particleObjects)
        {
            particle.Stop();
        }

        SetIsActived(false);
    }
}