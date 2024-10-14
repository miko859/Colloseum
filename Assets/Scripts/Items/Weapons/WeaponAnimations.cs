using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAnimations : Weapon
{
    private bool isBashing = false;
    private bool isBlocking = false;
    private bool isAttacking = false;
    private int damage;
    

    public int getDamage()
    {
        return damage; 
    }

    public bool getIsBashing()
    {
        return isBashing;
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
                damage = weaponData.lightAttackDamage;
                var attackType = Random.Range(1, weaponData.lightAttackTypesCount);
                StartCoroutine(PlayAnimation(weaponData.lightAttackAnimation + attackType, (attackType == 1) ? weaponData.lightAttackDuration1 : ((attackType == 2) ? weaponData.lightAttackDuration2 : weaponData.lightAttackDuration3)));
                StartCoroutine(PlayBodyAnimation(weaponData.lightAttackAnimation + attackType, (attackType == 1) ? weaponData.lightAttackDuration1 : ((attackType == 2) ? weaponData.lightAttackDuration2 : weaponData.lightAttackDuration3)));
                isAttacking = false;
            }
        }
    }

    /// <summary>
    /// Hard Attack/Strong Attack
    /// </summary>
    /// <param name="attackPhase"></param>
    public override void HardAttack(bool attackPhase)
    {
        if (!isBlocking)
        {
            if (attackPhase)
            {
                damage = weaponData.heavyAttackDamage;
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
        bodyAnimator.SetBool("Block", isBlocking);
    }

    /// <summary>
    /// Bash, player will bash enemy, which will stun enemy for little time
    /// </summary>
    public override void Bash()
    {
        if (isBlocking)
        {
            StartCoroutine(PlayAnimation(weaponData.bashAnimation, weaponData.bashDuration));
            StartCoroutine(PlayBodyAnimation(weaponData.bashAnimation, weaponData.bashDuration));
        }
    }

    /// <summary>
    /// Function to handle inputs from InputSystem Map
    /// </summary>
    /// <param name="playerInputActions"></param>
    public override void HandleInput(PlayerInputActions playerInputActions)
    {
        if (playerInputActions.Player.Attack.triggered)
        {
            Attack();
        }
        if (playerInputActions.Player.Attack.triggered)
        {
            HardAttack(true || false);
        }
        if (playerInputActions.Player.Block.triggered)
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