using UnityEngine;

public class BlessingOfHealing : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.BlessingOfHealingConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        gameObject.transform.GetComponent<Health>().Heal(data.HealPerTick * stacks);
    }

    public override void ReverseEffect()
    {
        throw new System.NotImplementedException();
    }
}