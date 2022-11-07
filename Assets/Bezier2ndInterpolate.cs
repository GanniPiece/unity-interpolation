using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier2ndInterpolate : Interpolator
{
    public override string Name => "Bezier2nd";

    public override void Run (List<GameObject> candidate, uint num)
    {
        if (candidate.Count < 2) return;
        if (candidate.Count == 2)
        {
            GameObject obj = Instantiate(candidate[0]);
            obj.transform.position = Vector3.Lerp(candidate[0].transform.position,
                                             candidate[1].transform.position,
                                             1 / 2);
            candidate.Insert(1, obj);
        }

        foreach (var obj in objs)
            Destroy(obj);
        objs.Clear();

        for (int i = 1; i <= num; i++)
        {
            float ratio = (float)i / (num + 1);
            for (int j = 0; j < candidate.Count - 2; j++)
            {
                Vector3 p1 = candidate[j].transform.position;
                Vector3 p2 = candidate[j+1].transform.position;
                Vector3 p3 = candidate[j+2].transform.position;

                Vector3 p12 = Vector3.Lerp(p1, p2, ratio);
                Vector3 p23 = Vector3.Lerp(p2, p3, ratio);

                Vector3 p = Vector3.Lerp(p12, p23, ratio);

                GameObject obj = Instantiate(candidate[0]);
                obj.transform.position = p;
                obj.transform.parent = candidate[0].transform.parent;

                var renderer = obj.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", Color.cyan);

                objs.Add(obj);
            }
        }
    }
}
