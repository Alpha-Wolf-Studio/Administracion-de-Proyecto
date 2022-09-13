#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerCampaignManager))]
public class PlayerCampaignManagerEditor : Editor
{

    private LevelData currentSelectedLevelData = new LevelData();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying) return;

        var script = (PlayerCampaignManager)target;

        GUILayout.Space(20f);

        if(GUILayout.Button("Complete Current Selected Level")) 
        {
            script.CompleteCurrentLevel();
        }

        GUILayout.Space(20f);

        var currentSelectedTerrain = script.GetCurrentSelectedTerrain();

        if (currentSelectedTerrain == null) return;

        GUILayout.Label("Level Index " + currentSelectedTerrain.TerrainIndex + " Configurations");

        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Current Level Name: " + currentSelectedTerrain.TerrainName);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("New Level Name: ");
        currentSelectedLevelData.LevelName = EditorGUILayout.TextField(currentSelectedLevelData.LevelName);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Current Province Index: " + currentSelectedTerrain.ProvinceIndex);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("New Province Index: ");
        currentSelectedLevelData.ProvinceIndex = EditorGUILayout.IntField(currentSelectedLevelData.ProvinceIndex);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Current Gold Income: " + currentSelectedTerrain.GoldIncome);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("New Gold Income: ");
        currentSelectedLevelData.GoldIncome = EditorGUILayout.IntField(currentSelectedLevelData.GoldIncome);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.LabelField("Current Gold On Complete: " + currentSelectedTerrain.GoldOnComplete);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("New Gold On Complete: ");
        currentSelectedLevelData.GoldOnComplete = EditorGUILayout.IntField(currentSelectedLevelData.GoldOnComplete);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        GUILayout.EndVertical();

        if (GUILayout.Button("Set New Level Data"))
        {
            currentSelectedLevelData.Index = currentSelectedTerrain.TerrainIndex;
            script.GetCurrentSelectedTerrain().SetData(currentSelectedLevelData);
        }

    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }

}

#endif