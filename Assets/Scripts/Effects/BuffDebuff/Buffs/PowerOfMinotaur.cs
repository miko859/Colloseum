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
            gameObject.transform.GetComponentInChildren<WeaponAnimations>().ChangeBuffDebuffDmg(data.DamageChangedBy);
        }
        else
        {
            gameObject.transform.GetComponent<AIController>().ChangeBuffDebuffDmgBy(data.DamageChangedBy);
        }
    }

    public override void ReverseEffect()
    {
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.GetComponentInChildren<WeaponAnimations>().ChangeBuffDebuffDmg((data.DamageChangedBy * (-1)));
        }
        else
        {
            gameObject.transform.GetComponent<AIController>().ChangeBuffDebuffDmgBy(data.DamageChangedBy * (-1));
        }
    }
}