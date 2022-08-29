#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldBuilderHexagon))]
public class WorldBuilderHexagonEditor : Editor
{

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        var script = (WorldBuilderHexagon)target;

        GUILayout.Space(20);

        if(GUILayout.Button("Create Hexagons"))
        {
            script.CreateHexagons();
        }

        if(GUILayout.Button("Clear Hexagons")) 
        {
            script.ResetHexagons();
        }
    }

}

#endif