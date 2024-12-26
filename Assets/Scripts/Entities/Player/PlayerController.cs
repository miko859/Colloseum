using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    public Weapon currentWeapon;
    private EquipedWeaponManager equipedWeaponManager;
    public SpellManager spellManager;

    public Spell spell;

    public Animator animator;
    public StaminaBar staminaBar;

    private bool isCharging = true;
    private bool isWalking = false;
    private bool isBlocking = false;
    private bool isRunning = false;

    [Header("Testujem")]
    private AnimatorStateInfo stateInfo;
    private Animator oldAnimator;
    private AnimatorClipInfo[] clipInfo;
    private AnimatorControllerParameter[] parametre;
    private Animator bodyAni;

    private void Start()
    {
        equipedWeaponManager = GetComponent<EquipedWeaponManager>();
        staminaBar.SetMaxStamina(100, 2.5);
        playerInput = GetComponent<PlayerInput>();
        EquipedWeaponManager.Instance.AddWeapon(currentWeapon);
    }

    private void OnEnable()
    {
        playerInput.currentActionMap.Enable();
    }

    private void OnDisable()
    {
        playerInput.currentActionMap.Disable();
    }

    private bool isBashing = false;

    private void Update()
    {
        if (playerInput.actions["Attack"].IsPressed() && playerInput.actions["Block"].IsInProgress())
        { 
            if (staminaBar.GetCurrentStamina() > currentWeapon.weaponData.bashStaminaCons && !isBashing)
            {
                isBashing = !isBashing;
                StartCoroutine(BashCooldown());
                currentWeapon.Bash();
                staminaBar.ReduceStamina(currentWeapon.weaponData.bashStaminaCons);
                currentWeapon.SetIsBashing(true);
            }
        }
    }

    IEnumerator BashCooldown()
    {
        yield return new WaitForSeconds(currentWeapon.weaponData.bashDuration);
        isBashing = !isBashing;
    }

    /// <summary>
    /// Swap weapon
    /// </summary>
    /// <param name="newWeapon"></param>

    public IEnumerator EquipWeapon(Weapon newWeapon)
    {
        // options of old Animator on Weapon
        if (currentWeapon != null)
        {
            currentWeapon.GetBodyAnimator().Play("unequip", 0, 0f);
            currentWeapon.GetAnimator().Play("unequip", 0, 0f);

            yield return new WaitForSeconds(currentWeapon.weaponData.equipUnequipDuration);

            currentWeapon.Unequip();
        }

        // change old for new weapon
        currentWeapon = newWeapon;

        // override new Weapon Animator
        currentWeapon.setBodyAnimator(animator);
        currentWeapon.setNewAnimationsForBody();

        currentWeapon.Equip();

        currentWeapon.GetBodyAnimator().Play("equip", 0, 0f);
        currentWeapon.GetAnimator().Play("equip", 0, 0f);
        yield return new WaitForSeconds(currentWeapon.weaponData.equipUnequipDuration);

        if (currentWeapon != null)
        {
            // sync parameters
            foreach (AnimatorControllerParameter parameter in currentWeapon.bodyAnimator.parameters)
            {
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        currentWeapon.GetAnimator().SetFloat(parameter.name, currentWeapon.bodyAnimator.GetFloat(parameter.name));
                        break;
                    case AnimatorControllerParameterType.Int:
                        currentWeapon.GetAnimator().SetInteger(parameter.name, currentWeapon.bodyAnimator.GetInteger(parameter.name));
                        break;
                    case AnimatorControllerParameterType.Bool:
                        currentWeapon.GetAnimator().SetBool(parameter.name, currentWeapon.bodyAnimator.GetBool(parameter.name));
                        break;
                }
            }
        }
    }

    public StaminaBar GetStaminaBar()
    {
        return staminaBar;
    }

    public bool GetIsBashing()
    {
        return isBashing;
    }

    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            if (context.ReadValue<Vector2>().y < 0)
            {
                int yValueDown = equipedWeaponManager.getCurrentWeaponIndex() - 1;

                if (yValueDown < 0)
                {
                    yValueDown = equipedWeaponManager.weaponery.Count;
                }

                equipedWeaponManager.SwitchWeapon(yValueDown);
            }
            else
            {
                int yValueUp = equipedWeaponManager.getCurrentWeaponIndex() + 1;

                if (yValueUp > equipedWeaponManager.weaponery.Count)
                {
                    yValueUp = 0;
                }

                equipedWeaponManager.SwitchWeapon(yValueUp);
            }
        }
    }

    private bool test = false;

    /// <summary>
    /// Will perform attack based on action, if just clicked, LightAttack will be performed and if action is Hold, attack will charge and by releasing button HardAttack will perform
    /// </summary>
    /// <param name="context"></param>
    public void OnAttack(InputAction.CallbackContext context)
    {
        
        if (context.interaction is SlowTapInteraction && context.started && context.time > 1 && context.action.IsPressed())
        {
            if (staminaBar.GetCurrentStamina() >= currentWeapon.weaponData.heavyAttackStaminaCons)
            {
                isCharging = true;
                currentWeapon.HardAttack(true);
            }
            
        }
        if (context.performed && context.interaction is SlowTapInteraction)
        {
            currentWeapon.HardAttack(false);
            
            if (isCharging)
            {
                staminaBar.ReduceStamina(currentWeapon.weaponData.heavyAttackStaminaCons);
                isCharging = false;
            }
        }

        if (context.performed && context.interaction is TapInteraction)
        {
            currentWeapon.Attack();
        }

        if (context.interaction is SlowTapInteraction && context.canceled)
        {
            StartCoroutine(CancelHeavyAttack());
            isCharging = false;
        }
    }

    IEnumerator CancelHeavyAttack()
    {
        animator.SetBool("Return", true);
        currentWeapon.GetAnimator().SetBool("Return", true);
        currentWeapon.GetAnimator().SetBool(currentWeapon.weaponData.heavyAttackAnimation, false);
        animator.SetBool(currentWeapon.weaponData.heavyAttackAnimation, false);
        
        yield return new WaitForSeconds(currentWeapon.weaponData.heavyAttackChargeDuration);
        animator.SetBool("Return", false);
        currentWeapon.GetAnimator().SetBool("Return", false);
    }
    
    public void SetIsCharging(bool value)
    {
        isCharging = value;
    }

    /// <summary>
    /// Will perform block
    /// </summary>
    /// <param name="context"></param>
    public void OnBlock(InputAction.CallbackContext context)
    {
        if (!isBlocking)
        {
            currentWeapon.Block();
            Debug.Log("Player Controller - Block");
            isBlocking = !isBlocking;
        }
        else
        {
            isBlocking = !isBlocking;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = playerInput.actions["Movement"].ReadValue<Vector2>();

        if (value.x != 0 || value.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
                
        GetComponent<PlayerMovement>().Move(value);

        animator.SetBool("Walk", isWalking);
        currentWeapon.GetAnimator().SetBool("Walk", isWalking); 
    }

    public void OnRun(InputAction.CallbackContext context)
    {

        if (!isRunning)
        {
            isRunning = !isRunning;
            GetComponent<PlayerMovement>().Run();

            if (context.action.IsPressed() && staminaBar.GetCurrentStamina() > 0.2)
            {
                Debug.Log("utekaaaaam");
                animator.SetBool("Run", true);
                currentWeapon.GetAnimator().SetBool("Run", true);

            }
            else
            {
                animator.SetBool("Run", false);
                currentWeapon.GetAnimator().SetBool("Run", false);
            }
        }
        else
        {
            isRunning = !isRunning;
        }
        
    }

    private bool interacted = false;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
            if (!interacted)
            {
                transform.GetComponent<PlayerInteraction>().Interact();
                interacted = true;
            } 
            
        }
        else if (context.performed) 
        { 
            interacted = false; 
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (staminaBar.GetCurrentStamina() > 15)
            {
                GetComponent<PlayerMovement>().Jump();
            }
            
        }
    }

    public void OnSpellCast(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            spellManager.CallActiveBall();
        }
    }

    public void OnSpellCasting(InputAction.CallbackContext context)
    {
        
        if (context.action.IsPressed())
        {
            spellManager.CallActive();

        }
        else
        {
            spellManager.CallDeactive();
        }
    }

    public void OnUtilCast(InputAction.CallbackContext context)
    {

        if (context.action.IsPressed())
        {
            spellManager.CallActiveUtility();
            Debug.Log("Called Util cast");

        }
    }
    public void OnSpellSwitch(InputAction.CallbackContext context)
    {
        if (context.performed) {
            spellManager.SpellSwitch(context.control.name);
        }
    }

    public void GotHit()
    {
        currentWeapon.GetComponent<WeaponAnimations>().GotHit();
    }

}