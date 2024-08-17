using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMainMenu : MonoBehaviour{
    bool menuOn = true;
    public GameObject MainMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            MainMenu.SetActive(menuOn);
            Cursor.lockState = menuOn ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = !menuOn ? 1.0f : 0.0f;
            menuOn = !menuOn;
        }
    }
}
