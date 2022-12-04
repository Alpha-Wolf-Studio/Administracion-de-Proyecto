using UnityEngine;

public class UpgradeLevelUpUnit : UpgradeBase
{
    public override void UpdateCost ()
    {
        int levelUnit = 0;
        int baseCost = 0;

        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                levelUnit = GameManager.Get().GetLevelsUnitsArmy(uiMilitaryBase.subCategorySelect);
                baseCost = uiMilitaryBase.upgradeProgression.lvlUp[uiMilitaryBase.subCategorySelect].baseCost;
                break;
            case (int) MilitaryType.Mercenary:
                // Don't use.
                break;
        }

        cost = (int) (Mathf.Pow(2, levelUnit) * baseCost);
        textCost.text = cost.ToString();
    }

    public override void BuyUpgrade ()
    {
        bool wasSuccessful = GameManager.Get().LevelUpUnit(cost, currencyType, uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        SetTryBuy(wasSuccessful);
    }
}