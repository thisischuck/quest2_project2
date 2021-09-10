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
    bool move;
    Vector3 target;
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
        if (move)
        {
            this.transform.position = Vector3.Lerp(transform.position, target, 0.1f);
            if (Vector3.Distance(transform.position, target) < 1)
                move = false;
        }
        //hp = scout.CalculateDamageToSelf(hp, 10, Fire, scout.Type);
    }


    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            move = true;
            Vector3 dir = other.gameObject.GetComponent<MoveFoward>().direction;
            target = transform.position + dir * enemyObject.Knockback;
            Destroy(other.gameObject);
        }
    }
}
