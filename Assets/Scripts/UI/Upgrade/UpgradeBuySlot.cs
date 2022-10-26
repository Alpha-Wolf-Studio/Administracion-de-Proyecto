using UnityEngine;

public class UpgradeBuySlot : UpgradeBase
{
    protected override void BuyUpgrade ()
    {
        GameManager.Get().BuySlot(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
    }
}