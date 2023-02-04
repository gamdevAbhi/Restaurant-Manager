using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Employee), true)]
public class EmployeeEditor : Editor
{
    SerializedProperty employeeName;
    SerializedProperty age;
    SerializedProperty gender;

    SerializedProperty speed;
    SerializedProperty cleaning;
    SerializedProperty cooking;
    SerializedProperty service;

    private void OnEnable()
    {
        employeeName = serializedObject.FindProperty("_employeeName");
        age = serializedObject.FindProperty("_age");
        gender = serializedObject.FindProperty("_gender");
        speed = serializedObject.FindProperty("speed");
        cleaning = serializedObject.FindProperty("cleaning");
        cooking = serializedObject.FindProperty("cooking");
        service = serializedObject.FindProperty("service");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        GUILayout.Label("Identity", GUILayout.ExpandWidth(true), GUILayout.Width(200), GUILayout.Height(20));
        GUILayout.Space(2);
        GUILayout.Label("Name: " + employeeName.stringValue, GUILayout.ExpandWidth(true), GUILayout.Width(200), GUILayout.Height(20));
        GUILayout.Label("Age: " + age.intValue, GUILayout.ExpandWidth(true), GUILayout.Width(90), GUILayout.Height(20));
        string _gender = (gender.boolValue)? "Male" : "Female"; 
        GUILayout.Label("Gender: " + _gender, GUILayout.ExpandWidth(true), GUILayout.Width(120), GUILayout.Height(20));

        GUILayout.Space(5);

        GUILayout.Label("Stats", GUILayout.ExpandWidth(true), GUILayout.Width(90), GUILayout.Height(20));
        GUILayout.Space(2);
        GUILayout.BeginHorizontal();
        GUILayout.Label("SP: " + speed.intValue, GUILayout.ExpandWidth(true), GUILayout.Width(75), GUILayout.Height(20));
        GUILayout.Label("CL: " + cleaning.intValue, GUILayout.ExpandWidth(true), GUILayout.Width(75), GUILayout.Height(20));
        GUILayout.Label("CK: " + cooking.intValue, GUILayout.ExpandWidth(true), GUILayout.Width(75), GUILayout.Height(20));
        GUILayout.Label("SV: " + service.intValue, GUILayout.ExpandWidth(true), GUILayout.Width(75), GUILayout.Height(20));
        GUILayout.EndHorizontal();



        serializedObject.ApplyModifiedProperties();
    }
}
