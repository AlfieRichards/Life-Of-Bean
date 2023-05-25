using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class mainMenuController : MonoBehaviour
{
    public Material textOff;
    public Material textOn;

    //main menu
    public GameObject text1, text2, text3;

    //sub menu
    public GameObject text4, text5, text6;

    public GameObject subMenu;
    public GameObject mainMenu;

    public GameObject videoSettings;
    public GameObject audioSettings;
    public LevelLoader levelLoader;

    public AudioManager audioManager;

    int index = 0;

    bool subMenuBool = false;
    bool vidOpen = false;
    bool audOpen = false;

    bool menusVisible = false;
    public CanvasGroup canvas;
    public CanvasGroup canvas2;

    //0 is video, 1 is audio
    int lastOpen;

    public  float lerp = 0f, duration = 2f;

    public AudioMixer masterMixer;
    public Slider master, music, effects;

    
    
    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound("startup");
        audioManager.PlaySound("pcLoop");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!menusVisible)
        {
            if(canvas.alpha > 0 || canvas2.alpha > 0)
            {
                if(lastOpen == 0)
                {
                    lerp -= Time.deltaTime / duration;
                    canvas.alpha = lerp;                   
                }
                if(lastOpen == 1)
                {
                    lerp -= Time.deltaTime / duration;
                    canvas2.alpha = lerp;                 
                }
            }
        }
        if(menusVisible && vidOpen && !audOpen)
        {
            if(canvas.alpha < 1)
            {
                lerp += Time.deltaTime / duration;
                canvas.alpha = lerp;
            }
        }
        if(menusVisible && audOpen && !vidOpen)
        {
            if(canvas2.alpha < 1)
            {
                lerp += Time.deltaTime / duration;
                canvas2.alpha = lerp;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            audioManager.PlayOneShotSound("keypress");

            if(index == 0 && !subMenuBool)
            {
                PlayGame();
            }

            if(index == 1 && !subMenuBool)
            {
                subMenuBool = true;
                index = 0;
                lerp = 0;
                canvas.alpha = 0;
                canvas2.alpha = 0;
                
                audioManager.PlayOneShotSound("loading");
                mainMenu.SetActive(false);
                subMenu.SetActive(true);
            }

            if(index == 2 && !subMenuBool)
            {
                QuitGame();
            }

            if(index == 0 && subMenuBool)
            {
                if(!audOpen && !vidOpen)
                {
                    GetComponent<AnimationManager>().PlayNoSkip("VideoOpen");

                    audOpen = false;
                    vidOpen = true;

                    Invoke("VideoSettingsActive", 2.2f);
                }
                else
                {
                    if(audOpen && !vidOpen)
                    {
                        lastOpen = 1;
                        menusVisible = false;
                        audioManager.PlayOneShotSound("PaperOpen");
                        GetComponent<AnimationManager>().PlayNoSkip("AudioClose");
                        GetComponent<AnimationManager>().PlayNoSkip("VideoOpen");
                        audioManager.PlayOneShotSound("PaperOpen");
                        //GetComponent<AnimationManager>().PlayNoSkip("FadeIn");
                    }
                    audOpen = false;
                    vidOpen = true;

                    Invoke("VideoSettingsActive", 4.2f);
                }
                //video settings
            }

            if(index == 1 && subMenuBool)
            {
                if(!audOpen && !vidOpen)
                {
                    GetComponent<AnimationManager>().PlayNoSkip("AudioOpen");

                    vidOpen = false;
                    audOpen = true;

                    Invoke("AudioSettingsActive", 2.2f);
                }
                else
                {
                    if(!audOpen && vidOpen)
                    {
                        lastOpen = 0;
                        menusVisible = false;
                        audioManager.PlayOneShotSound("PaperOpen");
                        GetComponent<AnimationManager>().PlayNoSkip("VideoClose");
                        GetComponent<AnimationManager>().PlayNoSkip("AudioOpen");
                        audioManager.PlayOneShotSound("PaperOpen");
                    }
                    vidOpen = false;
                    audOpen = true;

                    Invoke("AudioSettingsActive", 4.2f);
                }
                //audio settings
            }

            if(index == 2 && subMenuBool)
            {
                menusVisible = false;
                if(audOpen){GetComponent<AnimationManager>().PlayNoSkip("AudioClose"); audioManager.PlayOneShotSound("PaperOpen");}
                if(vidOpen){GetComponent<AnimationManager>().PlayNoSkip("VideoClose"); audioManager.PlayOneShotSound("PaperOpen");}

                subMenuBool = false;
                index = 0;

                vidOpen = false;
                audOpen = false;

                Invoke("NoSettingsActive", 3f);

                mainMenu.SetActive(true);
                subMenu.SetActive(false);
                audioManager.PlayOneShotSound("loading");
            }
        }

        if(Input.GetKeyDown("up"))
        {
            audioManager.PlayOneShotSound("keypress");
            index--;
            if(index < 0)
            {
                index = 2;
            }
        }

        if(Input.GetKeyDown("down"))
        {
            audioManager.PlayOneShotSound("keypress");
            index++;
            if(index > 2)
            {
                index = 0;
            }
        }

        //Debug.Log(index);

        text1.GetComponent<Renderer>().material = textOff;
        text2.GetComponent<Renderer>().material = textOff;
        text3.GetComponent<Renderer>().material = textOff;
        text4.GetComponent<Renderer>().material = textOff;
        text5.GetComponent<Renderer>().material = textOff;
        text6.GetComponent<Renderer>().material = textOff;

        switch (index)
        {
            case 0:
            {
                text1.GetComponent<Renderer>().material = textOn;
                text4.GetComponent<Renderer>().material = textOn;
                break;
            }
            case 1:
            {
                text2.GetComponent<Renderer>().material = textOn;
                text5.GetComponent<Renderer>().material = textOn;
                break;
            }
            case 2:
            {
                text3.GetComponent<Renderer>().material = textOn;
                text6.GetComponent<Renderer>().material = textOn;
                break;
            }
            default:
                break;
        }
    }

    void VideoSettingsActive()
    {
        Invoke("MenusVisible", 1f);
    }

    void AudioSettingsActive()
    {
        Invoke("MenusVisible", 1f);
    }

    void NoSettingsActive()
    {
        Invoke("MenusVisible", 1f);
    }

    void MenusVisible()
    {
        if(vidOpen || audOpen)
        {
            if(vidOpen){videoSettings.SetActive(true); audioSettings.SetActive(false);}
            if(audOpen){videoSettings.SetActive(false); audioSettings.SetActive(true);}
            lerp = 0;
            menusVisible = true;
        }
    }

    void LoadPrefs()
    {
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("BgmVolume");
        effects.value = PlayerPrefs.GetFloat("SfxVolume");


        switch (PlayerPrefs.GetInt("FPSLevel"))
        {
            case 0:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 30;
                break;
            }

            case 1:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                break;
            }

            case 2:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 90;
                break;
            }

            case 3:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 120;
                break;
            }

            case 4:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
                break;
            }
            default:
                break;
        }

        switch (PlayerPrefs.GetInt("QualityLevel"))
        {
            case 0:
            {
                QualitySettings.SetQualityLevel(0);
                break;
            }

            case 1:
            {
                QualitySettings.SetQualityLevel(1);
                break;
            }

            case 2:
            {
                QualitySettings.SetQualityLevel(2);
                break;
            }

            case 3:
            {
                QualitySettings.SetQualityLevel(3);
                break;
            }
            default:
                break;
        }

        switch (PlayerPrefs.GetInt("FullScreen"))
        {
            case 0:
            {
                Screen.fullScreen = true;
                break;
            }

            case 1:
            {
                Screen.fullScreen = false;
                break;
            }
            default:
                break;
        }
    }

    public void PlayGame()
    {
        Debug.Log("PLAY GAME!");
        levelLoader.LoadNextScene();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }
}
