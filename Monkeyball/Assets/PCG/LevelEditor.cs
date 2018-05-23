using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator levelGenerator = (LevelGenerator)target;
        if (GUILayout.Button("Build Level"))
        {
            levelGenerator.ClearLevel();
            levelGenerator.Generate();
          
        }
    }
}