using UnityEngine;
using UnityEngine.UI;

public class UIMissionButton : MonoBehaviour
{

    public System.Action<int> OnMissionSelected;

    public int MissionLevel
    {
        private set
        {
            missionLevel = value;
        }
        get
        {
            return missionLevel;
        }
    }

    [Header("Button Configurations")]
    [SerializeField] private GameObject disableGO = default;
    [SerializeField] private int missionLevel = 0;

    private Button clickButton = default;
    private Image buttonImage = default;
    private TMPro.TextMeshProUGUI buttonTextComponent = default;


    private void Awake()
    {
        clickButton = GetComponentInChildren<Button>();
        clickButton.onClick.AddListener(ButtonClicked);
    }

    private void OnEnable()
    {
        buttonImage = clickButton.GetComponent<Image>();
        buttonTextComponent = clickButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        clickButton.onClick.RemoveListener(ButtonClicked);
    }

    private void ButtonClicked() => OnMissionSelected?.Invoke(missionLevel);

    public void EnableMission()
    {
        disableGO.SetActive(false);
        buttonImage.color = Color.white;
        buttonTextComponent.color = Color.white;
    }

    public void DisableMission()
    {
        disableGO.SetActive(true);
        buttonImage.color = Color.grey;
        buttonTextComponent.color = Color.grey;
    }

}
