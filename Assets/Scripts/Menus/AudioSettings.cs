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

    public void SetEffectsVolume ()
    {
        masterMixer.SetFloat("SfxVolume", effects.value);
        PlayerPrefs.SetFloat("SfxVolume", effects.value);
        Debug.Log(effects.value);
    }

    public void SetMusicVolume ()
    {
        masterMixer.SetFloat("BgmVolume", music.value);
        PlayerPrefs.SetFloat("BgmVolume", music.value);
        Debug.Log(music.value);
    }

    public void SetMasterVolume ()
    {
        masterMixer.SetFloat("MasterVolume", master.value);
        PlayerPrefs.SetFloat("MasterVolume", master.value);
        Debug.Log(master.value);
    }

    void LoadPrefs()
    {
        master.value = PlayerPrefs.GetFloat("MasterVolume", -10);
        music.value = PlayerPrefs.GetFloat("BgmVolume", -10);
        effects.value = PlayerPrefs.GetFloat("SfxVolume", -10);
    }
}
