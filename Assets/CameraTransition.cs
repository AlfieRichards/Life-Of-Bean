using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public AudioManager audioManager;
    public LevelLoader levelLoader;
    // Start is called before the first frame update

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(AudioDelay());
        StartCoroutine(VideoDelay());
    }

    public IEnumerator AudioDelay()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.PlayOneShotSound("33");
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && levelLoader != null)
        {
            levelLoader.StartCoroutine(levelLoader.LoadLevel(3));
        }
    }

    public IEnumerator VideoDelay()
    {
        //wait
        yield return new WaitForSeconds(10);

        //load menu
        levelLoader.StartCoroutine(levelLoader.LoadLevel(3));
    }

}
