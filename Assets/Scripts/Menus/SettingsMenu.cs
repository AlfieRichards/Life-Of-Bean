using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer masterMixer;

    public TMPro.TMP_Dropdown QualityDropdown;

    public Toggle fullscreenToggle;

    public Slider master, music, effects, sensitivity;

    public playerMovement player;

    void Start ()
    {
        //Loads all player prefs except resolution
        LoadPrefs();
    }

    public void SetSensitivity (float sens)
    {
        player.mouseSensitivity = sens;
        PlayerPrefs.SetFloat("Sensitivity", sens);
        Debug.Log(sens);
    }



    public void SetEffectsVolume (float volume)
    {
        masterMixer.SetFloat("SfxVolume", volume);
        PlayerPrefs.SetFloat("SfxVolume", volume);
        Debug.Log(volume);
    }

    public void SetMusicVolume (float volume)
    {
        masterMixer.SetFloat("BgmVolume", volume);
        PlayerPrefs.SetFloat("BgmVolume", volume);
        Debug.Log(volume);
    }

    public void SetMasterVolume (float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        Debug.Log(volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if(isFullscreen)
        {
            PlayerPrefs.SetInt("FullscreenBool", 1);
            fullscreenToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("FullscreenBool", 0);
            fullscreenToggle.isOn = false;
        }
    }

    void CheckFullscreen(int num)
    {
        if(num == 1)
        {
            SetFullscreen(true);
            PlayerPrefs.SetInt("FullscreenBool", num);
            fullscreenToggle.isOn = true;
        }
        else
        {
            SetFullscreen(false);
            PlayerPrefs.SetInt("FullscreenBool", num);
            fullscreenToggle.isOn = false;
        }
    }

    void LoadPrefs()
    {
        QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("BgmVolume");
        effects.value = PlayerPrefs.GetFloat("SfxVolume");
        sensitivity.value = PlayerPrefs.GetFloat("Sensitivity");
        player.mouseSensitivity = sensitivity.value;
        CheckFullscreen(PlayerPrefs.GetInt("FullscreenBool"));
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }
}
