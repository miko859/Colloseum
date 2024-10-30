using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject inventory;
    public GameObject mainMenu;
    public InventoryManager inventoryManager;

    private bool isInventoryOpen = false;
    private bool isMenuOpen = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isMenuOpen)
            {
                isInventoryOpen = !isInventoryOpen;
                inventory.SetActive(isInventoryOpen);
                mainMenu.SetActive(false);
                inventoryManager.ListItems();

                Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
                Time.timeScale = isInventoryOpen ? 0.0f : 1.0f;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!isInventoryOpen)
            {
                isMenuOpen = !isMenuOpen;
                mainMenu.SetActive(isMenuOpen);
                inventory.SetActive(false);

                Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
                Time.timeScale = isMenuOpen ? 0.0f : 1.0f;
            }
        }
    }
}
