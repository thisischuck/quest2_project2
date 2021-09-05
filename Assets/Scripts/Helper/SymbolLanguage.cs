using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HelperTypes;

[System.Serializable]
public class SymbolName
{
    public string value;

    public SymbolName(List<Vector3> dir, SymbolLanguage lang)
    {
        Debug.Log(lang);
        CalculateName(lang.Language, dir);
    }

    void CalculateName(Dictionary<Vector3, int> lang, List<Vector3> dir)
    {
        value = "";
        foreach (var d in dir)
        {
            if (lang.ContainsKey(d))
            {
                value += lang[d] + ",";
                continue;
            }

            float min = 180;
            int? c = null;
            foreach (var kv in lang)
            {
                float a = Vector3.Angle(d, kv.Key);
                if (a < min)
                {
                    HelperFunctions.MyDebug($"angle:{a} cMin:{min}\ndir:{d} key:{kv.Key}");
                    min = a;
                    c = kv.Value;
                }
            }
            value += c + ",";
        }
    }

    string Flip(Vector3 axis)
    {
        return value;
    }
}

[System.Serializable]
public class SymbolLanguage : ISerializationCallbackReceiver
{
    float pi = 3.14f;
    public Dictionary<Vector3, int> Language;
    public Dictionary<int, Vector3> LanguageRev;

    public SymbolLanguage(int stepNumber)
    {
        Language = new Dictionary<Vector3, int>();
        LanguageRev = new Dictionary<int, Vector3>();
        CreateLanguage(stepNumber);
    }

    public void ReadLanguage()
    {
        foreach (var k in Language)
        {
            Debug.Log(k);
        }
    }

    float Round(float f)
    {
        return Mathf.Round(f * 10.0f) * 0.1f;
    }

    int CreateLanguage(int stepNumber = 4)
    {
        int count = 0;
        float step = pi / stepNumber;
        for (float phi = 0; phi < 2 * pi; phi += step)
            for (float theta = 0; theta <= pi; theta += step)
            {
                float x = Mathf.Cos(phi) * Mathf.Sin(theta);
                float y = Mathf.Sin(phi) * Mathf.Sin(theta);
                float z = Mathf.Cos(theta);

                Vector3 key = new Vector3(
                    Round(x),
                    Round(y),
                    Round(z)
                );

                key.Normalize();
                Debug.DrawLine(Vector3.zero, 10 * key, Color.black, 100000);

                if (!Language.ContainsKey(key))
                {
                    Language.Add(key, count);
                    LanguageRev.Add(count, key);
                    count++;
                }
            }

        return count;
    }

    public List<Vector3> _keys = new List<Vector3>();
    public List<int> _values = new List<int>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in Language)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        Language = new Dictionary<Vector3, int>();
        LanguageRev = new Dictionary<int, Vector3>();

        for (int i = 0; i != Mathf.Min(_keys.Count, _values.Count); i++)
        {
            Language.Add(_keys[i], _values[i]);
            LanguageRev.Add(_values[i], _keys[i]);

        }
    }

    void OnGUI()
    {
        foreach (var kvp in Language)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }
}
