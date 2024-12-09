using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private Dropdown difficulty;
    [Header("Video")]
    [SerializeField] private Dropdown quality;
    [SerializeField] private Dropdown resolution;
    [SerializeField] private Dropdown anti_aliasing;
    [SerializeField] private Toggle fullscreen;
    [SerializeField] private Toggle v_sync;
    [Header("Audio")]
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider soundVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider effectVolume;

    void Start()
    {
        Settings.Initialize();

        difficulty.value = SetDifficulty(Settings.Get<string>("DIFFICULTY"));
        quality.value = SetQuality(Settings.Get<string>("QUALITY_LEVEL"));
        resolution.value = Settings.Get<int>("RESOLUTION_ID");
        anti_aliasing.value = Settings.Get<int>("ANTI-ALIASING");
        fullscreen.isOn = Settings.Get<bool>("FULLSCREEN");
        v_sync.isOn = Settings.Get<bool>("V-SYNC");
        masterVolume.value = Settings.Get<float>("MASTER_VOLUME");
        soundVolume.value = Settings.Get<float>("SOUND_VOLUME");
        musicVolume.value = Settings.Get<float>("MUSIC_VOLUME");
        effectVolume.value = Settings.Get<float>("EFFECT_VOLUME");
    }

    public void applyChanges()
    {
        Settings.Save();

        QualitySettings.SetQualityLevel(quality.value);
        string[] resolutions = resolution.itemText.text.Split("x");
        Screen.SetResolution(Int32.Parse(resolutions[0]), Int32.Parse(resolutions[1]), fullscreen.isOn);
        QualitySettings.antiAliasing = anti_aliasing.value;
        QualitySettings.vSyncCount = (v_sync.isOn) ? 2 : 0;
    }

    public void OnDifficultyChange()
    {
        Settings.Set("DIFFICULTY", difficulty.itemText.text);
    }

    public void OnQualityChange()
    {
        Settings.Set("QUALITY_LEVEL", quality.itemText.text);

        QualitySettings.SetQualityLevel(quality.value);

        anti_aliasing.value = QualitySettings.antiAliasing;
        v_sync.isOn = (QualitySettings.vSyncCount == 0) ? false : true; 
    }

    public void OnResolutionChange()
    {
        Settings.Set("RESOLUTION", resolution.itemText.text);
        Settings.Set("RESOLUTION_ID", resolution.value);
    }

    public void OnAntiAliasingChange()
    {
        Settings.Set("ANTI-ALIASING", anti_aliasing.value);
    }

    public void OnFullScreenChange()
    {
        Settings.Set("FULLSCREEN", fullscreen.isOn);
    }

    public void OnVSyncChange()
    {
        Settings.Set("V-SYNC", v_sync.isOn);
    }

    public void OnMasterVolumeChange()
    {
        Settings.Set("MASTER_VOLUME", masterVolume.value);
    }

    public void OnMusicVolumeChange()
    {
        Settings.Set("MUSIC_VOLUME", musicVolume.value);
    }

    public void OnEffectVolumeChange()
    {
        Settings.Set("EFFECT_VOLUME", effectVolume.value);
    }

    public void OnSoundVolumeChange()
    {
        Settings.Set("SOUND_VOLUME", soundVolume.value);
    }

    private int SetDifficulty(string value)
    {
        switch(value)
        {
            case "Easy":
                return 0;
            case "Medium":
                return 1;
            case "Hard":
                return 2;
        }
        return 1;
    }

    private int SetQuality(string value)
    {
        switch(value)
        {
            case "Very Low":
                return 0;
            case "Low":
                return 1;
            case "Medium":
                return 2;
            case "High":
                return 3;
            case "Very High":
                return 4;
            case "Ultra":
                return 5;
        }
        return 2;
    }
}
