using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelShop : MonoBehaviour
{
    [SerializeField] private TMP_Text textNameUnit;
    [SerializeField] private List<Button> btnUnitsHeader = new List<Button>();
    [SerializeField] private List<UiPanelShopStat> shopStat = new List<UiPanelShopStat>();
    [SerializeField] private Sprite imageNormal;
    [SerializeField] private Sprite imageSelected;
    [SerializeField] private Color colorSelected;
    [SerializeField] private Button btnUpgrade;
    private int currentUnit;

    private void Start()
    {
        LoadShop();
        for (int i = 0; i < btnUnitsHeader.Count; i++)
        {
            int index = i;
            btnUnitsHeader[i].onClick.AddListener(() => { LoadUnitUi(index); });
        }

        btnUpgrade.onClick.AddListener(ButtonUpgrade);
    }

    private void LoadShop()
    {
        LoadUnitUi(0);
    }

    private void LoadUnitUi(int unitIndex)
    {
        int maxLevelUnits = 10;
        currentUnit = unitIndex;

        int unitLevel = GameManager.Get().GetLevelsUnits(unitIndex);
        int unitNextLevel = unitLevel + 1 > maxLevelUnits ? maxLevelUnits : unitLevel + 1;

        btnUpgrade.interactable = unitLevel != maxLevelUnits;
        textNameUnit.text = GameManager.Get().unitsStatsLoaded[unitIndex].nameUnit + "( " + unitLevel + " )";

        float unitLife = GameManager.Get().unitsStatsLoaded[unitIndex].GetLifeLevel(unitLevel);
        float unitNextLife = GameManager.Get().unitsStatsLoaded[unitIndex].GetLifeLevel(unitNextLevel);
        float unitMaxLife = GameManager.Get().unitsStatsLoaded[unitIndex].GetLifeLevel(maxLevelUnits);
        Vector3 currMaxLife = new Vector3(unitLife, unitNextLife, unitMaxLife);
        shopStat[0].UpdateUi(unitLife, unitNextLife, unitMaxLife, unitLevel);

        float unitDamage = GameManager.Get().unitsStatsLoaded[unitIndex].GetDamageLevel(unitLevel);
        float unitNextDamage = GameManager.Get().unitsStatsLoaded[unitIndex].GetDamageLevel(unitNextLevel);
        float unitMaxDamage = GameManager.Get().unitsStatsLoaded[unitIndex].GetDamageLevel(maxLevelUnits);
        Vector3 currMaxDamage = new Vector3(unitDamage, unitNextDamage, unitMaxDamage);
        shopStat[1].UpdateUi(unitDamage, unitNextDamage, unitMaxDamage, unitLevel);
    }

    public void AddMoney(int value)
    {
        GameManager.Get().ModifyGoldPlayer(value);
    }

    private void ButtonUpgrade()
    {
        int moneyDecrease = GameManager.Get().unitsStatsLoaded[currentUnit].moneyPerLevel;
        if (GameManager.Get().ModifyGoldPlayer(-moneyDecrease))
        {
            GameManager.Get().AddLevelUnit(currentUnit);
            LoadUnitUi(currentUnit);
        }
    }
}