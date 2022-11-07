using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public enum InterpolationType { Linear, Bezier2nd, Bezier3rd }

public class AutoInterpolation : MonoBehaviour
{
    [Header("Place the objects in sequential.")]
    public List<GameObject> candidate;

    public InterpolationType i_type;
    private Interpolator interpolator;

    [Range(0, 30)]
    [Header("The amount of objects to interplate.")]
    public uint num;

    // Use this for initialization
    void Start()
    {
        if (candidate == null)
            candidate = new List<GameObject>();

        CreateInterpolator();
    }

    void Update()
    {
        if (interpolator.Name != i_type.ToString())
        {
            CreateInterpolator();
        }

        if (interpolator.Objs == null || interpolator.Objs.Count == num)
            return;




        interpolator.Run(candidate, num);
    }

    private void CreateInterpolator ()
    {
        if (interpolator != null)
            Destroy(interpolator);
        var T = Interpolator.Generate(i_type);
        interpolator = gameObject.AddComponent(T) as Interpolator;
    }
}
