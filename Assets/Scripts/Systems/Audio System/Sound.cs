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
    //this is pitch distortion based on distance, imagine the pitch changing as a bullet flys past you. Mostly useless though so keep at 0
    public float dopplerLevel = 0f;
    public float maxDistance = 50f;
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
