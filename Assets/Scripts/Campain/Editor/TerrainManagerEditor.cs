#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        if (!Application.isPlaying) return;

        var script = (TerrainManager)target;

        GUILayout.Space(20);

        GUILayout.Label("States Configurations");

        if (GUILayout.Button("Reset Current Hexagon States")) 
        {
            script.ResetCurrentHexagonStates();
        }

        if (GUILayout.Button("Save Current Hexagon States"))
        {
            script.SaveCurrentHexagonStates();
        }

        if (GUILayout.Button("Unlock All Hexagons"))
        {
            script.UnlockAllHexagons();
        }

        GUILayout.Space(20);

        GUILayout.Label("Data Configurations");

        if (GUILayout.Button("Reset All Hexagons Data"))
        {
            script.ResetCurrentHexagonData();
        }

        if (GUILayout.Button("Save All Hexagons Data"))
        {
            script.SaveCurrentHexagonData();
        }

    }

}

#endif