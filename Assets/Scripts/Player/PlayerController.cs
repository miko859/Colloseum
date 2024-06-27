using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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

    private void Update()
    {
        // Tu mÙûeö pridaù logiku na v˝menu zbranÌ naprÌklad pomocou tlaËidiel
        // if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(axe);
        // if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(sword);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = newWeapon;
        currentWeapon.gameObject.SetActive(true);
    }

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
                Debug.Log("LIGHT ATTACK");
                currentWeapon.Attack();
            }
            if (context.action.IsPressed())
            {
                Debug.Log("PREPARING HARD ATTACK");
                currentWeapon.HardAttack(true);
            }
            else
            {
                isCharging = false;
                Debug.Log("PREPARING HARD ATTACK");
                currentWeapon.HardAttack(false);
            }

        }
    }
    
    private void OnBlock(InputAction.CallbackContext context)
    {
        if (currentWeapon != null)
        {
            currentWeapon.Block();
        }
    }

}