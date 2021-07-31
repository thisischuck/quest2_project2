using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symbol
{
    SymbolObject sData;
    public List<Vector3> vectors, directions;
    public List<float> distances;

    // Try distance from the center instead

    public Symbol(LineRenderer r, SymbolObject s)
    {
        sData = s;
        vectors = new List<Vector3>();
        directions = new List<Vector3>();
        distances = new List<float>();
        for (int i = 0; i < r.positionCount; i++)
        {
            Vector3 position = r.GetPosition(i);
            if (i <= r.positionCount - 2)
            {
                //distances[i] = Vector3.Distance(position, r.GetPosition(i + 1));
                distances.Add(Vector3.Distance(position, Vector3.zero)); //distance from the center
                directions.Add(Vector3.Normalize(position - r.GetPosition(i + 1)));
            }
            vectors.Add(position);
        }
    }

    public void CalculateLists(SymbolObject s)
    {
        sData = s;
        directions = new List<Vector3>();
        distances = new List<float>();
        for (int i = 0; i < vectors.Count; i++)
        {
            Vector3 position = vectors[i];
            if (i <= vectors.Count - 2)
            {
                //distances[i] = Vector3.Distance(position, r.GetPosition(i + 1));
                distances.Add(Vector3.Distance(position, Vector3.zero)); //distance from the center
                directions.Add(Vector3.Normalize(position - vectors[i + 1]));
            }
        }
    }

    public float Compare(Symbol s, bool rotate)
    {
        float dI = 0, dR = 0;
        int t = sData.StepValue;
        if (vectors.Count != s.vectors.Count)
            return -1;

        float rD = CompareDistances(s);
        if (rotate)
        {
            List<Vector2> r = new List<Vector2>();
            for (int i = 0; i < t; i++)
            {
                Quaternion q = Quaternion.Euler(0, i * 360 / t, 0);
                s.directions.ForEach(v => v = q * v);
                r.Add(CompareDirections(s));
            }
            var fv = Min(r);
            dR = Mathf.Min(fv.x, fv.y) / sData.DirectionThreshold * sData.DirectionValue;
        }
        else
        {
            Vector2 r = CompareDirections(s);
            dR = Mathf.Min(r.x, r.y) / sData.DirectionThreshold * sData.DirectionValue;
        }

        dI = rD / sData.DistanceThreshold * sData.DistanceValue;
        HelperFunctions.MyDebug(
             "direction: " + dR + ", distance:" + dI
        );
        return dR + dI;
        //- Optimal Outcome is 0
    }

    Vector2 Min(List<Vector2> f)
    {
        Vector2 c = new Vector2(1, 1);
        foreach (var i in f)
        {
            c.x = Mathf.Min(c.x, i.x);
            c.y = Mathf.Min(c.y, i.y); //reflected
        }
        return c;
    }

    float CompareDistances(Symbol s)
    {
        float f = 0;
        for (int i = 0; i < s.distances.Count; i++)
        {
            f += CompareDistance(distances[i], s.distances[i]);
        }
        return f / distances.Count;
    }

    float CompareDistance(float n, float m)
    {
        return Mathf.Abs(n - m);
    }

    Vector2 CompareDirections(Symbol s)
    {
        float f = 0;
        float fR = 0;
        for (int i = 0; i < s.distances.Count; i++)
        {
            f += CompareDirection(directions[i], s.directions[i]);
            fR += CompareDirection(directions[i], Vector3.Reflect(s.directions[i], Vector3.up));
        }
        return new Vector2(f / distances.Count, fR / distances.Count);
    }

    float CompareDirection(Vector3 v, Vector3 u)
    {
        return Vector3.Angle(v, u);
    }
}
