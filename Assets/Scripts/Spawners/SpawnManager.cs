using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperTypes;


public class SpawnManager : MonoBehaviour
{
    public SpawnerObject sp;
    public Transform target;

    Queue<EnemyTypes> q;

    List<Spawner> spawnerList;
    // Start is called before the first frame update
    void Start()
    {
        q = new Queue<EnemyTypes>();
        spawnerList = new List<Spawner>(transform.GetComponentsInChildren<Spawner>(false));

        foreach (var s in spawnerList)
        {
            s.Rate = sp.spawnRate;
        }

        sp.spawnAction += Spawn;
    }

    void Spawn(EnemyTypes e)
    {
        q.Enqueue(e);
    }

    // Update is called once per frame
    void Update()
    {
        spawnerList.ForEach(s =>
        {
            if (q.Count > 0)
            {
                var p = q.Peek();
                bool a = p == EnemyTypes.Tank && s.type == Spawner.SpawnerPosition.Center;
                bool b = p != EnemyTypes.Tank && s.type != Spawner.SpawnerPosition.Center;
                if (a || b)
                {
                    if (!s.spawned)
                    {
                        var g = s.Spawn(p);
                        var z = g.GetComponent<EnemyController>();
                        z.SetTarget(target);
                        q.Dequeue();
                        s.Rate = sp.spawnRate;
                    }
                }
            }
        });
    }
}
