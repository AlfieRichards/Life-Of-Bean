using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [Header("Animation Options")]
    [SerializeField] private Animator transition;

    [Header("Locking Options")]
    [SerializeField] public bool lockable = false;
    [ConditionalHide("lockable", true)]
    [SerializeField] public bool locked = false;
    [HideInInspector] public bool visible = false;
    [HideInInspector] private bool lastState = false;

    [Header("Text Options")]
    [SerializeField] private GameObject regularText;
    [ConditionalHide("lockable", true)]
    [SerializeField] private GameObject lockedText;
    // Start is called before the first frame update
    void Update()
    {
        if(visible != lastState)
        {
            lastState = visible;
            transition.SetBool("Visible", visible);
            if(lockable)
            {
                if(locked)
                {
                    regularText.SetActive(false);
                    lockedText.SetActive(true);
                }
                else
                {
                    regularText.SetActive(true);
                    lockedText.SetActive(false);
                }
            }
        }
    }
}
