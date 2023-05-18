using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Transform _targetHolder;
    [SerializeField] private HingeJoint _targetAnchor;
    [HideInInspector] private float _amplification = 1;
    [HideInInspector] private Vector3 _anchorIntended;
    Rigidbody rb;
    public GameObject topHinge;

    // Start is called before the first frame update
    void Start()
    {
     rb = topHinge.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float _zDifference = _targetHolder.position.z - topHinge.transform.position.z;
        float _z = topHinge.transform.position.z + (_zDifference * _amplification);
        Vector3 _destination = new Vector3(topHinge.transform.position.x, topHinge.transform.position.y, _z);

        rb.MovePosition(_destination);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     _zDifference = _targetHolder.position.z - _targetAnchor.connectedAnchor.z;
    //     _z = _targetAnchor.connectedAnchor.z + (_zDifference * _amplification);
    //     _anchorIntended = new Vector3(_targetAnchor.connectedAnchor.x, _targetAnchor.connectedAnchor.y, _z);
    //     _targetAnchor.connectedAnchor = _anchorIntended;

    //     Debug.Log(_targetAnchor.connectedAnchor);
    //     //Debug.Log(_anchorIntended);

    //     _targetAnchor.autoConfigureConnectedAnchor = true;
    //     _targetAnchor.autoConfigureConnectedAnchor = false;
    // }
}