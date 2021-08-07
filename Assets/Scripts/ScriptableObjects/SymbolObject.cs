using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolObject", menuName = "Data/Symbol")]
public class SymbolObject : ScriptableObject
{
    public SymbolLanguage Language;

    public List<Symbol> symbols;

    [Range(0, 1)]
    public float DistanceValue;
    [Range(0, 1)]
    public float DirectionValue;

    public int StepValue;

    public float DistanceThreshold;
    [Tooltip("Angle in Degrees")]
    public float DirectionThreshold;

    public bool SaveMe = false;
}
