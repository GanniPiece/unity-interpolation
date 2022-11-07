using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolator : MonoBehaviour
{
    protected List<GameObject> objs;
    public List<GameObject> Objs
    {
        get { return objs; }
    }

    public virtual void Run(List<GameObject> candidate, uint num) { }
    public virtual string Name
    {
        get { return "Interpolator"; }
    }

    void Start()
    {
        objs = new List<GameObject>();
    }

    public static Type Generate (InterpolationType type)
    {
        switch (type)
        {
            case InterpolationType.Linear:
                return Type.GetType("LinearInterpolate");
            case InterpolationType.Bezier2nd:
                return Type.GetType("Bezier2ndInterpolate");
            case InterpolationType.Bezier3rd:
                return Type.GetType("Bezier3rdInterpolate");
            default:
                return Type.GetType("LinearInterpolate");
        }
    }
}