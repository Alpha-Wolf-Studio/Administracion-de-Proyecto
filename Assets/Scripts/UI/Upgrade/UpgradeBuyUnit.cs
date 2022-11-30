using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuyUnit : UpgradeBase
{
    public override void UpdateCost ()
    {
        int constMultiplicator = uiMilitaryBase.upgradeProgression.buy.constMultiplicator;

        List<UnitData> units = uiMilitaryBase.GetUnitsFiltered();
        int multiply = 1;

        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                multiply = uiMilitaryBase.upgradeProgression.buy.baseMultiplyArmy;
                break;
            case (int) MilitaryType.Mercenary:
                multiply = uiMilitaryBase.upgradeProgression.buy.baseMultiplyMercenary;
                break;
            default:
                multiply = 1;
                break;
        }

        cost = units.Count * constMultiplicator * multiply;
        textCost.text = cost.ToString();
    }

    protected override void BuyUpgrade ()
    {
        int filteredAmount = uiMilitaryBase.GetUnitsFiltered().Count;
        int slotsAvailables = GameManager.Get().GetMaxUnits(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        bool wasSuccessful = false;

        if (filteredAmount < slotsAvailables)
        {
            switch ((MilitaryType) uiMilitaryBase.mainCategorySelect)
            {
                case MilitaryType.Army:
                    wasSuccessful = GameManager.Get().BuyArmy(cost, uiMilitaryBase.subCategorySelect);
                    break;
                case MilitaryType.Mercenary:
                    wasSuccessful = GameManager.Get().BuyMercenary(cost, uiMilitaryBase.subCategorySelect);
                    break;
            }
        }

    }
}