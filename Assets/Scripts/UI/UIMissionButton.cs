using UnityEngine;
using UnityEngine.UI;

public class UIMissionButton : MonoBehaviour
{

    public System.Action<int> OnMissionSelected;

    public int MissionLevel { set { missionLevel = value; } get { return missionLevel; } }

    [Header("Button Configurations")]
    [SerializeField] private GameObject disableGO = default;
    [SerializeField] private int missionLevel = 0;

    private Button clickButton = default;

    private void Awake()
    {
        clickButton = GetComponentInChildren<Button>();
        clickButton.onClick.AddListener(ButtonClicked);
    }

    private void OnDestroy()
    {
        clickButton.onClick.RemoveListener(ButtonClicked);
    }

    private void ButtonClicked() => OnMissionSelected?.Invoke(missionLevel);

    public void EnableMission() => disableGO.SetActive(false);

    public void DisableMission() => disableGO.SetActive(true);

}
