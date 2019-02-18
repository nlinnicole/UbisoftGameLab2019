﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if (GUILayout.Button("Generate Rooms"))
        {
            myScript.GenerateRoomTeam1(0);
            myScript.GenerateRoomTeam2(0);
        }
    }
}