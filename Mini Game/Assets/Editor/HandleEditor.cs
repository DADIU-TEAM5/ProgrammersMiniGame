using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandleTest))]
public class HandleEditor : Editor
{
    int windowID = 1234;
    Rect windowRect;
    HandleTest component;
    Color matColor = Color.white;
    

    void OnSceneGUI()
    {

        component = target as HandleTest;

        GUIWindow();
    }


    void GUIWindow()
    {
        windowRect = GUILayout.Window(windowID, windowRect, id =>
        {
            GUI.contentColor = Color.black;
            EditorGUILayout.LabelField("Label");
            matColor = EditorGUILayout.ColorField("New Color", matColor);
            if(GUILayout.Button("Change Color"))
                component.ObjectColor = matColor;
        }, "Window", GUILayout.Width(100));
    }

}

    public class HandleGUIScope : GUI.Scope
    {
        public HandleGUIScope()
        {
            Handles.BeginGUI();
        }

        protected override void CloseScope()
        {
            Handles.EndGUI();
        }
    }