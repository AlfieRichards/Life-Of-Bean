using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCredits());
    }


    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
        }
    }


    public IEnumerator PlayCredits()
    {
        //wait
        yield return new WaitForSeconds(55);

        //load menu
        levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
    }
}
