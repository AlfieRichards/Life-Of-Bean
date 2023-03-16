using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerObjectSelect : MonoBehaviour
{
    // Start is called before the first frame update

    //Layer Mask
    [Header("Layer Options")]
    [SerializeField] private LayerMask _selectionPointLayer;

    //raycast hit output
    [HideInInspector] private RaycastHit _hit;

    //Layer Mask
    [HideInInspector] public bool _temp = true;
    [HideInInspector] public bool _holdingItem = false;
    [HideInInspector] public bool _inUi;
    [HideInInspector] public bool _paused = false;

    //Game Objects
    [Header("Game Objects")]
    [HideInInspector] public GameObject _heldItem;
    [SerializeField] public GameObject _playerModel;


    // Update is called once per frame
    void Update()
    {
        //debug draw rays with their colours
        if(_temp)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100f, Color.green);
        }

        if(!_temp)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _hit.distance, Color.blue);
        }



        //pickup item and raycast
        if(Input.GetKeyDown("e"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 100f , _selectionPointLayer))
            {
                //colour debug
                _temp = false;
                //Debug.Log("Did Hit");
                Invoke("ColourReset", 1f);

                //picks up the item
                if(_hit.transform.tag == "Moveable")
                {
                    _heldItem = _hit.transform.gameObject;
                    _holdingItem = true;
                }

                //interacts with item
                if(_hit.transform.tag == "Interactable")
                {
                    //Debug.Log("Had the right tag");
                    if(_hit.transform.gameObject.GetComponent<InteractionScript>() != null)
                    {
                        _hit.transform.gameObject.GetComponent<InteractionScript>()._interacted = true;
                        //Debug.Log("Interacted with");
                    }
                }
            }
        }

        // if(Input.GetMouseButtonDown(0))
        // {
        //     if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 10f, _selectionPointLayer))
        //     {
        //         //opens the required ui
        //         if(_hit.transform.tag == "Door")
        //         {
        //             _door.SetActive(true);
        //             Cursor.lockState = CursorLockMode.None;
        //             Cursor.visible = true;
        //             _playerModel.GetComponent<MeshRenderer>().enabled = false;
        //             _playerModel.GetComponent<playerMovement>().enabled = false;
        //             _inUi = true;
        //         }
        //     }
        // } 

        //exit menus
        // if(!_inUi)
        // {
        //     if(Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         PauseMenu menu = pause.GetComponent<PauseMenu>();
        //         if(_paused)
        //         {
        //             pause.SetActive(false);
        //             menu.Resume();
        //         }
        //         else
        //         {
        //             pause.SetActive(true);
        //             menu.Pause();
        //         }
        //     }
        // }
        // if(_inUi)
        // {
        //     if(Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         computer.SetActive(false);
        //         calendar.SetActive(false);
        //         build1.SetActive(false);
        //         //build2.SetActive(false);
        //         door.SetActive(false);

        //         Cursor.lockState = CursorLockMode.Locked;
        //         Cursor.visible = false;
        //         _playerModel.GetComponent<MeshRenderer>().enabled = true;
        //         _playerModel.GetComponent<PlayerController>().enabled = true;
        //         _inUi = false;                                                        
        //     }
        // }
    }

    //reset the debug colour
    void ColourReset(){
        _temp = true;
    }

    public void ExitUI(){
        //set ui objects active to false
        //computer.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerModel.GetComponent<MeshRenderer>().enabled = true;
        _playerModel.GetComponent<playerMovement>().enabled = true;
        _inUi = false;     
    }
}
