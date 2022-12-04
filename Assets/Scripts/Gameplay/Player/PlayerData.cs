using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    private const int diferentsOtters = 4;
    public static int levelMoreMercenary = 5;
    public int tutorialIndex = 0;
    public int tutorialStep = 0;

    public long LastSavedTime;

    public int LastLevelComplete = 0;
    public int[] CampaingStatus = Array.Empty<int>();

    public int CurrentGold = 100;
    public int CurrentDiamond = 1;
    public string PlayerName = "None";

    public int[] LevelUnitsArmy = new int[diferentsOtters];
    public int[] LevelUnitsMercenary = new int[diferentsOtters];

    public int[] maxAmountArmy = new int[diferentsOtters];
    public int[] maxAmountMercenary = new int[diferentsOtters];

    public UnitData[] DataArmies = Array.Empty<UnitData>();
    public UnitData[] DataMercenaries = Array.Empty<UnitData>();

    public PlayerData ()
    {
        for (int i = 0; i < diferentsOtters; i++)
        {
            maxAmountMercenary[i] = 12;
            LevelUnitsMercenary[i] = levelMoreMercenary;
        }
    }

    public void SaveUnitsArmy (List<Unit> units)
    {
        DataArmies = new UnitData[units.Count];
        for (int i = 0; i < units.Count; i++)
        {
            DataArmies[i].IdUnit = units[i].stats.idUnit;
            DataArmies[i].UnitType = units[i].stats.unitType;
            DataArmies[i].Life = units[i].stats.life;
        }
    }

    public void SaveUnitsMercenaries (List<Unit> units)
    {
        DataMercenaries = new UnitData[units.Count];
        for (int i = 0; i < units.Count; i++)
        {
            DataMercenaries[i].IdUnit = units[i].stats.idUnit;
            DataMercenaries[i].UnitType = units[i].stats.unitType;
            DataMercenaries[i].Life = units[i].stats.life;
        }
    }
}

[System.Serializable]
public class UnitData
{
    public int IdUnit;
    public UnitsType UnitType;
    public float Life;

    public UnitData (int newIdUnit, UnitsType newUnitType, float newLife)
    {
        IdUnit = newIdUnit;
        UnitType = newUnitType;
        Life = newLife;
    }
}