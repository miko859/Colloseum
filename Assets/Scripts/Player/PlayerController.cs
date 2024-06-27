using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public Weapon currentWeapon;

    private bool isCharging = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.performed += OnAttack;
        //playerInputActions.Player.Attack.performed -= OnHardAttack;
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

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCharging = true;
        }
        else if (context.duration < 0.1 && isCharging)
        {
            isCharging = false;
            Debug.Log("LIGHT ATTACK");
            currentWeapon.Attack();
            
        }
        else if (isCharging)
        {
            isCharging = false;
            Debug.Log("PREPARING HARD ATTACK");
            currentWeapon.HardAttack();
        }
    }
    /*
    private void OnHardAttack(InputAction.CallbackContext context)
    {
        if (currentWeapon != null)
        {
            Debug.Log("PREPARING HARD ATTACK");
            currentWeapon.HardAttack();
        } 
    }*/
    
    private void OnBlock(InputAction.CallbackContext context)
    {
        if (currentWeapon != null)
        {
            currentWeapon.Block();
        }
    }

}