#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitStateMoving))]
public class UnitStateMovingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (UnitStateMoving)target;
        if (myScript.GetTarget() == null) GUILayout.Label("No target movement");
        else GUILayout.Label("Target Position: " + myScript.GetTarget().position);
    }
}

#endif