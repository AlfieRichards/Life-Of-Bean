using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    //need walking sprinting jumping and crouching

    //movementOptions
    [Header("Movement Options")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 10;

    //jumpOptions
    [Header("Jumping Options")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _groundCheckDistance;

    //mouseOptions
    [Header("Mouse Options")]
    [SerializeField] public float mouseSensitivity = 100f;

    //objects
    [Header("Objects")]
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _camParentBone;
    private Rigidbody _rb;

    //debuggingBools
    [HideInInspector] public bool _jumping = false;
    [HideInInspector] public bool _crouching = false;
    [HideInInspector] public bool _moving = false;
    [HideInInspector] public bool _sprinting = false;
    [HideInInspector] public bool _grounded = false;
    [HideInInspector] public Vector3 _location;
    [HideInInspector] public float _yRotation = 0f;
    [HideInInspector] public float _xRotation = 0f;
    [HideInInspector] public bool _locked = true;
    [HideInInspector] public bool _debugGUI = false;
    private bool _recentDebugOption = false;

    //frameTimes
    [HideInInspector] private float _deltaTime;
    [HideInInspector] public float _fps;

    //health
    public float _health;
    float _prevHealth;
    public float _lerp = 0f, _duration = 2f;
    public float regenMulti = 1f;
    public Material _hurtFlash;
    public Material _healthOverlay;
    private Color red = Color.red;
    private Color healthOverlay = Color.red;
    public float redLimit = 1;
    private bool _healing = false;
    private bool _flash = false;
    private float _TimeSinceDamage = 0f;
    private LevelManager levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _prevHealth = _health;
        red.a = 0f;
        healthOverlay.a = 0f;
        levelManager = FindObjectOfType<LevelManager>();

        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 150);
    }

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        _fps = 1.0f / _deltaTime;
        _fps = Mathf.Ceil(_fps);

        MouseRotation();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        GroundCheck();
        CheckInput();
        RigidbodyMovement();
        DebugCursor();
        HealthUpdate();

        _location = transform.position;
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.LeftShift)){_sprinting = true;}else{_sprinting = false;}
        if(Input.GetKey(KeyCode.LeftControl)){_crouching = true;}else{_crouching = false;}
        if(Input.GetKey(KeyCode.Space)){Jump();}
    }

    void RigidbodyMovement()
    {
        Vector3 _tempVelocity;
        float _tSpeed = _speed;

        //gets player input
        float _vertical = Input.GetAxisRaw("Vertical");
        float _horizontal = Input.GetAxisRaw("Horizontal");

        //if youre crouched and try to moves plays the uncrouch anim, cant move until uncrouched
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            _moving = true;
            if(_crouching)
            {
                _tempVelocity = new Vector3(0,_rb.velocity.y,0);
                _rb.velocity = _tempVelocity;
                ToggleCrouch();
                return;
            }
        }
        else{_moving = false;}

        //if sprinting make move faster
        if(_sprinting)
        {
            _tSpeed = _sprintSpeed;
        }

        //handle movement anims
        if(_vertical != 0)
        {
            //anim.setBool walking = true
        }
        else
        {
            if(_horizontal != 0)
            {
                //anim.setBool sideWalking = true
            }
        }

        if(!_jumping)
        {
            //sets velocity
            _tempVelocity = new Vector3();

            _tempVelocity.x = (transform.forward.x * _vertical * _tSpeed) + (transform.right.x * _horizontal * _tSpeed);
            _tempVelocity.z = (transform.forward.z * _vertical * _tSpeed) + (transform.right.z * _horizontal * _tSpeed);
            _tempVelocity.y = _rb.velocity.y;

            _rb.velocity = _tempVelocity;
        }
    }

    void MouseRotation()
    {
        //gets mouse input
        float _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -65f, 65f);

        _camParentBone.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * _mouseX);

        //Debug.Log(_mouseX + " + " + _mouseY);


        // //constrains y input
        // _yRotation = _yRotation += _mouseY;
        // _yRotation = Mathf.Clamp(_yRotation, -65f, 65f);

        // _xRotation = _xRotation += _mouseX;

        // //rotates player horizontally (y axis)
        // _playerBody.transform.rotation = Quaternion.Euler(0, _xRotation, 0);

        // //rotates player bone vertically (x axis)
        // _camParentBone.transform.rotation = Quaternion.Euler(_yRotation, 0, 0);
    }




    void Jump()
    {
        if(!_jumping)
        {
            //if youre crouched and try to jump plays the uncrouch anim, cant move until uncrouched
            if(_crouching)
            {
                ToggleCrouch();
                return;
            }

            //checks if grounded
            Debug.DrawRay(_groundPoint.position, Vector3.down * _groundCheckDistance, Color.blue);
            _grounded = Physics.Raycast(_groundPoint.position, Vector3.down, _groundCheckDistance, _ground);
            //Debug.Log(_grounded);
            if(!_grounded){return;}

            //adds the jump force
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            Debug.Log("aa");
            _jumping = true;
        }
    }

    //just checks if grounded, not particularly useful yet
    void GroundCheck()
    {
        Debug.DrawRay(_groundPoint.position, Vector3.down * _groundCheckDistance, Color.blue);
        _grounded = Physics.Raycast(_groundPoint.position, Vector3.down, _groundCheckDistance, _ground);
        //Debug.Log(_grounded);
        if(_jumping && _grounded)
        {
            _jumping = false;
            //Invoke("JumpDelay", 0.2f);
        }
    }

    void JumpDelay()
    {
        if(_grounded)
        {
            _jumping = false;
        }
    }

    void ToggleCrouch()
    {
        if(_crouching){
            //anim.setbool crouching false

            //this is temp and will be replaced by the uncrouch function below
            _crouching = false;
        }
    }

    //call this whenever the animation event for crouching is called (event at end of anim)
    void UnCrouch()
    {
        _crouching = false;
    }

    void DebugCursor()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
        {
            if(Input.GetKey("h") && _locked && !_recentDebugOption)
            {
                Debug.Log("unlocked");
                Cursor.lockState = CursorLockMode.None;
                _locked = false;
                _recentDebugOption = true;
                Invoke("ResetDebugCursor", 0.1f);
                return;
            }
            
            if(Input.GetKey("h") && !_locked && !_recentDebugOption)
            {
                Debug.Log("locked");
                Cursor.lockState = CursorLockMode.Locked;
                _locked = true;
                _recentDebugOption = true;
                Invoke("ResetDebugCursor", 0.1f);
            }

            if(Input.GetKey("j") && !_locked && !_recentDebugOption)
            {
                Debug.Log("DebugGuiOpen");
                _debugGUI = true;
                _recentDebugOption = true;
                Invoke("ResetDebugCursor", 0.1f);
                return;
            }

            if(Input.GetKey("j") && _debugGUI && !_recentDebugOption)
            {
                Debug.Log("DebugGuiClosed");
                _debugGUI = false;
                _recentDebugOption = true;
                Invoke("ResetDebugCursor", 0.1f);
            }
        }
    }

    void ResetDebugCursor()
    {
        _recentDebugOption = false;
    }

    void HealthUpdate()
    {
        if(_health <= 0)
        {
            levelManager.PlayerKilled(gameObject);
        }
        if(_health < _prevHealth)
        {
            //hurt flash
            _flash = true;
            Invoke("ResetFlash", 0.2f);

            _TimeSinceDamage = 0f;
            _healing = false;
        }
        else
        {
            if(!_healing)
            {
                _TimeSinceDamage += Time.deltaTime;
                if(_TimeSinceDamage >= 5f)
                {
                    _healing = true;
                }
            }
            else
            {
                _health += Time.deltaTime * regenMulti;
                if(_health >= 100f)
                {
                    _health = 100f;
                    _healing = false;
                    _TimeSinceDamage = 0f;
                }
            }
        }
        //_hurtFlash.color.a
        // _lerp -= Time.deltaTime / _duration;
        //             red.a = _lerp;

        //if flash make go red for length of flash reset

        //1 is red, 0 is not
        if(_flash)
        {
            if(red.a < redLimit)
            {
                _lerp += Time.deltaTime / _duration;
                red.a = _lerp;
            }
        }
        else
        {
            if(red.a > 0)
            {
                _lerp -= Time.deltaTime / _duration;
                red.a = _lerp;
            }
        }

        healthOverlay.a = 1 - (_health/100);

        _healthOverlay.color = healthOverlay;
        _hurtFlash.color = red;

        _prevHealth = _health;
    }

    void ResetFlash()
    {
        _flash = false;
    }
}
