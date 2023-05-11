using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int index = 0;

    bool subMenuBool = false;
    bool vidOpen = false;
    bool audOpen = false;

    bool menusVisible = false;
    public CanvasGroup canvas;

    public  float lerp = 0f, duration = 2f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(menusVisible)
        {
            if(canvas.alpha < 1)
            {
                lerp += Time.deltaTime / duration;
                canvas.alpha = (int)Mathf.Lerp (canvas.alpha, 1f, lerp);
                //canvas.alpha += 0.05f;
            }
        }
        else
        {
            if(canvas.alpha > 0)
            {
                lerp -= Time.deltaTime / duration;
                canvas.alpha = (int)Mathf.Lerp (canvas.alpha, 0f, lerp * -1.5f);
                //canvas.alpha -= 0.05f;
            }
        }

        canvas.alpha = lerp;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(index == 1 && !subMenuBool)
            {
                subMenuBool = true;
                index = 0;
                lerp = 0;
                canvas.alpha = 0;
                
                mainMenu.SetActive(false);
                subMenu.SetActive(true);
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
                        menusVisible = false;
                        GetComponent<AnimationManager>().PlayNoSkip("AudioClose");
                        GetComponent<AnimationManager>().PlayNoSkip("VideoOpen");
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
                        menusVisible = false;
                        GetComponent<AnimationManager>().PlayNoSkip("VideoClose");
                        GetComponent<AnimationManager>().PlayNoSkip("AudioOpen");
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
                if(audOpen){GetComponent<AnimationManager>().PlayNoSkip("AudioClose");}
                if(vidOpen){GetComponent<AnimationManager>().PlayNoSkip("VideoClose");}

                subMenuBool = false;
                index = 0;

                vidOpen = false;
                audOpen = false;

                Invoke("NoSettingsActive", 3f);

                mainMenu.SetActive(true);
                subMenu.SetActive(false);
            }
        }

        if(Input.GetKeyDown("up"))
        {
            index--;
            if(index < 0)
            {
                index = 2;
            }
        }

        if(Input.GetKeyDown("down"))
        {
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
        if(vidOpen)
        {
            lerp = 0;
            menusVisible = true;
        }
    }
}
