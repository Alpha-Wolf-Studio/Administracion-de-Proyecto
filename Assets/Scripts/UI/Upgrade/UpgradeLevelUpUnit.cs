using UnityEngine;

public class UpgradeLevelUpUnit : UpgradeBase
{
    protected override void BuyUpgrade ()
    {
        GameManager.Get().LevelUpUnit(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
    }
}