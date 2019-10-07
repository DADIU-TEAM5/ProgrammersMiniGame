using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BehaviourScript))]
public class BehaviorEditor : Editor
{
    private BehaviourScript behaviourScript;

    private void OnEnable()
    {
        behaviourScript = (BehaviourScript)target;
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        //EditorGUILayout.Space();
        //Player player = (Player)target;
        if(behaviourScript.behaviour == BehaviourScript.Behaviour.Wander)
        {
            behaviourScript.rotationSpeed = EditorGUILayout.Slider("rotationSpeed", behaviourScript.rotationSpeed, 0f, 50f);
            EditorGUILayout.Space();
            behaviourScript.waitSeconds = (int)EditorGUILayout.Slider("waitSeconds", behaviourScript.waitSeconds, 0f, 50f);
        }
        EditorUtility.SetDirty(behaviourScript);

    }
}
