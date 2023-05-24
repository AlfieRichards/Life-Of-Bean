using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int score = 0000;
    public string time = "00 : 00";
    private float actualTime = 0f;
    private int intTime = 0;

    public int spawnAmount = 10;
    public int roundDelay = 5;
    public List<EnemySpawner> spawners;

    int waveNumber;
    int spawnerCount;
    int neededScore;
    bool roundEnded = false;
    bool playing = true;
    bool soundPlayed = false;

    public Animator transition;
    public float transitionTime = 1f;
    public GameObject cam;

    public LevelLoader levelLoader;
    public AudioManager audioManager;
    private GameObject hurtCanvas;

    void Start()
    {
        waveNumber = 1;
        foreach(EnemySpawner spawner in spawners)
        {
            spawner._maxEnemies = spawnAmount;
            spawner._enemyCount = 0;
            spawnerCount += 1;
        }

        neededScore = (score + (spawnerCount * spawnAmount));
        hurtCanvas = GameObject.Find("HurtCanvas");
        levelLoader = FindObjectOfType<LevelLoader>();
        audioManager = FindObjectOfType<AudioManager>();

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioManager.ForcePlaySound("34");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playing)
        {
            float tTime = actualTime += Time.deltaTime;
            intTime = (int)Mathf.Round(tTime);

            int minutes = intTime / 60;
            int seconds = intTime % 60;

            string secondsString = "";
            if(seconds < 10)
            {
                secondsString = "0" + seconds;
            }
            else
            {
                secondsString = seconds.ToString();;
            }

            string minutesString = "";
            if(minutes < 10)
            {
                minutesString = "0" + minutes;
            }
            else
            {
                minutesString = minutes.ToString();;
            }

            time = minutesString + " : " + secondsString;

            if(score == 1 && !soundPlayed)
            {
                soundPlayed = true;
                audioManager.ForcePlaySound("35");
            }

            CheckRoundEnd();
        }
    }

    public void EnemyKilled()
    {
        score += 1;
    }

    public void PlayerKilled(GameObject player)
    {
        if(playing)
        {
            audioManager.ForcePlaySound("36");
            StartCoroutine(FadeOut(player));
        }
    }

    public IEnumerator FadeOut(GameObject player)
    {
        playing = false;
        Transform child = transform.GetChild(0);
        child.gameObject.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        Destroy(player);
        Debug.Log("player destroyed");
        Destroy(hurtCanvas);

        //Play animation
        Debug.Log("Cam on");
        cam.SetActive(true);
        cam.AddComponent<AudioListener>();
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioManager.ForcePlaySound("37");
    }

    void CheckRoundEnd()
    {
        if(score >= neededScore && !roundEnded)
        {
            Debug.Log("round Over");
            //play a ding sound
            Invoke("NewRound", roundDelay);
            roundEnded = true;
        }
    }

    void NewRound()
    {
        Debug.Log("starting Round");
        spawnAmount += 2;

        waveNumber += 1;

        foreach(EnemySpawner spawner in spawners)
        {
            spawner._maxEnemies = spawnAmount;
            spawner._enemyCount = 0;
        }

        neededScore = (score + (spawnerCount * spawnAmount));
        roundEnded = false;
    }


    public void Retry()
    {
        levelLoader.StartCoroutine(levelLoader.LoadLevel(3));
    }

    public void MainMenu()
    {
        levelLoader.StartCoroutine(levelLoader.LoadLevel(1));
    }

    public void Credits()
    {
        levelLoader.StartCoroutine(levelLoader.LoadLevel(4));
    }
}
