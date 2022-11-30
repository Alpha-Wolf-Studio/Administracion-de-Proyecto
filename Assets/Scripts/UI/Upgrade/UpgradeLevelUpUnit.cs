public class UpgradeLevelUpUnit : UpgradeBase
{
    public override void UpdateCost ()
    {
        int levelMultiplicator = 1;

        int levelUnit = 0;
        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                levelUnit = GameManager.Get().GetLevelsUnitsArmy(uiMilitaryBase.subCategorySelect);
                levelMultiplicator = uiMilitaryBase.upgradeProgression.lvlUp.armyLevelMultiplicator;
                break;
            case (int) MilitaryType.Mercenary:
                levelUnit = GameManager.Get().GetLevelsUnitsMercenary(uiMilitaryBase.subCategorySelect);
                levelMultiplicator = uiMilitaryBase.upgradeProgression.lvlUp.mercenaryLevelMultiplicator;
                break;
            default:
                levelUnit = 0;
                levelMultiplicator = 0;
                break;
        }

        cost = (levelUnit + 1) * 100;
        textCost.text = cost.ToString();
    }

    protected override void BuyUpgrade ()
    {
        bool wasSuccessful = GameManager.Get().LevelUpUnit(cost, uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
    }
}