

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

        foreach (var particle in particleObjects)
        {
            particle.Play();
        }
        

        
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

        foreach (var particle in particleObjects)
        {
            particle.Stop();
        }

        SetIsActived(false);
    }
}