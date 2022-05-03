#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitShootBehaviour))]
public class UnitShootBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (UnitShootBehaviour)target;
        if (myScript.GetCurrentEnemyTransform() != null) GUILayout.Label("Current Enemy: " + myScript.GetCurrentEnemyTransform().gameObject.name);
        else GUILayout.Label("No enemy in sight");
    }
}

#endif
