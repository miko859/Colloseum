
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleInventory : MonoBehaviour{
    bool inventoryOn = true;
    public GameObject inventory;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            inventory.SetActive(inventoryOn);
            Cursor.lockState = inventoryOn ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = !inventoryOn ? 1.0f : 0.0f;
            inventoryOn = !inventoryOn;
        }
    }
}
