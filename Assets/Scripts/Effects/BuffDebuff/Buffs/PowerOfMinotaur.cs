using UnityEngine;

public class PowerOfMinotaur : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.PowerOfMinotaurConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.GetComponentInChildren<WeaponAnimations>().ChangeBuffDebuffDmg(data.DamageChangedBy * stacks);
        }
        else
        {
            gameObject.transform.GetComponent<AIController>().ChangeBuffDebuffDmgBy(data.DamageChangedBy * stacks);
        }
    }

    public override void ReverseEffect()
    {
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.GetComponentInChildren<WeaponAnimations>().ChangeBuffDebuffDmg((data.DamageChangedBy * stacks * (-1)));
        }
        else
        {
            gameObject.transform.GetComponent<AIController>().ChangeBuffDebuffDmgBy(data.DamageChangedBy * stacks * (-1));
        }
    }
}