using UnityEngine;

public class CharacterObject : ScriptableObject
{
    public GameObject Prefab;
    [Min(0)]
    public float Speed;
    [Min(0)]
    public int Hp;
    [Min(0)]
    public int Defense;
    public Vector3 Layers;
}
