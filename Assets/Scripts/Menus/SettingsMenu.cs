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
        player = GameObject.Find("Player").GetComponent<playerMovement>();
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
            PlayerPrefs.SetInt("FullScreen", 1);
            fullscreenToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
            fullscreenToggle.isOn = false;
        }
    }

    void CheckFullscreen(int num)
    {
        if(num == 0)
        {
            SetFullscreen(true);
            PlayerPrefs.SetInt("FullScreen", num);
            fullscreenToggle.isOn = true;
        }
        else
        {
            SetFullscreen(false);
            PlayerPrefs.SetInt("FullScreen", num);
            fullscreenToggle.isOn = false;
        }
    }

    void LoadPrefs()
    {
        QualityDropdown.value = PlayerPrefs.GetInt("QualityLevel", 3);
        master.value = PlayerPrefs.GetFloat("MasterVolume", -10);
        music.value = PlayerPrefs.GetFloat("BgmVolume", -10);
        effects.value = PlayerPrefs.GetFloat("SfxVolume", -10);
        sensitivity.value = PlayerPrefs.GetFloat("Sensitivity", 150);
        player.mouseSensitivity = sensitivity.value;
        CheckFullscreen(PlayerPrefs.GetInt("FullScreen", 0));
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
