#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitShootBehaviour))]
public class UnitShootBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying) return;
        
        var myScript = (UnitShootBehaviour)target;

        if(myScript.TimeForNextShot < 0) 
        {
            GUILayout.Label("Can shoot.");
        }
        else 
        {
            GUILayout.Label("Next shot in " + myScript.TimeForNextShot + ".");
        }

    }
}

#endif
