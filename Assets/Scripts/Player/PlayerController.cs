using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public Weapon currentWeapon;

    private bool isCharging = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.performed += OnAttack;
        playerInputActions.Player.Block.performed += OnBlock;
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
        
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = newWeapon;
        currentWeapon.gameObject.SetActive(true);
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
}