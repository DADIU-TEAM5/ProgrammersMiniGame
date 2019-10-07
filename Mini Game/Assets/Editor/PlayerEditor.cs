using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private Player player;

    private bool playerStatsIsOpen = false;

    private void OnEnable()
    {
        player = (Player)target;
    }

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        
        //EditorGUILayout.Space();
        //Player player = (Player)target;
        playerStatsIsOpen = EditorGUILayout.BeginFoldoutHeaderGroup(playerStatsIsOpen,"Player Stats");
        if (playerStatsIsOpen)
        {
            player.movespeed = EditorGUILayout.Slider(player.movespeed,1f,50f);
            EditorGUILayout.Space();
            player.rotationspeed = EditorGUILayout.Slider(player.rotationspeed, 0f, 10f);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();


    }
}
