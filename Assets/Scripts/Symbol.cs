using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Symbol
{
    SymbolObject sData;
    public SymbolName Name;
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
                directions.Add(Vector3.Normalize(r.GetPosition(i + 1) - position));
            }
            vectors.Add(position);
        }
        Name = new SymbolName(directions, sData.Language);
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
                directions.Add(Vector3.Normalize(vectors[i + 1] - position));
            }
        }
        Name = new SymbolName(directions, sData.Language);
    }

    public float Compare(Symbol s)
    {
        if (s.Name.value.Equals(Name.value))
            return 1;

        float d = 0;

        //need to check if it's close to another thing
        if (s.Name.value.Length == Name.value.Length)
        {
            float keyValue = 1 / Name.value.Length;
            for (int i = 0; i < Name.value.Length; i++)
            {
                int a = int.Parse(s.Name.value.Split(',')[i]);
                int b = int.Parse(Name.value.Split(',')[i]);
                if (a == b)
                {
                    d += keyValue;
                }
                else
                {
                    float xyzValue = keyValue / 3;

                    Vector3 result = new Vector3(
                        Mathf.Abs(s.directions[i].x - directions[i].x),
                        Mathf.Abs(s.directions[i].y - directions[i].y),
                        Mathf.Abs(s.directions[i].z - directions[i].z)
                    );


                    d += xyzValue * f(result.x);
                    d += xyzValue * f(result.y);
                    d += xyzValue * f(result.z);
                }
            }
        }
        return d;
    }

    float f(float v)
    {
        return -(v / 2) + 1;
    }

    public string CalculateName()
    {
        return "";
    }
}
