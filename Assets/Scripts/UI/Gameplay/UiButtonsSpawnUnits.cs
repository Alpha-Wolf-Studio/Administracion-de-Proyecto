using System.Collections.Generic;
using UnityEngine;

public class UiButtonsSpawnUnits : MonoBehaviour
{
    [SerializeField] private List<UiButtonSpawnUnit> btnSpawnUnit = new List<UiButtonSpawnUnit>();
    private List<AmountMilitary> amountMilitary = new List<AmountMilitary>();

    private void Start ()
    {
        amountMilitary.Add(new AmountMilitary()); // Army
        amountMilitary.Add(new AmountMilitary()); // Mercenary
        UnitData[] unitArmy = GameManager.Get().GetUnitsArmy();
        UnitData[] unitMerc = GameManager.Get().GetUnitsMercenary();

        for (int i = 0; i < unitArmy.Length; i++)
        {
            int indexMilitar = (int) MilitaryType.Army;
            int idUnit = unitArmy[i].IdUnit;
            amountMilitary[indexMilitar].amountId[idUnit]++;
        }

        for (int i = 0; i < unitMerc.Length; i++)
        {
            int indexMilitar = (int)MilitaryType.Mercenary;
            int idUnit = unitMerc[i].IdUnit;
            amountMilitary[indexMilitar].amountId[idUnit]++;
        }

        for (int i = 0; i < btnSpawnUnit.Count; i++)
        {
            int indexMilitary = (int) btnSpawnUnit[i].militaryType;
            int indexUnit = btnSpawnUnit[i].idUnit;

            int amount = amountMilitary[indexMilitary].amountId[indexUnit];
            btnSpawnUnit[i].Set(amount);
        }
    }
}

class AmountMilitary
{
    public List<int> amountId = new List<int>();

    public AmountMilitary ()
    {
        int maxDiferentUnits = GameManager.Get().unitsStatsLoaded.Count;

        for (int i = 0; i < maxDiferentUnits; i++)
        {
            amountId.Add(0);
        }
    }
}