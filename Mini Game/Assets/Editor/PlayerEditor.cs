using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestEditor))]
public class PlayerEditor : Editor
{
    private TestEditor testEditor;

    private bool playerInfoIsOpen = true;
    private bool playerStatsIsOpen = false;

    private void OnEnable()
    {
        testEditor = (TestEditor)target;
        if(testEditor.playerStats == null)
        {
            testEditor.playerStats = "test";
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
