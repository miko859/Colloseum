using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    

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

}