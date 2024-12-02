using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    [SerializeField]private GameObject activeScreen;


    [Header("Buttons")]
    public GameObject optionsGameplayB;
    public GameObject optionsVideoB;
    public GameObject optionsAudioB;
    public GameObject optionsCotrollsB;
    public GameObject optionsBackB;

    [Header("Option screens")]
    public GameObject optionsGameplayS;
    public GameObject optionsVideoS;
    public GameObject optionsAudioS;
    public GameObject optionsCotrollsS;

    private void Start()
    {
        
        options.SetActive(false);
    }

    public void PlayGame()
    {
        transform.parent = null;
        DontDestroyOnLoad(this);
        
        SceneManager.LoadSceneAsync(1);
    }

    public void Options()
    {
        options.SetActive(true);

        activeScreen = optionsGameplayS;
        activeScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        options.SetActive(false);
    }

    public void ScreenSwitchToVideoOption()
    {
        OptionScreenSwitch(optionsVideoS);
    }
    public void ScreenSwitchToGameplayOption()
    {
        OptionScreenSwitch(optionsGameplayS);
    }
    public void ScreenSwitchToAudioOption()
    {
        OptionScreenSwitch(optionsAudioS);
    }
    public void ScreenSwitchToControlls()
    {
        OptionScreenSwitch(optionsCotrollsS);
    }
    private void OptionScreenSwitch(GameObject screen)
    {
        activeScreen.SetActive(false);
        activeScreen = screen;
        activeScreen.SetActive(true);
    }
}
