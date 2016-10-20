using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator levelGenerator = (LevelGenerator) target;
        if (GUILayout.Button("Generate Level"))
        {
            levelGenerator.LoadMap();
        }

        if (GUILayout.Button("Clear Level"))
        {
            levelGenerator.EmptyMap();
        }
    }
}


