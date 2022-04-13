#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit))]
public class UnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myScript = (Unit)target;

        var heightOption = GUILayout.Height(25);

        var statsTitleStyle = new GUIStyle();

        statsTitleStyle.fontSize = 18;
        statsTitleStyle.normal.textColor = Color.red;
        statsTitleStyle.alignment = TextAnchor.LowerLeft;

        GUILayout.Space(10f);
        GUILayout.Label("Current State: " + myScript.GetCurrentState());
        GUILayout.Space(30f);
        GUILayout.Label("-Stats-\n", statsTitleStyle);
        GUILayout.Label("Name: " + myScript.stats.nameUnit + "\n", heightOption);
        GUILayout.Label("Life: " + myScript.stats.life + "\n", heightOption);
        GUILayout.Label("Sight: " + myScript.stats.radiusSight + "\n", heightOption);

        GUILayout.Label("Can Move: " + myScript.stats.canMove + "\n", heightOption);
        if (myScript.stats.canMove) GUILayout.Label("Velocity: " + myScript.stats.velocity + "\n", heightOption);

        GUILayout.Label("Can Shoot: " + myScript.stats.canShoot + "\n", heightOption);
        if (myScript.stats.canShoot) 
        {
            GUILayout.Label("Damage: " + myScript.stats.damage + "\n", heightOption);
            GUILayout.Label("Fire Rate: " + myScript.stats.fireRate + "\n", heightOption);
            GUILayout.Label("Bullet Speed: " + myScript.stats.bulletSpeed + "\n", heightOption);
        }

    }

}
#endif
