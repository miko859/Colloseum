using Unity.VisualScripting;
using UnityEngine;

public class Burning : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.BurningConfig);
        health = entity.GetComponent<Health>();
    }

    public override void Functionality()
    {
        health.DealDamage(data.DamagePerTick);
    }
}