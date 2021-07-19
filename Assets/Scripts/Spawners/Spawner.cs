using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperTypes;

public class Spawner : MonoBehaviour
{
    public SpawnerObject sp;

    public GameObject ScoutPrefab;
    public GameObject NormalPrefab;
    public GameObject TankPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sp.spawnAction += Spawn;
    }

    Vector3 CalculatePosition()
    {
        return Vector3.zero;
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSecondsRealtime(sp.spawnRate);
        Spawn(sp.enemyType);
    }

    // Update is called once per frame
    void Spawn(EnemyTypes en)
    {
        switch (en)
        {
            case EnemyTypes.Normal:
                Instantiate(NormalPrefab, CalculatePosition(), transform.rotation, transform);
                break;
            case EnemyTypes.Scout:
                Instantiate(ScoutPrefab, CalculatePosition(), transform.rotation, transform);
                break;
            case EnemyTypes.Tank:
                Instantiate(TankPrefab, CalculatePosition(), transform.rotation, transform);
                break;
        }
    }
}
