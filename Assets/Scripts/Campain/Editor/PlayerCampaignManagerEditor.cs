#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerCampaignManager))]
public class PlayerCampaignManagerEditor : Editor
{

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

    }

}

#endif