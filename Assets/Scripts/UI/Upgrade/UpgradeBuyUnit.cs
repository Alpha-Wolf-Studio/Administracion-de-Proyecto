using UnityEngine;

public class UpgradeBuyUnit : UpgradeBase
{
    protected override void BuyUpgrade ()
    {
        switch ((MilitaryType) uiMilitaryBase.mainCategorySelect)
        {
            case MilitaryType.Army:
                GameManager.Get().BuyArmy(uiMilitaryBase.subCategorySelect);
                break;
            case MilitaryType.Mercenary:
                GameManager.Get().BuyMercenary(uiMilitaryBase.subCategorySelect);
                break;
        }
    }
}