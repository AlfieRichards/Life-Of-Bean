using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    public List <GameObject> QualityOptions = new List <GameObject>();
    public List <GameObject> FPSOptions = new List <GameObject>();
    public List <GameObject> FullScreenOptions = new List <GameObject>();
    bool delay = false;

    public AudioController audioManager;

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuality (int qualityIndex)
    {
        audioManager.PlayOneShotSound("PaperCircle");
        if(delay){return;}
        delay = true;

        GameObject sender = QualityOptions[qualityIndex];
        //resets all toggles
        foreach(var option in QualityOptions)
        {
            if(option != sender)
            {
                option.GetComponent<Toggle>().isOn = false;
            }
        }

        switch (sender.name)
        {
            case "Low":
            {
                QualitySettings.SetQualityLevel(0);
                PlayerPrefs.SetInt("QualityLevel", 0);
                break;
            }

            case "Medium":
            {
                QualitySettings.SetQualityLevel(1);
                PlayerPrefs.SetInt("QualityLevel", 1);
                break;
            }

            case "High":
            {
                QualitySettings.SetQualityLevel(2);
                PlayerPrefs.SetInt("QualityLevel", 2);;
                break;
            }

            case "Ultra":
            {
                QualitySettings.SetQualityLevel(3);
                PlayerPrefs.SetInt("QualityLevel", 3);
                break;
            }
            default:
                break;
        }

        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);

        Invoke("DelayReset", 0.1f);
    }

    public void SetFPS (int fpsIndex)
    {
        audioManager.PlayOneShotSound("PaperCircle");
        if(delay){return;}
        delay = true;

        GameObject sender = FPSOptions[fpsIndex];
        //resets all toggles
        foreach(var option in FPSOptions)
        {
            if(option != sender)
            {
                option.GetComponent<Toggle>().isOn = false;
            }
        }

        switch (sender.name)
        {
            case "30":
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 30;
                PlayerPrefs.SetInt("FPSLevel", 0);
                break;
            }

            case "60":
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                PlayerPrefs.SetInt("FPSLevel", 1);
                break;
            }

            case "90":
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 90;
                PlayerPrefs.SetInt("FPSLevel", 2);;
                break;
            }

            case "120":
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 120;
                PlayerPrefs.SetInt("FPSLevel", 3);
                break;
            }
            case "Unlimited":
            {
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = -1;
                PlayerPrefs.SetInt("FPSLevel", 4);
                break;
            }
            default:
                break;
        }

        PlayerPrefs.SetInt("FPSLevel", fpsIndex);

        Invoke("DelayReset", 0.1f);
    } 

    public void SetFullscreen (int screenIndex)
    {
        audioManager.PlayOneShotSound("PaperCircle");
        if(delay){return;}
        delay = true;

        GameObject sender = FullScreenOptions[screenIndex];
        //resets all toggles
        foreach(var option in FullScreenOptions)
        {
            if(option != sender)
            {
                option.GetComponent<Toggle>().isOn = false;
            }
        }

        switch (sender.name)
        {
            case "Yes":
            {
                Screen.fullScreen = true;
                PlayerPrefs.SetInt("FullScreen", 0);
                break;
            }

            case "No":
            {
                Screen.fullScreen = false;
                PlayerPrefs.SetInt("FullScreen", 1);
                break;
            }
            default:
                break;
        }

        PlayerPrefs.SetInt("FullScreen", screenIndex);

        Invoke("DelayReset", 0.1f);
    }

    void DelayReset()
    {
        delay = false;
    }

    void LoadPrefs()
    {
        foreach(GameObject option in QualityOptions)
        {
            option.GetComponent<Toggle>().isOn = false;
        }

        switch (PlayerPrefs.GetInt("QualityLevel", 3))
        {
            case 0:
            {
                QualitySettings.SetQualityLevel(0);
                QualityOptions[0].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 1:
            {
                QualitySettings.SetQualityLevel(1);
                QualityOptions[1].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 2:
            {
                QualitySettings.SetQualityLevel(2);
                QualityOptions[2].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 3:
            {
                QualitySettings.SetQualityLevel(3);
                QualityOptions[3].GetComponent<Toggle>().isOn = true;
                break;
            }
            default:
                break;
        }

        foreach(GameObject option in FPSOptions)
        {
            option.GetComponent<Toggle>().isOn = false;
        }

        switch (PlayerPrefs.GetInt("FPSLevel", 4))
        {
            case 0:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 30;
                FPSOptions[0].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 1:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                FPSOptions[1].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 2:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 90;
                FPSOptions[2].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 3:
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 120;
                FPSOptions[3].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 4:
            {
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = -1;
                FPSOptions[4].GetComponent<Toggle>().isOn = true;
                break;
            }
            default:
                break;
        }

        foreach(GameObject option in FullScreenOptions)
        {
            option.GetComponent<Toggle>().isOn = false;
        }

        switch (PlayerPrefs.GetInt("FullScreen", 0))
        {
            case 0:
            {
                Screen.fullScreen = true;
                FullScreenOptions[0].GetComponent<Toggle>().isOn = true;
                break;
            }

            case 1:
            {
                Screen.fullScreen = false;
                FullScreenOptions[1].GetComponent<Toggle>().isOn = true;
                break;
            }
            default:
                break;
        }
    }
}
