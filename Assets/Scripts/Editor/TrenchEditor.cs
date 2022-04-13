#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trench))]
public class TrenchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (Trench)target;
        GUILayout.Label("Has Troops: " + myScript.HasTroops);
        if (myScript.HasTroops) 
        {
            GUILayout.Label("Current Troops Layer: " + myScript.CurrentTroopsLayer);
            GUILayout.Space(10);

            for (int i = 0; i < myScript.GetCoveragesPositions().Count; i++)
            {
                var ocuppantString = "Current troop in slot " + i + ": ";
                if (myScript.GetCoveragesPositions()[i].occupant != null) 
                {
                    ocuppantString += myScript.GetCoveragesPositions()[i].occupant.gameObject.name;
                }
                else 
                {
                    ocuppantString += "None";
                }
                GUILayout.Label(ocuppantString);
            }
        }
    }
}

#endif