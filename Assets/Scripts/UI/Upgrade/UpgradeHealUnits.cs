using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealUnits : UpgradeBase
{
    public override void UpdateCost ()
    {
        List<UnitData> units = uiMilitaryBase.GetUnitsFiltered();

        if (units.Count > 0)
        {
            int idUnits = uiMilitaryBase.subCategorySelect;
            int levelUnit = 0;
            int baseCost = 0;

            switch (uiMilitaryBase.mainCategorySelect)
            {
                case (int) MilitaryType.Army:
                    levelUnit = GameManager.Get().GetLevelsUnitsArmy(idUnits);
                    baseCost = uiMilitaryBase.upgradeProgression.heal[uiMilitaryBase.subCategorySelect].baseCostArmy;
                    break;
                case (int) MilitaryType.Mercenary:
                    // Don't use.
                    break;
            }

            float maxLifeUnit = GameManager.Get().GetUnitStats(idUnits).GetLifeLevel(levelUnit, idUnits);
            float lifeRemaining = 0;

            for (int i = 0; i < units.Count; i++)
                lifeRemaining += (maxLifeUnit - units[i].Life);

            cost = (int) (Mathf.Pow(2, levelUnit) * baseCost);
            if (lifeRemaining < 1)
                cost = 0;
        }
        else
        {
            cost = 0;
        }

        textCost.text = cost.ToString();
    }

    public override void BuyUpgrade ()
    {
        if (uiMilitaryBase.GetUnitsFiltered().Count > 0)
        {
            bool wasSuccessful = GameManager.Get().HealAllUnitsFiltered(cost, currencyType, uiMilitaryBase.GetUnitsFiltered(), (MilitaryType) uiMilitaryBase.mainCategorySelect);
            SetTryBuy(wasSuccessful);
        }
    }
}