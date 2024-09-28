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
                StartCoroutine(PlayAnimation(weaponData.lightAttackAnimation + Random.Range(1, 4), weaponData.lightAttackDuration));
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
            }
            else
            {
                StartCoroutine(PlayAnimation(weaponData.heavyAttackPerformAnimation, weaponData.heavyAttackPerformDuration));   
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
    }

    /// <summary>
    /// Bash, player will bash enemy, which will stun enemy for little time
    /// </summary>
    public override void Bash()
    {
        if (isBlocking)
        {
            StartCoroutine(PlayAnimation(weaponData.bashAnimation, weaponData.bashDuration));
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

    public override void Equip()
    {
        gameObject.SetActive(true);
    }

    public override void Unequip()
    {
        gameObject.SetActive(false);
    }
}