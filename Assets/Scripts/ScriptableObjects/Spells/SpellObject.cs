using UnityEngine;
using HelperTypes;
using static HelperTypes.DamageTypes;
using static HelperTypes.SpellTypes;

[CreateAssetMenu(fileName = "SpellObject", menuName = "Data/Spell")]
public class SpellObject : ScriptableObject
{
    public SpellTypes SpellType;
    public DamageTypes Type;
    [Min(0)]
    public int BaseDamage;
}

