using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Palmmedia.ReportGenerator.Core.Common;

[CustomEditor(typeof(GridKeeper))]
public class Custominspector : Editor
{
    LevelGenerator targetScript;

    void OnEnable()
    {
       
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.BeginHorizontal();
        for (int y = 0; y < LevelGenerator.grid.GetLength(1); y++)
        {
            EditorGUILayout.BeginVertical();
            for (int x = 0; x < LevelGenerator.grid.GetLength(1); x++)
            {

                LevelGenerator.grid[y,x] = int.Parse(EditorGUILayout.TextField(LevelGenerator.grid[y, x].ToString()));
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();
    }
    }
