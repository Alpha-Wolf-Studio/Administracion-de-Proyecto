using System.Collections.Generic;
using UnityEngine;

public class UnitsMilitary : MonoBehaviour
{
    public MilitaryType militaryType = MilitaryType.Army;
    [SerializeField] private UnitMilitaryComponent pfUnitMilitar;
    [SerializeField] private Transform content;
    [SerializeField] private List<UnitMilitaryComponent> btnUnitsMilitary = new List<UnitMilitaryComponent>();

    [SerializeField] private UnitSelector unitSelector;

    void Start ()
    {
        foreach (UnitMilitaryComponent militar in btnUnitsMilitary)
        {
            militar.gameObject.SetActive(false);
        }

        UnitData[] unitsData = null;
        switch (militaryType)
        {
            case MilitaryType.Army:
                unitsData = GameManager.Get().GetUnitsArmy();
                GameManager.Get().OnAddUnitArmy += AddNewUnit;
                break;
            case MilitaryType.Mercenary:
                unitsData = GameManager.Get().GetUnitsMercenary();
                GameManager.Get().OnAddUnitMercenary += AddNewUnit;
                break;
        }

        for (int i = 0; i < unitsData.Length; i++)
        {
            int index = i;
            if (index >= btnUnitsMilitary.Count)
            {
                UnitMilitaryComponent militar = Instantiate(pfUnitMilitar, content);
                btnUnitsMilitary.Add(militar);
            }

            InizializePanel(index);
        }
    }

    private void InizializePanel (int index)
    {
        btnUnitsMilitary[index].gameObject.SetActive(true);

        int idUnit = 0;
        switch (militaryType)
        {
            case MilitaryType.Army:
                idUnit = GameManager.Get().GetUnitsArmy()[index].IdUnit;
                btnUnitsMilitary[index].UpdateFillLife(idUnit, GameManager.Get().GetUnitsArmy()[index].Life);
                break;
            case MilitaryType.Mercenary:
                idUnit = GameManager.Get().GetUnitsMercenary()[index].IdUnit;
                btnUnitsMilitary[index].UpdateFillLife(idUnit, GameManager.Get().GetUnitsMercenary()[index].Life);
                break;
        }

        btnUnitsMilitary[index].imageUnit.sprite = GameManager.Get().GetCurrentSprite(idUnit, militaryType);
    }

    void AddNewUnit ()
    {
        for (int i = 0; i < btnUnitsMilitary.Count; i++)
        {
            int index = i;
            if (!btnUnitsMilitary[i].gameObject.activeSelf)
            {
                InizializePanel(index);
                return;
            }
        }

        UnitMilitaryComponent militar = Instantiate(pfUnitMilitar, content);
        btnUnitsMilitary.Add(militar);
        InizializePanel(btnUnitsMilitary.Count - 1);
    }
}

public enum MilitaryType
{
    Army,
    Mercenary
}