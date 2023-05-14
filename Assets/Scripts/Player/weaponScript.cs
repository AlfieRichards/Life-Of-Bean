using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    //toggleOptions
    [Header("Toggle Options")]
    public bool _showVisualsOptions;
    public bool _showEffectsOptions;
    public bool _showCameraOptions;
    public bool _showAmmunitionOptions;
    public bool _showWeaponOptions;
    public bool _showEnemyOptions;

    //animation
    [ConditionalHide("_showVisualsOptions", true)]
    [Header("Animation Options")]
    public bool _hasAnimations;
    [ConditionalHide("_showVisualsOptions", true)]
    [SerializeField] public AnimationManager animController;
    private bool walkAnimPlaying = false;
    private bool sprintAnimPlaying = false;
    private bool idleAnimPlaying = false;

    //bullet hole
    [Header("BulletHole Options")]
    [ConditionalHide("_showEffectsOptions", true)]
    [SerializeField] private GameObject _bulletHole; //hole prefab

    //camera location
    [Header("Player Camera")]
    [ConditionalHide("_showCameraOptions", true)]
    [SerializeField] private playerMovement _movementScript;
    [ConditionalHide("_showCameraOptions", true)]
    [SerializeField] private Transform _camera;
    [ConditionalHide("_showCameraOptions", true)]
    [SerializeField] private Transform _spineEquivelent;

    //ammo volumes
    [Header("Ammunition Options")]
    [ConditionalHide("_showAmmunitionOptions", true)]
    [SerializeField] public int _magSize; //total mag size
    [ConditionalHide("_showAmmunitionOptions", true)]
    [SerializeField] public int _ammoCapacity; //ammo in mag
    [ConditionalHide("_showAmmunitionOptions", true)]
    [SerializeField] public int _spareAmmo; //spare ammo to reload with

    //weapon properties
    [Header("Weapon Options")]
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private float _fireRate; //in rpm
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private int _fireMode; //0 = semi, 1 = automatic
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private int _range;
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private int _damage;
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private int _reloadTime; //reload anim clip time
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private int _adsReloadTime; //ads reload anim clip time

    //gun fire location
    [ConditionalHide("_showWeaponOptions", true)]
    [SerializeField] private Transform _firePoint;
    
    //enemy options
    [Header("Enemy Options")]
    //enemy layers
    [ConditionalHide("_showEnemyOptions", true)]
    [SerializeField] private LayerMask _NotDamagable;



    //firing options
    [HideInInspector] public bool _firing = false;
    [HideInInspector] public bool _aiming = false;
    [HideInInspector] public bool _tempAiming = false;
    [HideInInspector] public bool _canFire = true;
    private float _shotDelay;


    //reloading options
    [HideInInspector] public bool _canReload = true;
    [HideInInspector] public bool _reloading = false;

    [HideInInspector] public AudioController audioController;

    bool dryFired = false;


    void Start()
    {
        //turns the fire rate from rpm to rps and then into time between shot
        _shotDelay = 1 / (_fireRate/60);
        Debug.Log(_shotDelay);

        //sets mag size
        _ammoCapacity = _magSize;

        //subtracts this mag from spare ammo
        Debug.Log(_spareAmmo);

        //sets audio controller
        audioController = GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        //shooting

        //if left click
        if (Input.GetButton("Fire1"))
        {
            //if automatic
            if(_fireMode == 1)
            {
                //starts firing
                FireWeapon();
                _firing = true;
            }

            //if semi automatic
            else
            {
                if(!_firing)
                {
                    //starts firing
                    FireWeapon();
                    _firing = true;
                }
            }
        }

        //if not left clicking
        if (Input.GetButtonUp("Fire1"))
        {
            //stops firing
            _firing = false;
        }


        //aiming

        //if right click
        if (Input.GetButtonDown("Fire2") && !_reloading)
        {
            _aiming = true;

            //animations

            //anim would play here
            AnimHandler("ads in");
        }

        //if not right clicking
        if (Input.GetButtonUp("Fire2") && !_reloading && _aiming)
        {
            //stops firing
            _aiming = false;

            //animations

            //anim would play here
            AnimHandler("ads out");
        }


        //debug rays

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
            Debug.DrawRay(_camera.position, transform.TransformDirection(Vector3.forward)* 100f, Color.black);
        }


        //reloading

        //if press r
        if(Input.GetKeyDown("r") && !_reloading)
        {
            //checks there is enough ammo to reload
            if(_spareAmmo > 0 && !_reloading)
            {
                //reloads the weapon after the reload time
                _reloading = true;
                _canReload = true;


                //animations

                //anim would play here
                if(!_aiming){AnimHandler("reload");}
                else{AnimHandler("ads reload");}

                if(!_aiming){Invoke("Reload", _reloadTime);}
                else{Invoke("Reload", _adsReloadTime); _tempAiming = true;}
                dryFired = false;
            }
            else
            {
                //if you have no ammo
                Debug.Log("No ammo");
                _reloading = false;
                _canReload = false;
                

                //animations

                //anim would play here
                if(!_aiming){AnimHandler("reload no spare");}
                else{AnimHandler("ads reload no spare");}
            }
        }


        //inspecting

        if(Input.GetKeyDown("f"))
        {
            if(!_hasAnimations){return;}
            AnimHandler("inspect");
        }


        //animations

        //idle animation
        if(!idleAnimPlaying && !_aiming && !_firing && !_reloading && !_movementScript._moving)
        {
            if(!_hasAnimations){return;}
            animController.PlayAnim("idle");
            idleAnimPlaying = true;
        }

        if(!animController.animSource.isPlaying)
        {
            if(!_hasAnimations){return;}
            walkAnimPlaying = false;
            sprintAnimPlaying = false;
            idleAnimPlaying = false;
        }

        if(!walkAnimPlaying && !sprintAnimPlaying && !_aiming && !_firing && !_reloading && _movementScript._moving)
        {
            if(!_hasAnimations){return;}
            animController.PlayAnim("walk");
            walkAnimPlaying = true;
        }

        if(!sprintAnimPlaying && !_aiming && !_firing && !_reloading && _movementScript._moving && _movementScript._sprinting)
        {
            if(!_hasAnimations){return;}
            animController.PlayAnim("sprint loop");
            sprintAnimPlaying = true;
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
                _canReload = false; //sets the begun can reload bool to false
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

        if (!Input.GetButton("Fire2") && _tempAiming){AnimHandler("ads out"); _tempAiming = false;}
    }

    void FireWeapon()
    {   
        //if mag empty or shot delay ongoing dont shoot
        if(_ammoCapacity < 1 || !_canFire || _reloading)
        {
            if(_ammoCapacity < 1 && _canFire && !_reloading && !dryFired)
            {
                AnimHandler("dryfire");
                dryFired = true;
            }
            return;
        }

        //adds correct delay inbetween shots
        _canFire = false;
        Invoke("ShotDelay", _shotDelay);

        //animations

        //anim would play here
        if(!_aiming){AnimHandler("fire");}
        else{AnimHandler("ads fire");}


        //Physics.Raycast(_firePoint.position, transform.TransformDirection(Vector3.forward), out hit, _range, _damageable


        //fires primary raycast
        RaycastHit cameraHit;
        if (Physics.Raycast(_camera.position, transform.TransformDirection(Vector3.forward), out cameraHit, _range))
        {
            Debug.Log(Physics.Linecast(_firePoint.position, cameraHit.point));
            Debug.DrawLine(_firePoint.position, cameraHit.point, Color.white, 5f, true);
            //check to see if the gun can hit it
            if (Physics.Linecast(_firePoint.position, cameraHit.point, _NotDamagable))
            {
                Debug.Log("Weapon Obstructed");
                RaycastHit weaponHit;

                if(Physics.Raycast(_firePoint.position, transform.TransformDirection(Vector3.forward), out weaponHit, _range))
                {
                    //puts hole in targets
                    if(weaponHit.transform.tag == "Penetrable")
                    {
                        GameObject hole = Instantiate(_bulletHole, weaponHit.point, Quaternion.FromToRotation(transform.up, weaponHit.normal));
                        hole.transform.parent = weaponHit.transform;
                    }
                    if(weaponHit.transform.tag == "Enemy")
                    {
                        weaponHit.transform.gameObject.GetComponent<EnemyAi>().health -= _damage;
                    }
                    //everything but the targets
                    else
                    {
                        //deals damage to hit object
                        //hit.takeDamage(damage);
                    }

                    //Debug code
                    Debug.Log("Did Hit");
                }
            }
            else
            {
                Debug.Log("Weapon UnObstructed");
                //puts hole in targets
                if(cameraHit.transform.tag == "Penetrable")
                {
                    GameObject hole = Instantiate(_bulletHole, cameraHit.point, Quaternion.FromToRotation(transform.up, cameraHit.normal));
                    hole.transform.parent = cameraHit.transform;
                }
                if(cameraHit.transform.tag == "Enemy")
                {
                    cameraHit.transform.gameObject.GetComponent<EnemyAi>().health -= _damage;
                }
                //everything but the targets
                else
                {
                    //deals damage to hit object
                    //hit.takeDamage(damage);
                }

                //Debug code
                Debug.Log("Did Hit");
            }
        }

        //subtracts from ammo
        _ammoCapacity -= 1;


        //debug code to show the weapon has been fired
        Debug.Log("Firing");
    }

    //used to delay the shots according to fire rate
    void ShotDelay()
    {
        //allows firing after the set delay on invoke
        _canFire = true;
    }


    void AnimHandler(string animReq)
    {
        if(!_hasAnimations){return;}
        switch (animReq)
        {
            case "equip":
                //audioController.PlayOneShotSound("equip");
                animController.PlayAnim("equip");
                break;
            case "fire":
                audioController.PlayOneShotSound("fire");
                animController.PlayAnim("fire");
                break;
            case "reload":
                audioController.PlayOneShotSound("reload");
                animController.PlayAnim("reload");
                break;
            case "reload no spare":
                //audioController.PlayOneShotSound("reload no spare");
                animController.PlayAnim("reload no spare");
                break;
            case "reload spare":
                // audioController.PlayOneShotSound("reload spare");
                // animController.PlayAnim("reload spare");
                break;
            case "idle":
                //audioController.PlayOneShotSound("idle");
                animController.PlayAnim("idle");
                break;
            case "walk":
                //audioController.PlayOneShotSound("walk");
                animController.PlayAnim("walk");
                break;
            case "sprint entry":
                //audioController.PlayOneShotSound("sprint entry");
                animController.PlayAnim("sprint entry");
                break;
            case "sprint loop":
                //audioController.PlayOneShotSound("sprint loop");
                animController.PlayAnim("sprint loop");
                break;
            case "sprint exit":
                //audioController.PlayOneShotSound("sprint exit");
                animController.PlayAnim("sprint exit");
                break;
            case "ads in":
                //audioController.PlayOneShotSound("ads in");
                animController.PlayAnim("ads in");
                break;
            case "ads fire":
                audioController.PlayOneShotSound("fire");
                animController.PlayAnim("ads fire");
                break;
            case "ads idle":
                //audioController.PlayOneShotSound("ads idle");
                animController.PlayAnim("ads idle");
                break;
            case "ads reload":
                //audioController.PlayOneShotSound("ads reload");
                animController.PlayAnim("ads reload");
                break;
            case "ads dryfire":
                // audioController.PlayOneShotSound("ads dryfire");
                // animController.PlayAnim("ads dryfire");
                break;
            case "ads reload no spare":
                //audioController.PlayOneShotSound("ads reload no spare");
                animController.PlayAnim("ads reload no spare");
                break;
            case "ads reload spare":
                // audioController.PlayOneShotSound("ads reload spare");
                // animController.PlayAnim("ads reload spare");
                break;
            case "ads out":
                //audioController.PlayOneShotSound("ads out");
                animController.PlayAnim("ads out");
                break;
            case "inspect":
                //audioController.PlayOneShotSound("inspect");
                animController.PlayAnim("inspect");
                break;
            case "dryfire":
                audioController.PlayOneShotSound("dryfire");
                // animController.PlayAnim("dryfire");
                break;                       
            default:
                // If the input string does not match any animation name, print an error message
                Debug.LogError("Animation not found: " + animReq);
                break;
        }
    }
}
