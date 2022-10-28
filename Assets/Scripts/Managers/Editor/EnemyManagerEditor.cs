#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if(GUILayout.Button("Save All Enemies"))
        {
            var script = (EnemyManager)target;
            script.SaveAllDataInLevel();
        }
        
    }
}

#endif