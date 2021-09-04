using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperTypes;

public class Spawner : MonoBehaviour
{
    public enum SpawnerPosition
    {
        Center,
        Sides
    }
    public SpawnerPosition type;
    float rate;
    public bool spawned;
    [Space]
    public GameObject Scout;
    public GameObject Zerg;
    public GameObject Tank;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (spawned)
        {
            if (rate < 0)
                spawned = false;
            else
                rate -= Time.deltaTime;
        }
    }

    public float Rate
    {
        get { return rate; }
        set { rate = value; }
    }

    // Update is called once per frame
    public GameObject Spawn(EnemyTypes en)
    {
        spawned = true;
        GameObject g = null;
        switch (en)
        {
            case EnemyTypes.Normal:
                g = Instantiate(Zerg, transform.position, transform.rotation, transform);
                break;
            case EnemyTypes.Scout:
                g = Instantiate(Scout, transform.position, transform.rotation, transform);
                break;
            case EnemyTypes.Tank:
                g = Instantiate(Tank, transform.position, transform.rotation, transform);
                break;
        }
        return g;
    }
}
