using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeObject
{
    public string name;

    public AnimationClip clip;

    public int firstFrame = 1;

    public int lastFrame = 1;

    [HideInInspector]
    public Animation source;
}
