using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public AnimationObject[] animations;

    Scene currentScene;

    public float fadeLength = 0.3f;

    public Animation animSource;


    // Start is called before the first frame update
    void Start()
    {
        animSource = gameObject.AddComponent<Animation>();
        animSource.playAutomatically = true;

        //add sounds to audio sources with their varying info
        foreach (AnimationObject a in animations)
        {
            //adds the animation
            a.source = animSource;

            //assigns the animation clips
            animSource.AddClip(a.clip, a.name);
        }
    }

    void Update()
    {

    }

    //plays sound use for non overlapping audio like grenades or footsteps
    public void PlayNoSkip(string name)
    {
        AnimationObject a = Array.Find(animations, animation => animation.name == name);
        //custom error message when cant find clip name
        if (a == null)
        {
            Debug.LogWarning("Animation: " + name + " not found!");
            return;
        }

        //animSource.CrossFadeQueued(a.name, fadeLength, QueueMode.PlayNow);
        animSource.PlayQueued(a.name, QueueMode.CompleteOthers);
    }

    //plays sound use for non overlapping audio like grenades or footsteps
    public void PlayAnim(string name)
    {
        AnimationObject a = Array.Find(animations, animation => animation.name == name);
        //custom error message when cant find clip name
        if (a == null)
        {
            Debug.LogWarning("Animation: " + name + " not found!");
            return;
        }

        animSource.CrossFadeQueued(a.name, fadeLength, QueueMode.PlayNow);
        //animSource.PlayQueued(a.name, QueueMode.PlayNow);
    }

    public void StopAll()
    {
        if(animSource.isPlaying)
        {
            animSource.Stop();
        }
    }
}
