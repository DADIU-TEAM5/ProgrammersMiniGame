using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandleTest))]
public class HandleEditor : Editor
{

    int windowID = 1234;
    Rect windowRect;
    void OnSceneGUI()
    {

        var component = target as HandleTest;

        GUIWindow();
    }


    void GUIWindow()
    {
        windowRect = GUILayout.Window(windowID, windowRect, id =>
        {
            GUI.contentColor = Color.black;
            EditorGUILayout.LabelField("Label");
            EditorGUILayout.ColorField(Color.cyan);
            EditorGUILayout.ToggleLeft("Toggle", false);
            GUILayout.Button("Button");
            GUI.DragWindow();
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