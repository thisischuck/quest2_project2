using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HelperTypes.DamageTypes;

public class ScoutController : MonoBehaviour
{
    public EnemyObject scout;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = scout.Hp;
    }

    // Update is called once per frame
    void Update()
    {
        hp = scout.CalculateDamageToSelf(hp, 10, Fire, scout.Type);
    }
}
