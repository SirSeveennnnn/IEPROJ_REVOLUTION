using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSettings))]
public class LevelBuilder : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelSettings level = (LevelSettings)target;

        if (GUILayout.Button("Create Level"))
        {
            level.CreateLevel();
        }
        else if (GUILayout.Button("Delete Level"))
        {
            level.DeleteLevels();
        }


    }
}
