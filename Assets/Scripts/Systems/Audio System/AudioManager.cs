using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Sound[] music;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundMixer;
    Scene currentScene;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        //makes it a singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        


        //add sounds to audio sources with their varying info
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = soundMixer;
        }

        foreach (Sound m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
            m.source.outputAudioMixerGroup = musicMixer;
        }
        //this could be used to play music when a scene loads
        //PlayMusic("BGM");
        SceneMusic();
    }


    void Update()
    {
        if(SceneManager.GetActiveScene() != currentScene)
        {
            SceneMusic();
        }
    }

    public void SceneMusic()
    {
        StopAll();
        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene;

        if(scene.name == "Menu")
        {
            PlayMusic("titleMusic");
        }
        if(scene.name == "MainScene")
        {
            PlayMusic("inGameMusic");
        }
    }

    //this could be used to play music when a scene loads


    //plays sound
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //custom error message when cant find clip name
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopAll()
    {
        Component[] sources;
        sources = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource tSource in sources){
            tSource.Stop();
        }
    }

    public void PlayMusic(string name)
    {
        Sound m = Array.Find(music, sound => sound.name == name);
        //custom error message when cant find clip name
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        m.source.Play();
    }

    //to play do
    //FindObjectOfType<AudioManager>().Play("SoundName")

    //to edit volume from another script do
    //AudioManager manager = FindObjectOfType<AudioManager>();
    //audioMixer.SetFloat("BgmVolume", volume);
}