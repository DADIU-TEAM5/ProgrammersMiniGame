using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private Player testEditor;

    private bool playerInfoIsOpen = true;
    private bool playerStatsIsOpen = false;

    private void OnEnable()
    {
        testEditor = (Player)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
