using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    SerializedProperty shouldUpdate;

    void OnEnable() 
    {
        shouldUpdate = serializedObject.FindProperty("shouldUpdate");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        shouldUpdate.boolValue = GUILayout.Button("Update", GUILayout.ExpandWidth(true), GUILayout.Width(90), GUILayout.Height(20));

        serializedObject.ApplyModifiedProperties();
    }
    
}
