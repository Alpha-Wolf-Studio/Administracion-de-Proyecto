using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CampaignUI : MonoBehaviour
{

    [Header("Selected Stats Panel")]
    [SerializeField] GameObject panelSelected;
    [SerializeField] TMP_Text textTitleLevel;
    [SerializeField] TMP_Text textGoldReward;
    [SerializeField] TMP_Text textIncomeReward;
    [SerializeField] TMP_Text textDiamondReward;

    [SerializeField] PlayerCampaignManager campaignManager;

    [Space(10)]
    [SerializeField] private Button selectMisionButton = default;

    private void Awake()
    {
        campaignManager.OnSelectionChange += OnSelectionChanged;
        selectMisionButton.onClick.AddListener(StartLevel);
    }

    private void Start()
    {
        GameManager.Get().TriggerIncomeGoldRecalculation();
    }

    private void OnDestroy()
    {
        campaignManager.OnSelectionChange -= OnSelectionChanged;
        selectMisionButton.onClick.RemoveListener(StartLevel);
    }

    private void StartLevel() 
    {
        CustomSceneManager.Get().LoadScene("Gameplay");
    }

    private void OnSelectionChanged(HexagonTerrain terrain) 
    {
        if(terrain != null) 
        {
            panelSelected.SetActive(true);
            LevelData terrainData = terrain.GetLevelData();
            textTitleLevel.text = terrainData.LevelName;
            textIncomeReward.text = "Terrain Income: " + terrainData.GoldIncome;
            textGoldReward.text = "Terrain Gold: " + terrainData.GoldOnComplete;
        }
        else 
        {
            panelSelected.SetActive(false);
        }

    }

}
