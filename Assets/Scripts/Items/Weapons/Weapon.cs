using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    protected Animator animator;
    

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void LightAttack();
    //public abstract void HardAttack();
    public abstract void Block();
    //public abstract void Bash();
    public abstract void HandleInput(PlayerInputActions playerInputActions);
    public abstract void Equip();
    public abstract void Unequip();

}