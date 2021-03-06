﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Serializer))]
public class SaveLocationEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //so we don't redo the whole inspector but only add to it
        //so we don't redo the whole inspector but only add to it
        DrawDefaultInspector();
        EditorGUILayout.TextArea("PLEASE CARE WHERE YOU CLICK");
        Serializer save = (Serializer)target;
        if (GUILayout.Button("Save level"))
        {
            save.SaveData();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.TextArea("SERIOUSLY WATCH OUT");

        if (GUILayout.Button("Load level"))
        {
            save.LoadData();
        }
        if (GUILayout.Button("Save terrain"))
        {
            save.SaveTerrainData();
        }
        if (GUILayout.Button("Load terrain"))
        {
            save.LoadTerrainData();
        }
    }
}
