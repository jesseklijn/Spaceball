using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Draw the basic inspector
        DrawDefaultInspector();
        GUILayout.Box("Builds the level with a random seed when clicked.");
        {
            
            //Creates a button in the inspector of levelgenerator.
            LevelGenerator levelGenerator = (LevelGenerator)target;
            if (GUILayout.Button("Build"))
            {
                //First clear the level, even if there is no level, and generate a level
                levelGenerator.ClearLevel();
                levelGenerator.Generate();

            }

        }
     

    }
}