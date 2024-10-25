using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Burning : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.BurningConfig);
        //health = entity.GetComponent<Health>();
    }

    public override void Functionality()
    {
        gameObject.GetComponent<Health>().DealDamage(data.DamagePerTick);
    }
}