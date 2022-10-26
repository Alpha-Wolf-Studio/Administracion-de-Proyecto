using UnityEngine;

public class UpgradeHealUnits : UpgradeBase
{
    protected override void BuyUpgrade ()
    {
        switch (uiMilitaryBase.mainCategorySelect)
        {
            case (int)MilitaryType.Army:
                GameManager.Get().HealAllUnits();
                break;
            case (int)MilitaryType.Mercenary:
                GameManager.Get().HealAllUnits();
                break;
        }
    }
}