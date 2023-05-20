using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public bool completedRange = false;
    public bool isRange = false;
    public bool isArena = false;

    public weaponScript gun;
    public AudioManager audioManager;
    public FadeController arenaText;



    // Start is called before the first frame update
    void Start()
    {
        if(isRange)
        {
            audioManager.PlayOneShotSound("9");
        }
        if(isArena)
        {
            audioManager.PlayOneShotSound("33");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("v"))
        {
            Debug.Log("help");
            if(isRange)
            {
                if(!gun.fired && gun.targetHits == 0)
                {
                    audioManager.PlayOneShotSound("12");
                    return;
                }
                if(gun._ammoCapacity == 0 && !gun.reloaded)
                {
                    audioManager.PlayOneShotSound("16");
                    return;
                }
                if(gun.fired && gun.targetHits == 0)
                {
                    audioManager.PlayOneShotSound("14");
                    completedRange = true;
                    return;
                }
            }
        }

        if(gun.targetHits >= 5 && !completedRange && isRange)
        {
            audioManager.PlayOneShotSound("21");
            completedRange = true;
            arenaText.locked = false;
        }

        if(isArena)
        {

        }
    }
}
