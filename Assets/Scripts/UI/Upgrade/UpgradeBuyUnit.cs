using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuyUnit : UpgradeBase
{
    public override void UpdateCost ()
    {
        List<UnitData> units = uiMilitaryBase.GetUnitsFiltered();
        int levelUnit = 0;
        int baseCost = 0;

        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                levelUnit = GameManager.Get().GetLevelsUnitsArmy(uiMilitaryBase.subCategorySelect);
                baseCost = uiMilitaryBase.upgradeProgression.buy[uiMilitaryBase.subCategorySelect].baseCostArmy;
                break;
            case (int) MilitaryType.Mercenary:
                levelUnit = GameManager.Get().GetLevelsUnitsMercenary(uiMilitaryBase.subCategorySelect) - PlayerData.levelMoreMercenary;
                baseCost = uiMilitaryBase.upgradeProgression.buy[uiMilitaryBase.subCategorySelect].baseCostMercenary;
                break;
        }

        cost = (int) (Mathf.Pow(2, levelUnit) * baseCost);
        textCost.text = cost.ToString();
    }

    public override void BuyUpgrade ()
    {
        int filteredAmount = uiMilitaryBase.GetUnitsFiltered().Count;
        int slotsAvailables = GameManager.Get().GetMaxUnits(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        bool wasSuccessful = false;

        if (filteredAmount < slotsAvailables)
        {
            switch ((MilitaryType) uiMilitaryBase.mainCategorySelect)
            {
                case MilitaryType.Army:
                    wasSuccessful = GameManager.Get().BuyArmy(cost, currencyType, uiMilitaryBase.subCategorySelect, MilitaryType.Army);
                    break;
                case MilitaryType.Mercenary:
                    wasSuccessful = GameManager.Get().BuyMercenary(cost, currencyType, uiMilitaryBase.subCategorySelect, MilitaryType.Mercenary);
                    break;
            }

            SetTryBuy(wasSuccessful);
        }
    }
}