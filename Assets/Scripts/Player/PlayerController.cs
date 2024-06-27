using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public Weapon currentWeapon;



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

    private void WeaponShow()
    {
        currentWeapon.enabled = true;
    }

    private void WeaponHide()
    {
        currentWeapon.gameObject.active = false;
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
        
        Debug.Log("UTOK");
        if (currentWeapon != null)
        {
            Debug.Log("UTOK IF");
            currentWeapon.LightAttack();
        }
    }

    private void OnBlock(InputAction.CallbackContext context)
    {
     
        OnDisable();

        Debug.Log("BLOK");
        if (currentWeapon != null)
        {
            Debug.Log("BLOK IF");
            currentWeapon.Block();
        }
    }

}