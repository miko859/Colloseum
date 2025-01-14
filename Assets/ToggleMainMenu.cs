using UnityEngine;

public class ToggleMainMenu : MonoBehaviour
{
    bool menuOn = true;
    public GameObject MainMenu;
    public GameObject ToggleInventory;
    int n = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            n++;
            if (n % 2 == 0)
            {
                ToggleInventory.SetActive(false);
            }
            else
            {
                ToggleInventory.SetActive(true);
            }
            MainMenu.SetActive(menuOn);
            Cursor.lockState = menuOn ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = !menuOn ? 1.0f : 0.0f;
            menuOn = !menuOn;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                return;
            }
        }
    }
}
