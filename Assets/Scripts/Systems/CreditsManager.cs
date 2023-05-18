using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Animator cam;
    [SerializeField] private Animator text;
    [SerializeField] private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCredits());
    }


    public IEnumerator PlayCredits()
    {
        //wait
        yield return new WaitForSeconds(37);

        //load menu
        levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
    }
}
