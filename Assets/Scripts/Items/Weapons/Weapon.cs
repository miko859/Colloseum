using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    public WeaponData weaponData;

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

}