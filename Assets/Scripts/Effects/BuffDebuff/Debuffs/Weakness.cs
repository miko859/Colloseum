using UnityEngine.AI;
using UnityEngine;

public class Weakness : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.WeaknessConfig);
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
            gameObject.transform.GetComponentInChildren<WeaponAnimations>().ChangeBuffDebuffDmg( (data.DamageChangedBy * (-1)) );
        }
        else
        {
            gameObject.transform.GetComponent<AIController>().ChangeBuffDebuffDmgBy(data.DamageChangedBy * (-1));
        }
    }
}