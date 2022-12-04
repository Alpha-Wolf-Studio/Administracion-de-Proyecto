using UnityEngine;

public class UpgradeBuySlot : UpgradeBase
{
    public override void UpdateCost ()
    {
        int baseCost = 0;

        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int) MilitaryType.Army:
                baseCost = uiMilitaryBase.upgradeProgression.expand[uiMilitaryBase.subCategorySelect].baseCostArmy;
                break;
            case (int) MilitaryType.Mercenary:
                // Don't use.
                break;
        }

        int amountUnits = GameManager.Get().GetMaxUnits(uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        cost = baseCost;

        if (amountUnits < 1)
            cost = 0;

        textCost.text = cost.ToString();
    }

    protected override void BuyUpgrade ()
    {
        bool wasSuccessful = GameManager.Get().BuySlot(cost, currencyType, uiMilitaryBase.subCategorySelect, (MilitaryType) uiMilitaryBase.mainCategorySelect);
        SetTryBuy(wasSuccessful);
    }
}