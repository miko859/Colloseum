using Blameless.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TMP_Dropdown difficulty;
    [Header("Video")]
    [SerializeField] private TMP_Dropdown quality;
    [SerializeField] private TMP_Dropdown resolution;
    [SerializeField] private TMP_Dropdown anti_aliasing;
    [SerializeField] private Toggle fullscreen;
    [SerializeField] private Toggle v_sync;
    [Header("Audio")]
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider soundVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider effectVolume;

    void Start()
    {
        //Settings.Initialize();

        difficulty.value = Settings.Conf.Get<int>("DIFFICULTY");
        quality.value = Settings.Conf.Get<int>("QUALITY_LEVEL");
        resolution.value = Settings.Conf.Get<int>("RESOLUTION_ID");
        anti_aliasing.value = Settings.Conf.Get<int>("ANTI-ALIASING");
        fullscreen.isOn = Settings.Conf.Get<bool>("FULLSCREEN");
        v_sync.isOn = Settings.Conf.Get<bool>("V-SYNC");
        masterVolume.value = Settings.Conf.Get<float>("MASTER_VOLUME");
        soundVolume.value = Settings.Conf.Get<float>("SOUND_VOLUME");
        musicVolume.value = Settings.Conf.Get<float>("MUSIC_VOLUME");
        effectVolume.value = Settings.Conf.Get<float>("EFFECT_VOLUME");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadConfigOnStart()
    { 
        Settings.Initialize();

        QualitySettings.SetQualityLevel(Settings.Get<int>("QUALITY_LEVEL"));
        string[] resolutions = Settings.Get<string>("RESOLUTION").Split("x");

        if (resolutions.Length != 2 ||
    !int.TryParse(resolutions[0].Trim(), out int width) ||
    !int.TryParse(resolutions[1].Trim(), out int height))
        {
            Debug.LogError("Failed to parse resolution");
            return;
        }

        // Successfully parsed width and height
        Debug.Log($"Width: {width}, Height: {height}");

        Screen.SetResolution(width, height, Settings.Conf.Get<bool>("FULLSCREEN"));
        QualitySettings.antiAliasing = Settings.Get<int>("ANTI-ALIASING");
        QualitySettings.vSyncCount = (Settings.Get<bool>("V-SYNC")) ? 2 : 0;
    }

    public void ApplyChanges()
    {
        Settings.Conf.Save();
        Debug.Log("Apply");

        QualitySettings.SetQualityLevel(quality.value);
        Debug.Log("Resolution -> " + resolution.options[resolution.value].text);
        string[] resolutions = resolution.options[resolution.value].text.Split("x");
        Debug.Log(resolutions[0] + " " + resolutions[1]);
        if (resolutions.Length != 2 ||
    !int.TryParse(resolutions[0].Trim(), out int width) ||
    !int.TryParse(resolutions[1].Trim(), out int height))
        {
            Debug.LogError("Failed to parse resolution");
            return;
        }
        Screen.SetResolution(width, height, fullscreen.isOn);
        //Screen.fullScreenMode = fullscreen.isOn ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
        QualitySettings.antiAliasing = anti_aliasing.value;
        QualitySettings.vSyncCount = (v_sync.isOn) ? 2 : 0;
    }

    public void OnDifficultyChange()
    {
        Settings.Conf.Set<int>("DIFFICULTY", difficulty.value);
    }

    public void OnQualityChange()
    {
        Settings.Conf.Set("QUALITY_LEVEL", quality.value);

        QualitySettings.SetQualityLevel(quality.value);

        anti_aliasing.value = QualitySettings.antiAliasing;
        v_sync.isOn = (QualitySettings.vSyncCount == 0) ? false : true;
    }

    public void OnResolutionChange()
    {
        Settings.Conf.Set("RESOLUTION", resolution.options[resolution.value].text);
        Settings.Conf.Set("RESOLUTION_ID", resolution.value);
    }

    public void OnAntiAliasingChange()
    {
        Settings.Conf.Set("ANTI-ALIASING", anti_aliasing.value);
    }

    public void OnFullScreenChange()
    {
        Settings.Conf.Set("FULLSCREEN", fullscreen.isOn);
    }

    public void OnVSyncChange()
    {
        Settings.Conf.Set("V-SYNC", v_sync.isOn);
    }

    public void OnMasterVolumeChange()
    {
        Settings.Conf.Set("MASTER_VOLUME", masterVolume.value);
    }

    public void OnMusicVolumeChange()
    {
        Settings.Conf.Set("MUSIC_VOLUME", musicVolume.value);
    }

    public void OnEffectVolumeChange()
    {
        Settings.Conf.Set("EFFECT_VOLUME", effectVolume.value);
    }

    public void OnSoundVolumeChange()
    {
        Settings.Conf.Set("SOUND_VOLUME", soundVolume.value);
    }
}