using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerSingleton : MonoBehaviour
{
    static ManagerSingleton instance;
    public Text sceneText;
    public GameObject startButton;

    void Awake()
    {
        startButton.GetComponent<Button>().Select();

        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //GUI.Label(new Rect(10, 10, 100, 20), "1 AND 2 TO SWITCH SCENES");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("Scene1");
        }
        else if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Scene2");
        }

        sceneText.text = (SceneManager.GetActiveScene().name);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 20), "Press 1 and 2 to switch scenes!");
        GUI.Label(new Rect(10, 30, 250, 20), "This is done with the manager singleton");
        GUI.Label(new Rect(10, 50, 250, 20), "And so is this text!");
        GUI.Label(new Rect(10, 100, 250, 20), "Current Scene Is:");
        GUI.Label(new Rect(10, 120, 250, 20), (SceneManager.GetActiveScene().name));
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
}
