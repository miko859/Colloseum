using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject optionsGameplay;
    public GameObject optionsVideo;
    public GameObject optionsAudio;
    public GameObject optionsCotrolls;
    public GameObject optionsBack;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Options()
    {
        options.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        options.SetActive(false);
    }
}
