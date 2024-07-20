using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    private bool isVisible = false; // Track the visibility state
    public GameObject uiElement; // Reference to the UI element to toggle

    void Start()
    {
        // Initially hide the UI element
        ToggleInventory(false);
    }

    void Update()
    {
        // Toggle the UI element visibility when the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory(!isVisible);
        }
    }

    private void ToggleInventory(bool state)
    {
        isVisible = state;
        uiElement.SetActive(state);
    }
}