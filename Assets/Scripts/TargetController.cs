using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Transform _targetHolder;
    [SerializeField] private HingeJoint _targetAnchor;
    [HideInInspector] private float _amplification = 1;
    [HideInInspector] private float _z;
    [HideInInspector] private float _zDifference;
    [HideInInspector] private Vector3 _anchorIntended;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _zDifference = _targetHolder.position.z - _targetAnchor.connectedAnchor.z;
        _z = _targetAnchor.connectedAnchor.z + (_zDifference * _amplification);
        _anchorIntended = new Vector3(_targetAnchor.connectedAnchor.x, _targetAnchor.connectedAnchor.y, _z);
        _targetAnchor.connectedAnchor = _anchorIntended;

        _targetAnchor.autoConfigureConnectedAnchor = true;
        _targetAnchor.autoConfigureConnectedAnchor = false;
    }
}