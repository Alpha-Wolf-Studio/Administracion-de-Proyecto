using UnityEngine;

public class UIMissionsScreen : MonoBehaviour
{

    private UIMissionButton[] missionButtons = default;

    private void Awake()
    {

        missionButtons = GetComponentsInChildren<UIMissionButton>();
        
    }

    private void Start()
    {

        int currentLevel = GameManager.Get().GetLastLevelPlayer();
        foreach (var button in missionButtons)
        {
            if(button.MissionLevel <= currentLevel + 1) 
            {
                button.EnableMission();
            }
            else 
            {
                button.DisableMission();
            }

            button.OnMissionSelected += MissionSelect;
        }
        
    }

    private void MissionSelect(int missionIndex) 
    {
        var levelString = "Level " + missionIndex.ToString();
        CustomSceneManager.Get().LoadScene(levelString);
    }

}
