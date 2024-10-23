

using System.Collections;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public Collider detectionCollider;
    public Collider damageCollider;
    
    private bool isActived = false;

    [Header("Trap settings")]
    public bool singleUse = false;
    public bool enemyCanActivate = true;
    [SerializeField] public bool timer = false;
    [ShowIf("timer", true, false)][SerializeField] float timeTillEnd = 0f;

    private void Start()
    {
        detectionCollider.isTrigger = true;
        damageCollider.isTrigger = false;
        detectionCollider.enabled = true;
        damageCollider.enabled = false;
    }

    /// <summary>
    /// Setting Collider for detection
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="enabled"></param>
    public void SetDetectionCollidor(bool trigger, bool enabled)
    {
        detectionCollider.isTrigger = trigger;
        detectionCollider.enabled = enabled;
    }

    /// <summary>
    /// Setting Collider for damage area
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="enabled"></param>
    public void SetDamageCollidor(bool trigger, bool enabled)
    {
        damageCollider.isTrigger = trigger;
        damageCollider.enabled = enabled;
    }

    /// <summary>
    /// Sets if trap is activated and is dealing damage to entity
    /// </summary>
    /// <param name="isActived"></param>
    protected void SetIsActived(bool isActived)
    {
        this.isActived = isActived;
    }

    protected bool GetIsActived() 
    { 
        return this.isActived;
    }

    protected IEnumerator SetTimerToEndTrap()
    {
        yield return new WaitForSecondsRealtime(timeTillEnd);
        StopTrap();
    }

    /// <summary>
    /// Triggered by detection collider, wake up trap
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (!isActived)
        {
            if (other.tag.Equals("Player") || (other.tag.Equals("Enemy") && enemyCanActivate))
            {
                StartTrap();

            }

            if (timer)
            {
                StartCoroutine(SetTimerToEndTrap());
            }
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") || (other.tag.Equals("Enemy") && enemyCanActivate))
        {
            RunningTrap();
        }
    }

    /// <summary>
    /// Triggered by leaving DamageAreaCollider, stops trap (if not on timer)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (isActived && !timer)
        {
            if (other.tag.Equals("Player") || (other.tag.Equals("Enemy") && enemyCanActivate))
            {
                StopTrap();
            }
        }
        
    }

    protected void TurnOffTrap()
    {
        detectionCollider.isTrigger = false;
    }

    public abstract void StartTrap();
    public abstract void RunningTrap();
    public abstract void StopTrap();

}