using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawner : ScriptableObject
{
    public int EnemiesSpawned;
    public float SpawnDelay;
    private float spawnTime = 0;
    public GameObject enemy;
    public BehaviourScript behaviourScript;

}
