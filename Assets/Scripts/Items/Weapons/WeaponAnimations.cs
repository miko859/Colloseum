using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAnimations : Weapon
{
    private bool isBlocking = false;
    
    private float damage;
    private float buffDebuffDmg = 0;
    

    public void ChangeBuffDebuffDmg(float value)
    {
        buffDebuffDmg += value;
    }

    public float getDamage()
    {
        return damage; 
    }

    public bool getIsBashing()
    {
        return isBashing;
    }

    public bool getIsBlocking()
    {
        return isBlocking;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Basic Attacl/Light Attack
    /// </summary>
    public override void Attack()
    {
        
        if (!isBlocking)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                damage = weaponData.lightAttackDamage + buffDebuffDmg;
                var attackType = Random.Range(1, weaponData.lightAttackTypesCount+1);
                StartCoroutine(PlayAnimation(weaponData.lightAttackAnimation + attackType, (attackType == 1) ? weaponData.lightAttackDuration1 : ((attackType == 2) ? weaponData.lightAttackDuration2 : weaponData.lightAttackDuration3)));
                StartCoroutine(PlayBodyAnimation(weaponData.lightAttackAnimation + attackType, (attackType == 1) ? weaponData.lightAttackDuration1 : ((attackType == 2) ? weaponData.lightAttackDuration2 : weaponData.lightAttackDuration3)));
            }
        }
    }

    /// <summary>
    /// HARD Attack/Strong Attack
    /// </summary>
    /// <param name="attackPhase"></param>
    public override void HardAttack(bool attackPhase)
    {
        if (!isBlocking)
        {
            if (attackPhase)
            {
                damage = weaponData.heavyAttackDamage + buffDebuffDmg;
                StartCoroutine(PlayAnimation(weaponData.heavyAttackAnimation, weaponData.heavyAttackChargeDuration));
                StartCoroutine(PlayBodyAnimation(weaponData.heavyAttackAnimation, weaponData.heavyAttackChargeDuration));
            }
            else
            {
                StartCoroutine(PlayAnimation(weaponData.heavyAttackPerformAnimation, weaponData.heavyAttackPerformDuration));   
                StartCoroutine(PlayBodyAnimation(weaponData.heavyAttackPerformAnimation, weaponData.heavyAttackPerformDuration));
            }

        }
    }

    /// <summary>
    /// Block, player will partly or fully block incoming damage
    /// </summary>
    public override void Block()
    {
        isBlocking = !isBlocking;
        animator.SetBool("Block", isBlocking);
        GetBodyAnimator().SetBool("Block", isBlocking);
    }

    /// <summary>
    /// Bash, player will bash enemy, which will stun enemy for little time
    /// </summary>
    public override void Bash()
    {
        if (isBlocking)
        {
            StartCoroutine(GetComponent<WeaponManager>().SwapCollBlockBash());
            StartCoroutine(PlayAnimation(weaponData.bashAnimation, weaponData.bashDuration));
            StartCoroutine(PlayBodyAnimation(weaponData.bashAnimation, weaponData.bashDuration));
        }
    }

    /// <summary>
    /// Function to handle inputs from InputSystem Map
    /// </summary>
    /// <param name="playerInputActions"></param>
    public override void HandleInput(PlayerInput playerInput)
    {
        if (playerInput.actions["Attack"].triggered)
        {
            Attack();
        }
        if (playerInput.actions["Attack"].triggered)
        {
            HardAttack(true || false);
        }
        if (playerInput.actions["Block"].triggered)
        {
            Block();
        }
    }

    public void GotHit()
    {
        StartCoroutine(PlayAnimation(weaponData.hurtAnimation, weaponData.hurtDuration));
        StartCoroutine(PlayBodyAnimation(weaponData.hurtAnimation, weaponData.hurtDuration));
    }

    public override void Equip()
    {
        gameObject.SetActive(true);
    }

    public override void Unequip()
    {
        gameObject.SetActive(false);
    }
}