using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shot
{
    public float upForce;
    public float hitForce;
    public float reflection;
    public float drag;
}

public class ShotManager : MonoBehaviour
{
    public Shot upSwing;
    public Shot chop;
    public Shot serve;
}
