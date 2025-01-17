using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public Collider detectionCollider;
    public Collider damageCollider;
    
    private bool isActived = false;
    private bool dealtInstaDmg = false;

    public List<BuffDebuff> listOfEffects = new List<BuffDebuff>();

    [Header("Trap settings")]
    public bool singleUse = false;
    public bool enemyCanActivate = true;
    public float damage = 0;
    public bool dmgPerTick = false;
    [Header("Trap Animation")]
    public Animator animator;

    protected bool turnOffTrap = false;

    private void Start()
    {
        detectionCollider.isTrigger = true;
        damageCollider.isTrigger = false;
        detectionCollider.enabled = true;
        damageCollider.enabled = false;
        OffParticles();
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

    protected bool GetDealtInstaDmg() { 
        return dealtInstaDmg; 
    }
    protected void SetDealtInstaDmg(bool isDealtInstaDmg)
    {
        this.dealtInstaDmg = isDealtInstaDmg;
    }

    protected bool GetIsActived() 
    { 
        return this.isActived;
    }

    /// <summary>
    /// Only is it has timer
    /// Will end trap, ONLY for particles trap
    /// </summary>
    /// <param name="timeTillEnd"></param>
    /// <returns></returns>
    protected IEnumerator SetTimerToEndTrap(float timeTillEnd)
    {
        Debug.Log("Nastavil sa timer " + timeTillEnd);
        yield return new WaitForSeconds(timeTillEnd);
        turnOffTrap = true;
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
            RunningTrap(other);
        }
    }

    /// <summary>
    /// Triggered by leaving DamageAreaCollider, stops trap (if not on timer)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (isActived)
        {
            if (other.tag.Equals("Player") || (other.tag.Equals("Enemy") && enemyCanActivate))
            {
                StopTrap();
            }
        }
        
    }

    /// <summary>
    /// Apply all effects that are available on friendly/enemy entity
    /// </summary>
    /// <param name="other"></param>
    protected void ApplyEffect(Collider other)
    {
        foreach (BuffDebuff effect in listOfEffects)
        {
            other.GetComponent<EffectStatus>().InsertEffect(effect);
        }
    }

    protected void TurnOffTrap(Collider other)
    {
        detectionCollider.isTrigger = false;
    }

    public abstract void StartTrap();
    public abstract void RunningTrap(Collider other);
    public abstract void StopTrap();
    public virtual void OffParticles() { }

}