using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelsData))]
public class LevelsDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        if(GUILayout.Button("Sort by Index")) 
        {
            var script = (LevelsData)target;
            script.SortListByIndex();
        }

        if (GUILayout.Button("Sort by Name"))
        {
            var script = (LevelsData)target;
            script.SortListByName();
        }

        DrawDefaultInspector();
    }
}
