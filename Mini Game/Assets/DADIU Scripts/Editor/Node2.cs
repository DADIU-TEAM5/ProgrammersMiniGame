using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node2
{
    public Rect nodeRect;
    public GUIStyle style;
    public GUIStyle selectedNodeStyle;
    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
    private GUIStyle defaultNodeStyle;
    public Action<Node2> OnRemoveNode;
    public Rect collectiveRect;
    public bool isSelected;
    public bool isDragged;
    public BehaviourScript obj;
    public string title;
    public float moveSpeed;
    public float rotationSpeed;


    public Node2(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node2> OnClickRemoveNode)
    {
        nodeRect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
        collectiveRect = new Rect(nodeRect.position.x, nodeRect.position.y, nodeRect.size.x, nodeRect.size.y + (nodeRect.size.y / 2f));
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (collectiveRect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                        GUI.FocusControl(null);
                    }
                }

                if (e.button == 1 && isSelected && collectiveRect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    public void Drag(Vector2 delta)
    {
        nodeRect.position += delta;
        collectiveRect = new Rect(nodeRect.position.x, nodeRect.position.y, nodeRect.size.x, nodeRect.size.y + (nodeRect.size.y / 2f));
    }

    public void Draw()
    {
        inPoint.Draw();
        outPoint.Draw();

        //GUI.BeginGroup(new Rect(nodeRect.position.x, nodeRect.position.y + nodeRect.size.y / 2f, nodeRect.size.x - 10f, nodeRect.size.y), style);
        //GUILayout.BeginArea ( collectiveRect );

        //myInfo.Draw(collectiveRect, style);

        //GUI.Box ( new Rect ( 0, 0, collectiveRect.size.x - 10f, myInfo.height ), "I am box!", style );
        //GUI.Button ( new Rect ( 0, myInfo.height, 100f, myInfo.height ), "I am button!", style );

        //GUILayout.Button ( "", style );
        //myInfo.Draw ( nodeRect, style );

        //GUILayout.EndArea ();
        //GUI.EndGroup();

        Rect baseRect = new Rect(10f, 30f, nodeRect.size.x - 30f, 30f);

        var extra = 40f;
        var topOffset = 75f;

        //baseRect = new Rect(baseRect.position.x, baseRect.position.y, baseRect.size.x, baseRect.size.y);

        //EditorGUI.LabelField ( baseRect, "Weapon" );

        float marginLeft = 5f;
        float marginRight = marginLeft + 0f;

        GUI.Box(new Rect(nodeRect.x+marginLeft, nodeRect.y, nodeRect.width-marginLeft*2, nodeRect.height + topOffset*3), "", style);
          
        EditorGUI.LabelField(nodeRect, title, style);
        title = EditorGUI.TextField(new Rect(nodeRect.position.x + marginRight*4f, nodeRect.position.y + topOffset, nodeRect.width - (marginRight * 4f * 2f), nodeRect.size.y / 2f), "Title:", title);

        obj = (BehaviourScript)EditorGUI.ObjectField(new Rect(nodeRect.position.x + marginRight*4f, nodeRect.position.y + topOffset + (extra), nodeRect.width-(marginRight*4f*2f), nodeRect.size.y / 2f), "Behaviour", obj, typeof(BehaviourScript), true);

        moveSpeed = DrawStat("Move Speed:", nodeRect, marginLeft, marginRight, topOffset, 2f, 2f, extra, moveSpeed, 0f, 100f);
        rotationSpeed = DrawStat("Rotation Speed:", nodeRect, marginLeft, marginRight, topOffset, 2f, 3f, extra, rotationSpeed, 0f, 100f);

        //EditorGUI.Slider(new Rect(nodeRect.position.x + marginRight * 4f, nodeRect.position.y + topOffset + (extra*2), nodeRect.width - (marginRight * 4f * 2f), nodeRect.size.y / 2f), "Rotation Speed", 50f, 0f, 100f);
        //EditorGUI.Slider(new Rect(nodeRect.position.x + marginRight * 4f, nodeRect.position.y + topOffset + (extra*2), nodeRect.width - (marginRight * 4f * 2f), nodeRect.size.y / 2f), "Rotation Speed", 50f, 0f, 100f);
    }

    private float DrawStat(string name, Rect rect, float marginLeft, float marginRight, float topOffset, float offsetX, float offsetY, float extra, float value, float minValue, float maxValue)
    {
        return EditorGUI.Slider(new Rect(rect.position.x + marginRight * 4f, rect.position.y + topOffset + (extra*offsetY), rect.width - (marginRight * 4f * offsetX), rect.size.y / 2f), name, value, minValue, maxValue);
    }
}