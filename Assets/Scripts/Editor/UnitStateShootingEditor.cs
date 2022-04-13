#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitStateShooting))]
public class UnitStateShootingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (UnitStateShooting)target;
        if (myScript.GetEnemyTransform() != null) GUILayout.Label("Current Enemy: " + myScript.GetEnemyTransform().gameObject.name);
        else GUILayout.Label("No enemy in sight");
    }
}

#endif
