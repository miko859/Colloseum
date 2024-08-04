using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeAnimation : Weapon
{

    public Collider Blade;

    private bool isBashing = false;
    private bool isBlocking = false;
    private bool isAttacking = false;
    public GameObject myPrefab;

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
            StartCoroutine(AttackAnim());
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
                StartCoroutine(HardAttackAnim());
            }
            else 
            {
                StartCoroutine(HardAttackAnimPerform());
            }
        }
    }

    /// <summary>
    /// Block, player will partly or fully block incoming damage
    /// </summary>
    public override void Block()
    {
        isBlocking = !isBlocking;
        animator.SetBool("AxeBlock", isBlocking);
    }

    /// <summary>
    /// Bash, player will bash enemy, which will stun enemy for little time
    /// </summary>
    public override void Bash()
    {
        if(isBlocking)
        {
            StartCoroutine(BashAnim());
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
    
    /// <summary>
    /// Async LightAttack animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackAnim()
    {
         if (!isAttacking) 
         {
          isAttacking = true;
         Blade.isTrigger = true;
         animator.SetBool("AxeAnim", true);
         yield return new WaitForSeconds(0.46f);
         Blade.isTrigger = false;
         animator.SetBool("AxeAnim", false);
         yield return new WaitForSeconds(0.46f);
         isAttacking = false; 
         }
        
       
    }
    
    /// <summary>
    /// Async HardAttack charge
    /// </summary>
    /// <returns></returns>
    private IEnumerator HardAttackAnim()
    {
        animator.SetBool("AxeHardAttack", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("AxeHardAttack", false);
    }
    
    /// <summary>
    /// Async HardAttack perform
    /// </summary>
    /// <returns></returns>
    private IEnumerator HardAttackAnimPerform()
    {
        Blade.isTrigger = true;
        animator.SetBool("AxeHardAttackPerform", true);
        yield return new WaitForSeconds(0.46f);
        Blade.isTrigger= false;
        animator.SetBool("AxeHardAttackPerform", false);   
    }

    /// <summary>
    /// Async Bash animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator BashAnim()
    {
        animator.SetBool("Bash", true);
        yield return new WaitForSeconds(0.48f);
        animator.SetBool("Bash", false);
    }
}
