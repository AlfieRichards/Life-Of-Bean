using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider master, music, effects;

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
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

    void LoadPrefs()
    {
        Debug.Log("FUCK");
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("BgmVolume");
        effects.value = PlayerPrefs.GetFloat("SfxVolume");
    }
}
