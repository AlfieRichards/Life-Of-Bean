using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    //gun fire location
    [Header("Fire Location")]
    [SerializeField] private Transform _firePoint;

    //ammo volumes
    [Header("Ammunition Options")]
    [SerializeField] public int _magSize; //total mag size
    [SerializeField] public int _ammoCapacity; //ammo in mag
    [SerializeField] public int _spareAmmo; //spare ammo to reload with

    //weapon properties
    [Header("Weapon Options")]
    [SerializeField] private float _fireRate; //in rpm
    [SerializeField] private int _fireMode; //0 = semi, 1 = automatic
    [SerializeField] private int _range;
    [SerializeField] private int _damage;
    [SerializeField] private int _reloadTime; //reload anim clip time
    
    //enemy options
    [Header("Enemy Options")]
    //enemy layers
    [SerializeField] private LayerMask _damageable;



    //firing options
    [HideInInspector] public bool _firing = false;
    [HideInInspector] public bool _canFire = true;
    private float _shotDelay;


    //reloading options
    [HideInInspector] public bool _canReload = true;
    [HideInInspector] public bool _reloading = false;


    void Start()
    {
        //turns the fire rate from rpm to rps and then into time between shot
        _shotDelay = 1 / (_fireRate/60);
        Debug.Log(_shotDelay);

        //sets mag size
        _ammoCapacity = _magSize;

        //subtracts this mag from spare ammo
        Debug.Log(_spareAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        //if left click
        if (Input.GetButton("Fire1"))
        {
            //if automatic
            if(_fireMode == 1)
            {
                //starts firing
                _firing = true;
            }

            //if semi automatic
            else
            {
                if(!_firing)
                {
                    //starts firing
                    _firing = true;
                }
            }
        }

        //if not clicking
        if (Input.GetButtonUp("Fire1"))
        {
            //stops firing
            _firing = false;
        }

        //if weapon is shooting
        if(_firing && !_reloading)
        {
            //debug code to show where weapon is pointed
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.red);        
        }

        //if weapon is reloading
        if(_reloading)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.blue);
        }

        //if weapon is doing nothing
        if(!_firing && !_reloading)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.green);
        }

        //if press r
        if(Input.GetKeyDown("r"))
        {
            //checks there is enough ammo to reload
            if(_spareAmmo > 0)
            {
                //reloads the weapon after the reload time
                _reloading = true;
                //anim would play here
                Invoke("Reload", _reloadTime);
            }
            else
            {
                //if you have no ammo
                Debug.Log("No ammo");
                _reloading = false;
                //play a sound or anim here for no ammo
            }
        }
    }


    //called to reload the weapon
    void Reload()
    {

        //if ammo capacity is less than 30 and there is spare ammo, do mag size - current ammo

        if(_ammoCapacity < _magSize && _spareAmmo != 0)
        {
            //works out how much more can be added to the mag
            int tAmmo = _magSize - _ammoCapacity;
            Debug.Log(tAmmo);

            //if you have enough ammo left to reload but not a full mag
            if(_spareAmmo < tAmmo)
            {
                _ammoCapacity += _spareAmmo; //adds remaining spare ammo into mag
                _spareAmmo = 0; //sets spare to empty
                Debug.Log("emptied spare ammo");
            }

            //if you have enough ammo left to reload from empty to full
            else
            {
                _ammoCapacity += tAmmo; //fills the mag
                _spareAmmo -= tAmmo; //subtracts the ammo used from spare ammo
                Debug.Log("used " + tAmmo + " from spare ammo");
            }
            _reloading = false;
        }

        //if there is 0 ammo left to reload with
        else
        {
            _reloading = false;
            //do some kinda out of ammo anim or sound here. Will also be played if you dont need to reload
        }
    }

    //used to delay the shots according to fire rate
    void ShotDelay()
    {
        //allows firing after the set delay on invoke
        _canFire = true;
    }
}