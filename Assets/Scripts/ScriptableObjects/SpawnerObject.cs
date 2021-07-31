using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperTypes;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName = "SpawnerObject", menuName = "Data/Spawner")]
public class SpawnerObject : ScriptableObject
{
    public float spawnRate;
    public EnemyTypes enemyType;
    public UnityAction<EnemyTypes> spawnAction;

    void OnEnable()
    {
    }

    public void SpawnActionInvoke()
    {
        if (spawnAction != null)
            spawnAction.Invoke(enemyType);
    }

    void OnInspectorGUI()
    {
        if (GUILayout.Button("Spawn"))
        {
            SpawnActionInvoke();
            Debug.Log("Pressed");
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SpawnerObject))]
public class SpawnerObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var t = (SpawnerObject)target;

        if (GUILayout.Button("Spawn"))
        {
            t.SpawnActionInvoke();
            Debug.Log("Pressed");
        }

    }
}

#endif
