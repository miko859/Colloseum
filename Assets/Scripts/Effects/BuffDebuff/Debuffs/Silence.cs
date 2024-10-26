using UnityEngine;

public class Silence : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.SilenceConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        gameObject.transform.GetComponent<Health>().DealDamage(data.DamagePerTick * stacks);
    }

    public override void ReverseEffect()
    {
        throw new System.NotImplementedException();
    }
}