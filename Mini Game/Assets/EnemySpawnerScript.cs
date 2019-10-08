using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public  GameObject[] enemies;
    public  float[] delays;

    //public EnemySpawner enemySpawner;
    public Vector2Variable planeSize;
    private enemyController ec;
    private float[] spawnTimes;



    private void Start()
    {
        print("number of enemies " + enemies.Length);
        spawnTimes = new float[enemies.Length];
    }

    void Update()
    {

        for (int i = 0; i < spawnTimes.Length; i++)
        {
            spawnTimes[i] += Time.deltaTime;

            if (spawnTimes[i] > delays[i])
            {
                SpawnEnemies(i);
                spawnTimes[i] = 0;

            }

        }
        

        
    }

    void SpawnEnemies(int enemNumb)
    {




        
            float randomX = Random.Range(-planeSize.Value.x*0.1f, planeSize.Value.x * 0.1f);
            float randomZ = Random.Range(-planeSize.Value.y * 0.1f, planeSize.Value.y * 0.1f);

            Vector3 temp = new Vector3(randomX, 50, randomZ);
            Quaternion tempQ = new Quaternion(0, 0, 0, 0);

            GameObject newEnemy = GameObject.Instantiate(enemies[enemNumb], temp, tempQ);
            ec = newEnemy.GetComponent<enemyController>();
            

        
    }


}
