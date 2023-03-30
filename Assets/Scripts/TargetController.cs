using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform targetHolder;
    public HingeJoint targetAnchor;
    float amplification = 1;
    public float z;
    public float zDifference;
    public Vector3 anchorIntended;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zDifference = targetHolder.position.z - targetAnchor.connectedAnchor.z;
        z = targetAnchor.connectedAnchor.z + (zDifference * amplification);
        anchorIntended = new Vector3(targetAnchor.connectedAnchor.x, targetAnchor.connectedAnchor.y, z);
        targetAnchor.connectedAnchor = anchorIntended;

        targetAnchor.autoConfigureConnectedAnchor = true;
        targetAnchor.autoConfigureConnectedAnchor = false;
    }
}