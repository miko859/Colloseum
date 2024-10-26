using UnityEngine.AI;
using UnityEngine;

public class Haste : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.HasteConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.GetComponentInChildren<PlayerMovement>().base_move_speed += data.MovementSpeedChangedBy;
        }
        else
        {
            gameObject.transform.GetComponent<NavMeshAgent>().speed += data.MovementSpeedChangedBy;
        }
    }

    public override void ReverseEffect()
    {
        if (gameObject.tag.Equals("Player"))
        {
            gameObject.transform.GetComponentInChildren<PlayerMovement>().base_move_speed -= data.MovementSpeedChangedBy;
        }
        else
        {
            gameObject.transform.GetComponent<NavMeshAgent>().speed -= data.MovementSpeedChangedBy;
        }
    }
}