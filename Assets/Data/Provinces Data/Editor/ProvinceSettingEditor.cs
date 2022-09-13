#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProvinceSettings))]
public class ProvinceSettingEditor : Editor
{

    private ProvinceSettings script;

    public void Awake()
    {
        script = (ProvinceSettings)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Current Terrains Amount: \t" + script.CurrentTerrainsAmount);
    }
}

#endif