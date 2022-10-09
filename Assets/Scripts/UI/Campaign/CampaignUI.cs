using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CampaignUI : MonoBehaviour
{

    [Header("General Stats Panel")]
    [SerializeField] GameObject panelGeneralStats = default;
    [SerializeField] TMPro.TextMeshProUGUI incomeTextComponent = default;
    [SerializeField] TMPro.TextMeshProUGUI currentGoldTextComponent = default;
    [SerializeField] TMP_Text currentDiamondTextComponent;
    [Header("Army and Mercenary Panel")]
    [SerializeField] GameObject panelArmyAndMercenary = default;
    [Header("Selected Stats Panel")]
    [SerializeField] GameObject panelSelected = default;
    [SerializeField] TMPro.TextMeshProUGUI selectedNameTextComponent = default;
    [SerializeField] TMPro.TextMeshProUGUI selectedWinGoldTextComponent = default;
    [SerializeField] TMPro.TextMeshProUGUI selectedIncomeTextComponent = default;
    [SerializeField] PlayerCampaignManager campaignManager = default;

    [Space(10)]
    [SerializeField] private Button selectMisionButton = default;

    private void Awake()
    {
        GoldCalculations.OnIncomeGoldChange += ChangeCurrentIncome;
        campaignManager.OnSelectionChange += OnSelectionChanged;
        selectMisionButton.onClick.AddListener(StartLevel);
    }

    private void Start()
    {
        GameManager.Get().TriggerIncomeGoldRecalculation();
    }

    private void OnDestroy()
    {
        GoldCalculations.OnIncomeGoldChange -= ChangeCurrentIncome;
        campaignManager.OnSelectionChange -= OnSelectionChanged;
        selectMisionButton.onClick.RemoveListener(StartLevel);
    }

    private void Update()
    {
        currentGoldTextComponent.text = "Current Gold: " + GameManager.Get().GetPlayerGold();
        currentDiamondTextComponent.text = "Current Diamond: " + GameManager.Get().GetPlayerDiamond();
    }

    private void ChangeCurrentIncome() 
    {
        incomeTextComponent.text = "Current Income: " + GoldCalculations.IncomeGold;
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
            var terrainData = terrain.GetLevelData();
            selectedNameTextComponent.text = terrainData.LevelName;
            selectedIncomeTextComponent.text = "Terrain Income: " + terrainData.GoldIncome;
            selectedWinGoldTextComponent.text = "Terrain Gold: " + terrainData.GoldOnComplete;
        }
        else 
        {
            panelSelected.SetActive(false);
        }

    }

}
