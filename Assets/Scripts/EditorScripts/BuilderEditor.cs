using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Builder))]
public class BuilderEditor : Editor {

    private int index = 0;

    public override void OnInspectorGUI()
    {
        //so we don't redo the whole inspector but only add to it
        DrawDefaultInspector();

        Builder builder = (Builder)target;
        builder.RefreshListOfObjects();
        string[] options = new string[builder.GetGameObjects().Length];

        for (int i = 0; i < options.Length; i++)
        {
            options[i] = builder.GetGameObjects()[i].name;
        }

        index = EditorGUILayout.Popup("Prefabs List:",index, options, EditorStyles.popup);

        if (GUILayout.Button("Add Object"))
        {
            builder.AddObject(index);
        }

        if (GUILayout.Button("Undo"))
        {
            builder.Undo();
        }

    }
}
