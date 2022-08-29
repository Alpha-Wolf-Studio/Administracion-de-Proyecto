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

        if (GUILayout.Button("Save Current Hexagon States"))
        {
            script.SaveCurrentHexagonStates();
        }

        if (GUILayout.Button("Reset Current Hexagon States")) 
        {
            script.ResetCurrentHexagonStates();
        }

    }

}

#endif