using UnityEngine;
using UnityEngine.AI;

public class Freezing : BuffDebuff
{
    public override void CreateObject(GameObject entity)
    {
        Initialize(BuffDebuffData.FreezingConfig);
        gameObject = entity;
    }

    public override void Functionality()
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

    public override void ReverseEffect()
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
}