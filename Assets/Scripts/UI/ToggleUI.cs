using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    private bool isMenuOpen = false;
    private bool isInventoryOpen = false;
    public GameObject inventory;
    public GameObject mainMenu;
    public InventoryManager inventoryManager;
    public Button closeButton;
    public Button continueButton;

    void Start()
    {
        closeButton.onClick.AddListener(CloseInventory);
        continueButton.onClick.AddListener(ContinueGame);
    }

    void Update()
    {
        HandleInventoryToggle();
        HandleMenuToggle();
    }

    public void HandleInventoryToggle()
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
                Cursor.visible = isInventoryOpen;
                Time.timeScale = isInventoryOpen ? 0.0f : 1.0f;
            }
        }
    }

    public void HandleMenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!isInventoryOpen)
            {
                isMenuOpen = !isMenuOpen;
                mainMenu.SetActive(isMenuOpen);
                inventory.SetActive(false);

                Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = isMenuOpen;
                Time.timeScale = isMenuOpen ? 0.0f : 1.0f;
            }
        }
    }

    private void CloseInventory()
    {
        isInventoryOpen = false;
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    private void ContinueGame()
    {
        isMenuOpen = false;
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }
}
