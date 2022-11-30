using System.Collections.Generic;

public class UpgradeHealUnits : UpgradeBase
{
    public override void UpdateCost ()
    {
        List<UnitData> units = uiMilitaryBase.GetUnitsFiltered();

        if (units.Count > 0)
        {
            int multiplyPerLevel = 0;
            int multiplyPerLife = 0;

            int idUnits = units[0].IdUnit;
            int levelUnit = 0;

            switch (uiMilitaryBase.mainCategorySelect)
            {
                case (int) MilitaryType.Army:
                    levelUnit = GameManager.Get().GetLevelsUnitsArmy(idUnits);
                    multiplyPerLevel = uiMilitaryBase.upgradeProgression.heal.armyMultiplyPerLevel;
                    multiplyPerLife = uiMilitaryBase.upgradeProgression.heal.armyMultiplyPerLife;
                    break;
                case (int) MilitaryType.Mercenary:
                    levelUnit = GameManager.Get().GetLevelsUnitsMercenary(idUnits);
                    multiplyPerLevel = uiMilitaryBase.upgradeProgression.heal.mercenaryMultiplyPerLevel;
                    multiplyPerLife = uiMilitaryBase.upgradeProgression.heal.mercenaryMultiplyPerLife;
                    break;
                default:
                    levelUnit = 0;
                    multiplyPerLevel = 0;
                    multiplyPerLife = 0;
                    break;
            }

            float maxLifeUnit = GameManager.Get().GetUnitStats(idUnits).GetLifeLevel(levelUnit, idUnits);
            float lifeRemaining = 0;

            for (int i = 0; i < units.Count; i++)
            {
                lifeRemaining += (maxLifeUnit - units[i].Life);
            }

            if (uiMilitaryBase.upgradeProgression.heal.isMultiplicateLifeAndLevel)
                cost = (int) ((lifeRemaining * multiplyPerLife) * ((levelUnit + 1) * multiplyPerLevel));
            else
                cost = (int) ((lifeRemaining * multiplyPerLife) + ((levelUnit + 1) * multiplyPerLevel));

            cost = (int) ((lifeRemaining * multiplyPerLife) * (levelUnit * multiplyPerLevel));
            if (lifeRemaining < 1)
                cost = 0;
        }
        else
        {
            cost = 0;
        }

        textCost.text = cost.ToString();
    }

    protected override void BuyUpgrade ()
    {
        if (uiMilitaryBase.GetUnitsFiltered().Count > 0)
        {
            bool wasSuccessful = GameManager.Get().HealAllUnitsFiltered(cost, uiMilitaryBase.GetUnitsFiltered(), (MilitaryType) uiMilitaryBase.mainCategorySelect);
        }
    }
}