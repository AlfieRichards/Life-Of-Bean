using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerObjectSelect : MonoBehaviour
{
    // Start is called before the first frame update

    //Layer Mask
    [Header("Layer Options")]
    [SerializeField] private LayerMask _selectionPointLayer;
    [SerializeField] private LayerMask _hoverableLayer;

    //raycast hit output
    [HideInInspector] private RaycastHit _hit;

    //Layer Mask
    [HideInInspector] public bool _temp = true;

    private FadeController lastInteracted;

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


        //check prompts
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 5f , _hoverableLayer))
        {
            if(_hit.transform.gameObject.GetComponent<FadeController>() != null)
            {
                _hit.transform.gameObject.GetComponent<FadeController>().visible = true;
                lastInteracted = _hit.transform.gameObject.GetComponent<FadeController>();
                //Debug.Log("Interacted with");
            }
        }
        else
        {
            if(lastInteracted != null)
            {
                lastInteracted.visible = false;
            }
        }



        //pickup item and raycast
        if(Input.GetKeyDown("e"))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, 5f , _selectionPointLayer))
            {
                //colour debug
                _temp = false;
                //Debug.Log("Did Hit");
                Invoke("ColourReset", 1f);

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
    }

    //reset the debug colour
    void ColourReset()
    {
        _temp = true;
    }
}
