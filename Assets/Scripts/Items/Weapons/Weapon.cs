using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    public Animator bodyAnimator;
    public WeaponData weaponData;
    public AnimatorOverrideController overrideController;

    protected bool isAttacking = false;
    protected bool isBashing = false;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void Attack();
    public abstract void HardAttack(bool attackPhase);
    public abstract void Block();
    public abstract void Bash();
    public abstract void HandleInput(PlayerInputActions playerInputActions);
    public abstract void Equip();
    public abstract void Unequip();

    protected IEnumerator PlayAnimation(string animationName, float duration)
    {
        animator.SetBool(animationName, true);
        yield return new WaitForSeconds(duration);
        animator.SetBool(animationName, false);
    }

    protected IEnumerator PlayBodyAnimation(string animationName, float duration)
    {
        bodyAnimator.SetBool(animationName, true);
        yield return new WaitForSeconds(duration);
        bodyAnimator.SetBool(animationName, false);
        isAttacking = false;
    }

    public void setNewAnimationsForBody()
    {
        bodyAnimator.runtimeAnimatorController = overrideController;
    }

    public void setBodyAnimator(Animator bodyAnimator) { 
        this.bodyAnimator = bodyAnimator; 
    }

    public void SetIsBashing(bool isBashing)
    {
        this.isBashing = isBashing;
    }

    public Animator GetAnimator() { return animator; }

    public Animator GetBodyAnimator() { return bodyAnimator; }
}