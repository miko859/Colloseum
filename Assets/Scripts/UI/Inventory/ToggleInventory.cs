using UnityEngine;

public class ToggleInventory : MonoBehaviour{
    bool inventoryOn = true;
    public GameObject inventory;
    public GameObject ToggleMainMenu;
    int n = 1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        { n++;
            if (n % 2 == 0)
            {
                ToggleMainMenu.SetActive(false);
            }
            else
            {
                ToggleMainMenu.SetActive(true);
            }
            
            inventory.SetActive(inventoryOn);
            Cursor.lockState = inventoryOn ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = !inventoryOn ? 1.0f : 0.0f;
            inventoryOn = !inventoryOn;
            if (Input.GetKeyDown(KeyCode.V))
            {
                return;
            }
        }
    }
}
