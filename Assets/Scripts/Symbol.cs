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
                directions.Add(Vector3.Normalize(position - r.GetPosition(i + 1)));
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
                directions.Add(Vector3.Normalize(position - vectors[i + 1]));
            }
        }
        Name = new SymbolName(directions, sData.Language);
    }

    public bool Compare(Symbol s)
    {
        if (s.Name.value.Equals(Name.value))
            return true;
        return false;
    }

    public string CalculateName()
    {
        return "";
    }
}
