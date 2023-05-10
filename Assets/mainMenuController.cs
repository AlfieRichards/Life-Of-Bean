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

    int index = 0;

    bool subMenuBool = false;
    bool vidOpen = false;
    bool audOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("asdgksdfghjsdf");
            if(index == 1 && !subMenuBool)
            {
                subMenuBool = true;
                index = 0;
                
                mainMenu.SetActive(false);
                subMenu.SetActive(true);
            }

            if(index == 0 && subMenuBool)
            {
                if(!audOpen && !vidOpen){GetComponent<AnimationManager>().PlayNoSkip("VideoOpen");}
                if(audOpen && !vidOpen)
                {
                    GetComponent<AnimationManager>().PlayNoSkip("AudioClose");
                    GetComponent<AnimationManager>().PlayNoSkip("VideoOpen");
                }
                audOpen = false;
                vidOpen = true;
                //video settings
            }

            if(index == 1 && subMenuBool)
            {
                if(!audOpen && !vidOpen){GetComponent<AnimationManager>().PlayNoSkip("AudioOpen");}
                if(!audOpen && vidOpen)
                {
                    GetComponent<AnimationManager>().PlayNoSkip("VideoClose");
                    GetComponent<AnimationManager>().PlayNoSkip("AudioOpen");
                }
                vidOpen = false;
                audOpen = true;
                //audio settings
            }

            if(index == 2 && subMenuBool)
            {
                if(audOpen){GetComponent<AnimationManager>().PlayNoSkip("AudioClose");}
                if(vidOpen){GetComponent<AnimationManager>().PlayNoSkip("VideoClose");}

                subMenuBool = false;
                index = 0;

                vidOpen = false;
                audOpen = false;

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
}
