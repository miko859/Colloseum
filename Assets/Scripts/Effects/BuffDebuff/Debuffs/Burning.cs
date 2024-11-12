using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Burning : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.BurningConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        gameObject.transform.GetComponent<Health>().DealDamage(data.DamagePerTick * stacks);
    }

    public override void ReverseEffect()
    {
        
    }
}