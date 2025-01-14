using UnityEngine;

public class MainMenuFunctions : MonoBehaviour
{
    public GameObject mainMenu;
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        mainMenu.SetActive(false);
        Time.timeScale = false ? 0.0f : 1.0f;

    }
}
