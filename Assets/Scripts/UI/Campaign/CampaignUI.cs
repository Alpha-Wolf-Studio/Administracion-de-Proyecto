using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CampaignUI : MonoBehaviour
{
    [SerializeField] private GameObject panelSelected;
    [SerializeField] private TMP_Text textTitleLevel;
    [SerializeField] private UiPanelReward[] panelReward;
    [SerializeField] private PlayerCampaignManager campaignManager;
    [SerializeField] private UiPanelShowUnitsTerrain[] uiPanelShowUnitsTerrain;
    [SerializeField] private Button selectMisionButton;

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
        if (terrain != null)
        {
            panelSelected.SetActive(true);
            LevelData terrainData = terrain.GetLevelData();
            textTitleLevel.text = terrainData.LevelName;

            panelReward[0].SetText(terrainData.GoldIncome);
            panelReward[1].SetText(terrainData.GoldOnComplete);
            panelReward[2].SetText(terrainData.DiamondOnComplete);

            List<int> amountForDiferentsEnemies = new List<int>();
            for (int i = 0; i < (int) EnemyType.Size; i++)
                amountForDiferentsEnemies.Add(0);

            for (int i = 0; i < terrainData.Enemies.Count; i++)
                amountForDiferentsEnemies[(int) (terrainData.Enemies[i].TypeOfEnemy)]++;

            for (int i = 0; i < uiPanelShowUnitsTerrain.Length; i++)
            {
                if (amountForDiferentsEnemies.Count - 1 >= i)
                {
                    uiPanelShowUnitsTerrain[i].Set(amountForDiferentsEnemies[i]);
                }
                else
                {
                    uiPanelShowUnitsTerrain[i].Set(0);
                    Debug.Log("Panel: " + i + " no tiene Unidad");
                }
            }
        }
        else 
        {
            panelSelected.SetActive(false);
        }
    }
}
