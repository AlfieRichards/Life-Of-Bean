using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    //need walking sprinting jumping and crouching

    //movementOptions
    [Header("Movement Options")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 10;
    [SerializeField] private float _crouchSpeed = 0;

    //jumpOptions
    [Header("Jumping Options")]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpPower;

    //mouseOptions
    [Header("Mouse Options")]
    [SerializeField] public float mouseSensitivity = 100f;

    //objects
    [Header("Objects")]
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _camParentBone;
    [HideInInspector] private Rigidbody _rb;

    //debuggingBools
    [HideInInspector] public bool _crouching = false;
    [HideInInspector] public bool _sprinting = false;
    [HideInInspector] public bool _grounded;
    [HideInInspector] public Vector3 _location;
    [HideInInspector] public float _yRotation = 0f;
    [HideInInspector] public float _xRotation = 0f;
    [HideInInspector] public bool _locked = true;
    [HideInInspector] public bool _debugGUI = false;
    private bool _recentDebugOption = false;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        MouseRotation();
        CheckInput();
        RigidbodyMovement();
        DebugCursor();

        _location = transform.position;
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.LeftShift)){_sprinting = true;}else{_sprinting = false;}
        if(Input.GetKey(KeyCode.LeftControl)){_crouching = true;}else{_crouching = false;}
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
            if(_crouching)
            {
                _tempVelocity = new Vector3(0,_rb.velocity.y,0);
                _rb.velocity = _tempVelocity;
                ToggleCrouch();
                return;
            }
        }

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

        //sets velocity
        _tempVelocity = (transform.forward * _vertical * _tSpeed) + (transform.right * _horizontal * _tSpeed);
        _rb.velocity = _tempVelocity;
    }

    void MouseRotation()
    {
        //gets mouse input
        float _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float _mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //stops rot from getting infinitely higher and going out of the limits of a float
        if(_xRotation > 360 || _xRotation < -360)
        {
            _xRotation = 0;
        }

        //constrains y input
        _yRotation = _yRotation += _mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -75f, 75f);

        _xRotation = _xRotation += _mouseX;

        //rotates player horizontally (y axis)
        _playerBody.transform.rotation = Quaternion.Euler(0, _xRotation, 0);

        //rotates player bone vertically (x axis)
        _camParentBone.transform.rotation = Quaternion.Euler(_yRotation, _xRotation, 0);
    }




    void Jump()
    {
        
    }

    void ToggleCrouch()
    {
        if(_crouching){
            //anim.setbool crouching false
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
}
