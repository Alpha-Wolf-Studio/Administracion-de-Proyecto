using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    // Todo: Este script está deprecado

    [SerializeField] private UnitSelector unitSelector;
    [SerializeField] private Button btnLevelUp;
    [SerializeField] private Button btnBuyMercenary;
    [SerializeField] private Button btnBuyArmy;
    [SerializeField] private Button btnHealAllUnits;

    private void Start ()
    {
        btnLevelUp.onClick.AddListener(LevelUpUnit);
        btnBuyMercenary.onClick.AddListener(BuyMercenary);
        btnBuyArmy.onClick.AddListener(BuyArmy);
        btnHealAllUnits.onClick.AddListener(HealAllUnits);
    }

    private void OnDestroy ()
    {
        btnLevelUp.onClick.RemoveAllListeners();
        btnBuyMercenary.onClick.RemoveAllListeners();
        btnBuyArmy.onClick.RemoveAllListeners();
        btnHealAllUnits.onClick.RemoveAllListeners();
    }

    private void HealAllUnits ()
    {
        //GameManager.Get().HealAllUnits();
    }

    private void BuyArmy ()
    {
        //GameManager.Get().BuyArmy(unitSelector.currentUnit);
    }

    private void BuyMercenary ()
    {
        //GameManager.Get().BuyMercenary(unitSelector.currentUnit);
    }

    private void LevelUpUnit ()
    {
        //GameManager.Get().LevelUpUnit(unitSelector.currentUnit);
    }
}