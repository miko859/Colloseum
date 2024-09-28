using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public Weapon currentWeapon;
    private EquipedWeaponManager equipedWeaponManager;

    public Animator animator;

    private bool isCharging = false;

    private bool isWalking = false;

    private void Awake()
    {
        equipedWeaponManager = GetComponent<EquipedWeaponManager>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.performed += OnAttack;
        playerInputActions.Player.Block.performed += OnBlock;
        playerInputActions.Player.Movement.performed += OnMovement;
        playerInputActions.Player.ChangeWeapon.performed += OnScroll;
        playerInputActions.Player.Run.performed += OnRun;
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
    public void EquipWeapon(Weapon newWeapon)
    {
        /*
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }*/

        currentWeapon = newWeapon;
       // currentWeapon.gameObject.SetActive(true);
    }

    public void OnScroll(InputAction.CallbackContext context)
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
                isCharging = false;
                currentWeapon.Attack();
 
            }
            if (context.action.IsPressed())
            {
                currentWeapon.HardAttack(true);
            }
            else
            {
                isCharging = false;
                currentWeapon.HardAttack(false);
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

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            
            Debug.Log("Pressed");
            if (context.control is KeyControl key && key.keyCode == Key.LeftShift) {
                //animator.SetBool("Run", true);
                Debug.Log("Holding Left Shift interaction");
            }
            else if (context.control.name == "w" || context.control.name == "s" || context.control.name == "a" || context.control.name == "d") {
                animator.SetBool("Walk", true);
                Debug.Log("Holding W interaction");
            }
        }
        else if (context.performed) {
            //animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
            Debug.Log("Player is Idle");
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

}