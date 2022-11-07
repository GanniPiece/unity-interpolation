using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolate : Interpolator
{
    public override string Name => "Linear";
    public override void Run (List<GameObject> candidate, uint num)
    {
        if (candidate.Count < 2) return;
        foreach (var obj in objs)
            Destroy(obj);
        objs.Clear();

        /*
        Transform start = candidate[0].transform;
        Transform end = candidate[1].transform;

        for (int i = 1; i <= num; i++)
        {
            GameObject obj = Instantiate(candidate[0]);
            obj.transform.position = Vector3.Lerp(start.position, end.position, (float)i / (num + 1));
            obj.transform.parent = candidate[0].transform.parent;

            var renderer = obj.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.cyan);

            objs.Add(obj);
        }
        */

        float totalLength = 0, curLength;
        float segment = 0;
        int ptr = 0;
        List<float> edges = new List<float>();
        for (int i = 0; i < candidate.Capacity - 1; i++)
        {
            var p1 = candidate[i].transform.position;
            var p2 = candidate[i + 1].transform.position;
            var edge = Vector3.Distance(p1, p2);
            edges.Add(edge);
            totalLength += edge;
        }

        segment = totalLength / (num + 1);
        curLength = segment;
        for (int i = 0; i < num; i++)
        {
            if (curLength > edges[ptr])
            {
                curLength -= edges[ptr];
                ptr++;
            }

            var p1 = candidate[ptr].transform.position;
            var p2 = candidate[ptr + 1].transform.position;

            Vector3 p = Vector3.Lerp(p1, p2, curLength / edges[ptr]);
            curLength += segment;

            GameObject obj = Instantiate(candidate[0]);
            obj.transform.position = p;
            obj.transform.parent = candidate[0].transform.parent;

            var renderer = obj.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.cyan);

            objs.Add(obj);
        }

    }
}
