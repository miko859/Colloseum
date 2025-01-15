using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    private GameObject activeScreen;
    public InputActionAsset actions;

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
        GetData();
        options.SetActive(true);

        activeScreen = optionsGameplayS;
        activeScreen.SetActive(true);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ApplySettings()
    {
        SaveData();
        activeScreen.SetActive(false);
        activeScreen = null;
        options.SetActive(false);
        GetComponent<Options>().ApplyChanges();
    }

    public void BackToMainMenu()
    {
        options.SetActive(false);
        activeScreen.SetActive(false);
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

    public void GetData()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void SaveData()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
}
