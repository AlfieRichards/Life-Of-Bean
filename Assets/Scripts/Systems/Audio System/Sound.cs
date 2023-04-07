using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup outputAudioMixerGroup;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    //for 3d audio (sound being emitted directionally in scene) you want spatialBlend to be 1, for non directional sound (background music or sound emitted from player) set this to 0.5
    public float spatialBlend = 1f;
    [Range(0f, 1f)]
    //this is pitch distortion based on distance, i have no idea why it exists or whats its meant to be used for, so just set this to 0
    public float dopplerLevel = 0f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
