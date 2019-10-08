using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpanwerSetup : EditorWindow
{
    
    public static int sizeOfEnemyList;
    public static GameObject[] arrayOfEnemies;
    public static float[] delays;


    Vector2 scroll1;
    bool showObjs;

    EnemySpawnerScript enemyspawner;


    private void OnEnable()
    {
        Debug.Log("i was ran");

        enemyspawner = FindObjectOfType<EnemySpawnerScript>();
        if(enemyspawner == null)
        {
           GameObject temp =   new GameObject();

            temp.name = "EnemySpawner";
            temp.AddComponent<EnemySpawnerScript>();
            enemyspawner = temp.GetComponent<EnemySpawnerScript>();
        }

        sizeOfEnemyList = enemyspawner.enemies.Length;
        arrayOfEnemies = enemyspawner.enemies;
        delays = enemyspawner.delays;
        
    }

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Enemy Spawner")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(EnemySpanwerSetup));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);


        sizeOfEnemyList = EditorGUILayout.IntField("Enemy Types", sizeOfEnemyList);

        if(arrayOfEnemies == null)
            arrayOfEnemies = new GameObject[sizeOfEnemyList];
        if (delays == null)
            delays = new float[sizeOfEnemyList];

        showObjs = EditorGUILayout.Foldout(showObjs,"Enemies");
        if (showObjs)
        {
            scroll1 = EditorGUILayout.BeginScrollView(scroll1, GUILayout.Width(300), GUILayout.Height(400));
            
            
            if (sizeOfEnemyList != arrayOfEnemies.Length)
            {
                GameObject[] temp = arrayOfEnemies;
                float[] floattemp = delays;
                arrayOfEnemies = new GameObject[sizeOfEnemyList];
                delays = new float[sizeOfEnemyList];


                for (int i = 0; i < arrayOfEnemies.Length; i++)
                {
                    delays[i] = 1;
                    if (i < temp.Length)
                    {
                        arrayOfEnemies[i] = temp[i];
                        delays[i] = floattemp[i];

                    }
                    
                }

            }



            GUILayout.Label("Spawn Delay, Enemy type");
            
            for (int i = 0; i < sizeOfEnemyList; i++)
            {
                EditorGUILayout.BeginHorizontal();
                delays[i] = EditorGUILayout.FloatField(delays[i]);
                arrayOfEnemies[i] = (GameObject)EditorGUILayout.ObjectField(arrayOfEnemies[i], typeof(GameObject), false);

                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndScrollView();


            if(enemyspawner.enemies != arrayOfEnemies || enemyspawner.delays != delays)
            {
                enemyspawner.enemies = arrayOfEnemies;
                enemyspawner.delays = delays;
            }
           

            
        }

       

        
    }
}
