using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    public AudioMixer masterMixer;
    public Slider cutscene;
    public VideoPlayer videoPlayer;
    int cutScenePlayed;

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
        StartCoroutine(VideoDelay());

        if(cutScenePlayed == 0)
        {
            videoPlayer.Play();
            PlayerPrefs.SetInt("CutScenePlayed", 1);
        }
        else
        {
            levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
        }
    }


    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
        }
    }

    public void SetCutSceneVolume()
    {
        masterMixer.SetFloat("CutSceneVolume", cutscene.value);
        PlayerPrefs.SetFloat("CutSceneVolume", cutscene.value);
        Debug.Log(cutscene.value);
    }


    public IEnumerator VideoDelay()
    {
        //wait
        yield return new WaitForSeconds(55);

        //load menu
        levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
    }

    void LoadPrefs()
    {
        cutscene.value = PlayerPrefs.GetFloat("CutSceneVolume", -10);
        cutScenePlayed = PlayerPrefs.GetInt("CutScenePlayed", 0);
    }
}
