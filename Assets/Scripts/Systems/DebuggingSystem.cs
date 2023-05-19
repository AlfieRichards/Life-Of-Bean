using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingSystem : MonoBehaviour
{
    //debug Toggles
    private bool _showWeapons;
    private bool _showPlayer;
    private bool _showDirection;

    //referenced Objects
    private weaponScript _weapon;
    private playerMovement _player;

    private void Start() 
    {
        _weapon = GameObject.Find("WeaponsSystem").GetComponent<weaponScript>();
        _player = GameObject.Find("Player").GetComponent<playerMovement>();
    }
    
    void OnGUI() 
    {
        if(_player._debugGUI)
        {
            GUI.Box(new Rect(10,10,170,130), "Debugging Controls");
            if (GUI.Button(new Rect(20,40,150,20), "Show Weapon Values"))
            {
                Debug.Log("Showing Weapon Values!");
                if(_showWeapons)
                {
                    _showWeapons = false;
                }
                else
                {
                    _showWeapons = true;
                }
            }

            if (GUI.Button(new Rect(20,70,150,20), "Show Player Values"))
            {
                Debug.Log("Showing Player Values!");
                if(_showPlayer)
                {
                    _showPlayer = false;
                }
                else
                {
                    _showPlayer = true;
                }
            }
            GUI.Label(new Rect(20, 100, 150, 20), _player._fps + " FPS");

            if(_showWeapons)
            {
                GUI.Box(new Rect (Screen.width - 250,0,250,200), "Weapon Values");
                GUI.Label(new Rect(Screen.width - 230, 20, 100, 20), "Ammunition");
                //GUI.Label(new Rect(Screen.width - 230, 40, 250, 20), "Mag Size: " + _weapon._magSize);
                //GUI.Label(new Rect(Screen.width - 230, 60, 250, 20), "Ammo In Mag: " + _weapon._ammoCapacity);
                //GUI.Label(new Rect(Screen.width - 230, 80, 250, 20), "Spare Ammo: " + _weapon._spareAmmo);

                GUI.Label(new Rect(Screen.width - 230, 100, 250, 20), "Booleans");
                GUI.Label(new Rect(Screen.width - 230, 120, 250, 20), "Firing: " + _weapon._firing);
                GUI.Label(new Rect(Screen.width - 230, 140, 250, 20), "Can Fire: " + _weapon._canFire);
                GUI.Label(new Rect(Screen.width - 230, 160, 250, 20), "Can Reload: " + _weapon._canReload);
                GUI.Label(new Rect(Screen.width - 230, 180, 250, 20), "Reloading: " + _weapon._reloading);
            }

            if(_showPlayer)
            {
                GUI.Box(new Rect (Screen.width - 500,0,250,160), "Player");
                GUI.Label(new Rect(Screen.width - 480, 20, 250, 20), "XYZ: " + _player._location);
                GUI.Label(new Rect(Screen.width - 480, 40, 250, 20), "Grounded: " + _player._grounded);
                GUI.Label(new Rect(Screen.width - 480, 60, 250, 20), "Jumping: " + _player._jumping);
                GUI.Label(new Rect(Screen.width - 480, 80, 250, 20), "Crouching: " + _player._crouching);
                GUI.Label(new Rect(Screen.width - 480, 100, 250, 20), "Sprinting: " + _player._sprinting);
                GUI.Label(new Rect(Screen.width - 480, 120, 250, 20), "xRotation: " + _player._xRotation);
                GUI.Label(new Rect(Screen.width - 480, 140, 250, 20), "yRotation: " + _player._yRotation);
            }    
        }
    }
}
