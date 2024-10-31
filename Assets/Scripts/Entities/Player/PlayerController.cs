using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public PlayerInputActions playerInputActions;
    public Weapon currentWeapon;
    private EquipedWeaponManager equipedWeaponManager;

    public Animator animator;

    private bool isCharging = false;

    private bool isWalking = false;

    [Header("Testujem")]
    private AnimatorStateInfo stateInfo;
    private Animator oldAnimator;
    private AnimatorClipInfo[] clipInfo;
    private AnimatorControllerParameter[] parametre;
    private Animator bodyAni;

    private void Awake()
    {
        equipedWeaponManager = GetComponent<EquipedWeaponManager>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.SetCallbacks(this);
        playerInputActions.Enable();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    /// <summary>
    /// update is readed every frame
    /// </summary>
    private void Update()
    {
        if (playerInputActions.Player.Attack.IsPressed() && playerInputActions.Player.Block.IsInProgress())
        { 
            currentWeapon.Bash(); 
        }
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
            //StartCoroutine(test());

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

    private IEnumerator test()
    {
        Debug.Log($"{currentWeapon.GetBodyAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime} === {currentWeapon.GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime}");
        currentWeapon.GetAnimator().Play(currentWeapon.GetBodyAnimator().GetCurrentAnimatorStateInfo(0).fullPathHash, 0, currentWeapon.GetBodyAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return null;
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

    /// <summary>
    /// Will perform attack based on action, if just clicked, LightAttack will be performed and if action is Hold, attack will charge and by releasing button HardAttack will perform
    /// </summary>
    /// <param name="context"></param>
    public void OnAttack(InputAction.CallbackContext context)
    {
            if (context.started)
            {
                isCharging = true;
            }
            else if (isCharging)
            {
                if (context.interaction is TapInteraction && context.performed)
                {
                    
                    currentWeapon.Attack();
                    isCharging = false;

                }
                if (context.action.IsPressed())
                {
                    currentWeapon.HardAttack(true);
                }
                else
                {
                    
                    currentWeapon.HardAttack(false);
                    isCharging = false;
                }
            } 
    }
    
    /// <summary>
    /// Will perform block
    /// </summary>
    /// <param name="context"></param>
    public void OnBlock(InputAction.CallbackContext context)
    { 
        currentWeapon.Block(); 
    }
    //private bool isWalking = false;
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = playerInputActions.Player.Movement.ReadValue<Vector2>();

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
        if (context.action.IsPressed())
        {
            animator.SetBool("Run", true);
            currentWeapon.GetAnimator().SetBool("Run", true);
            
        }
        else
        {
            animator.SetBool("Run", false);
            currentWeapon.GetAnimator().SetBool("Run", false);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        transform.GetComponent<PlayerInteraction>().Interact();
    }

    public void GotHit()
    {
        currentWeapon.GetComponent<WeaponAnimations>().GotHit();
    }

}