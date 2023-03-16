using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [Header("Target Options")]
    //_target Functionality
    [SerializeField] private bool _isTargetSwitch = false;
    [HideInInspector] private bool _movingTarget = false;

    //false is start, true is end
    [HideInInspector] private bool _targetState = false;

    [SerializeField] private float _targetSpeed = 5f;

    [SerializeField] private GameObject _target;
    [SerializeField] private Transform _targetStart;
    [SerializeField] private Transform _targetEnd;

    //interaction bool
    [HideInInspector] public bool _interacted = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(_interacted)
        {
            //Debug.Log("Interacted with");
            if(_isTargetSwitch)
            {
                _movingTarget = true;
                _interacted = false;
            }
        }

        if(_movingTarget)
        {
            float step =  _targetSpeed * Time.deltaTime; // calculate distance to move

            if(_targetState)
            {
                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(_target.transform.position, _targetStart.position) > 0.01f)
                {
                    _target.transform.position = Vector3.MoveTowards(_target.transform.position, _targetStart.position, step);
                }
                else
                {
                    _movingTarget = false;
                    _targetState = false;
                }
            }
            else
            {
                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(_target.transform.position, _targetEnd.position) > 0.01f)
                {
                    _target.transform.position = Vector3.MoveTowards(_target.transform.position, _targetEnd.position, step);
                }
                else
                {
                    _movingTarget = false;
                    _targetState = true;
                }
            }
        }  
    }
}
