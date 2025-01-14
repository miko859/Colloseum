using UnityEngine;

public class Silence : BuffDebuff
{
    public override void CreateObject(GameObject entity) /* REWORK NEEDED */ 
    {
        Initialize(BuffDebuffData.SilenceConfig);
        gameObject = entity;
    }

    public override void Functionality()
    {
        gameObject.transform.GetComponentInChildren<PlayerController>().ChangeSilence();
    }

    public override void ReverseEffect()
    {
        gameObject.transform.GetComponentInChildren<PlayerController>().ChangeSilence();
    }
}