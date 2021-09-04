using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static HelperTypes.DamageTypes;

public class EnemyController : MonoBehaviour
{
    public EnemyObject enemyObject;
    private int hp;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        hp = enemyObject.Hp;
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform t)
    {
        if (agent)
            agent.destination = t.position;
        else
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");
    }

    // Update is called once per frame
    void Update()
    {
        //hp = scout.CalculateDamageToSelf(hp, 10, Fire, scout.Type);
    }
}
