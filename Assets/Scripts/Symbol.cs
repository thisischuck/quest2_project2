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


    //this function is fucked
    public float Compare(Symbol s)
    {
        if (s.Name.value.Equals(Name.value))
            return 1;

        float d = 0;

        //need to check if it's close to another thing
        if (s.Name.value.Length == Name.value.Length)
        {
            float l = Mathf.Max(s.Name.value.Length, Name.value.Length);
            float min = Mathf.Min(s.Name.value.Length, Name.value.Length);

            var sname = s.Name.value.Split(',');
            var name = Name.value.Split(',');

            float keyValue = 1 / l;
            for (int i = 0; i < min; i++)
            {
                int a = int.Parse(sname[i]);
                int b = int.Parse(name[i]);
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


                    d += xyzValue * fun(result.x);
                    d += xyzValue * fun(result.y);
                    d += xyzValue * fun(result.z);
                }
            }
        }
        return d;
    }

    float fun(float v)
    {
        return -(v / 2) + 1;
    }

    public string CalculateName()
    {
        return "";
    }
}
