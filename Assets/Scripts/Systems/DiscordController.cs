using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using UnityEngine.SceneManagement;

public class DiscordController : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string details = "Playing Life Of Bean";
    [Space]
    public string largeImage = "game_logo";
    public string largeText = "Life Of Bean";

    private static bool instanceExists;
    public Discord.Discord discord;

    private long time;

    Scene currentScene;

    void Awake() 
    {
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Log in with the Application ID
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        UpdateStatus();
    }

    void Update()
    {
        // Destroy the GameObject if Discord isn't running
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }

        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene;
        if(scene.name == "CutScene")  details = "Getting up to date on the lore";
        if(scene.name == "Menu") details = "Preparing to fight some beans";
        if(scene.name == "InitialScene")details = "In training";
        if(scene.name == "ArenaIntro") details = "Admiring the arena";
        if(scene.name == "Arena")
        {
            LevelManager levelManager;
            levelManager = FindObjectOfType<LevelManager>();
            if(levelManager != null)
            {
                details = "Fighting in the arena (Score " + levelManager.score + ")";
            }
        }
        if(scene.name == "Credits") details = "Appreciating the devs work";
    }

    void LateUpdate() 
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        // Update Status every frame
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                Assets = 
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };


            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}