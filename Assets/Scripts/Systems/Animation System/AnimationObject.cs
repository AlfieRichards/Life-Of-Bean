using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationObject
{
    public string name;

    public AnimationClip clip;

    [HideInInspector]
    public Animation source;
}
