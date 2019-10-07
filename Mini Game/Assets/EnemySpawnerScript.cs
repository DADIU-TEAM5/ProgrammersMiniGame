using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public Vector2Variable planeSize;
    private enemyController ec;
    private float spawnTime = 0;


    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > enemySpawner.SpawnDelay)
        {
            SpawnEnemies();
            spawnTime = 0;

        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawner.EnemiesSpawned; i++)
        {
            float randomX = Random.Range(-planeSize.Value.x*0.1f, planeSize.Value.x * 0.1f);
            float randomZ = Random.Range(-planeSize.Value.y * 0.1f, planeSize.Value.y * 0.1f);

            Vector3 temp = new Vector3(randomX, 50, randomZ);
            Quaternion tempQ = new Quaternion(0, 0, 0, 0);

            GameObject newEnemy = GameObject.Instantiate(enemySpawner.enemy, temp, tempQ);
            ec = newEnemy.GetComponent<enemyController>();
            ec.BehaviourScript = enemySpawner.behaviourScript;

        }
    }


}
