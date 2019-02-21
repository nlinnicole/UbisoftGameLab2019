using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RopeGenerator))]
public class RopeGenButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RopeGenerator myScript = (RopeGenerator)target;
        if (GUILayout.Button("Generate Rope"))
        {
            myScript.generate();
        }
    }
}