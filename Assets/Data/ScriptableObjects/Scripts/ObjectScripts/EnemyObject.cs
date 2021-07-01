using UnityEngine;
using HelperTypes;
using static HelperTypes.DamageTypes;
using static HelperTypes.EnemyTypes;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "Data/Enemy")]
public class EnemyObject : CharacterObject
{
    public EnemyTypes Type;
    [Min(0)]
    public int BaseDamage;
    [Range(0.1f, 5f)]
    public float SpeedModifier;


    public int CalculateDamageToSelf(int currentHp, int incoming, DamageTypes t, EnemyTypes eT)
    {
        incoming = Mathf.Abs(incoming);
        switch (t)
        {
            case Fire:
                break;
            case Water:
                break;
            case Air:
                break;
            case Earth:
                break;
        }
        switch (eT)
        {
            case Scout:
                break;
            case Tank:
                break;
            case Normal:
                break;
        }
        return Hp - (incoming - Defense);
    }
}

