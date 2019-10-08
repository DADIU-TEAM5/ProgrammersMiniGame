using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyNodeEditor : EditorWindow
{
    private List<Node2> nodes = new List<Node2>();
    private List<Connection> connections = new List<Connection>();

    // Enemy Node Styles
    private GUIStyle enemyNodeStyle;
    private GUIStyle enemySelectedNodeStyle;

    // Player Node Styles
    private GUIStyle playerNodeStyle;
    private GUIStyle playerSelectedNodeStyle;

    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;

    private ConnectionPoint selectedInPoint;
    private ConnectionPoint selectedOutPoint;

    private Vector2 offset;
    private Vector2 drag;


    [MenuItem("Window/Node Based Editor/Enemy Node Editor")]
    private static void OpenWindow()
    {
        EnemyNodeEditor window = GetWindow<EnemyNodeEditor>();
        window.titleContent = new GUIContent("Node Based Enemy Editor");
    }

    private void OnEnable()
    {

        // Setting the Enemy's GUI styles
        enemyNodeStyle = new GUIStyle();
        enemyNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6.png") as Texture2D;
        enemyNodeStyle.border = new RectOffset(12, 12, 12, 12);
        enemyNodeStyle.alignment = TextAnchor.MiddleCenter;

        enemySelectedNodeStyle = new GUIStyle();
        enemySelectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6 on.png") as Texture2D;
        enemySelectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
        enemySelectedNodeStyle.alignment = TextAnchor.MiddleCenter;

        //Setting up the Player's GUI styles
        playerNodeStyle = new GUIStyle();
        playerNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        playerNodeStyle.alignment = TextAnchor.MiddleCenter;

        playerSelectedNodeStyle = new GUIStyle();
        playerSelectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        playerSelectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
        playerSelectedNodeStyle.alignment = TextAnchor.MiddleCenter;

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);
        inPointStyle.alignment = TextAnchor.MiddleCenter;

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
        outPointStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        // Sends a continous request to see if an event was triggered
        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.Button(new Rect(0, 0, 100, 30), "Save"))
        {
            //SaveNodes();
        }

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes()
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
    }

    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Enemy Type"), false, () => OnClickAddNode(mousePosition, "enemy"));
        genericMenu.AddItem(new GUIContent("Add Player Type"), false, () => OnClickAddNode(mousePosition, "player"));
        //genericMenu.AddItem ( new GUIContent ( "Add node" ), false, () => OnClickAddNode ( mousePosition ));
        genericMenu.ShowAsContext();
    }

    private void OnClickAddNode(Vector2 mousePosition, string type)
    {
        if (nodes == null)
        {
            nodes = new List<Node2>();
        }

        if (type == "enemy")
        {
            nodes.Add(new Node2(mousePosition, 350, 50, enemyNodeStyle, enemySelectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
        }
        else if (type == "player")
        {
            nodes.Add(new Node2(mousePosition, 350, 50, playerNodeStyle, playerSelectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
        }

    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }

        connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    private void OnClickRemoveNode(Node2 node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                connections.Remove(connectionsToRemove[i]);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);

        Repaint();
    }


    // This sections processes the different events, like clicking the mouse or pressing the delete button
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {

           
            case EventType.MouseDown:
                /*UNNECESSARY
                    if (e.button == 0)
                    {
                        //ClearConnectionSelection();
                    }
                */

                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;


            /* UNNCECESSARY
            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    //OnDrag(e.delta);
                }
                break;
                */

            case EventType.KeyDown:
                if (e.keyCode == KeyCode.Delete)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[i].isSelected)
                        {
                            OnClickRemoveNode(nodes[i]);
                        }
                    }
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        connections.Remove(connection);
    }
}
