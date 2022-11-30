public class UpgradeBuySlot : UpgradeBase
{
    public override void UpdateCost ()
    {
        int multiplyPerLevel = 0;
        int multiplyPerLevelPlus = 0;

        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                multiplyPerLevel = uiMilitaryBase.upgradeProgression.expand.armyMultiplyPerLevel;
                multiplyPerLevelPlus = uiMilitaryBase.upgradeProgression.expand.armyMultiplyPerLevelPlus;
                break;
            case (int) MilitaryType.Mercenary:
                multiplyPerLevel = uiMilitaryBase.upgradeProgression.expand.mercenaryMultiplyPerLevel;
                multiplyPerLevelPlus = uiMilitaryBase.upgradeProgression.expand.mercenaryMultiplyPerLevelPlus;
                break;
            default:
                multiplyPerLevel = 0;
                multiplyPerLevelPlus = 0;
                break;
        }

        int amountUnits = GameManager.Get().GetMaxUnits(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        cost = amountUnits * multiplyPerLevel + (amountUnits - 1) * multiplyPerLevelPlus;

        if (amountUnits < 1)
            cost = 0;

        textCost.text = cost.ToString();
    }

    protected override void BuyUpgrade ()
    {
        bool wasSuccessful = GameManager.Get().BuySlot(cost, uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
    }
}