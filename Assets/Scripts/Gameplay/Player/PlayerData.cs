using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public long LastSavedTime;

    public int LastLevelComplete;
    public int[] CampaingStatus;

    public int CurrentGold;
    public int CurrentDiamond;
    public int[] LevelUnits = new int[8];
    public string PlayerName;

    public UnitData[] DataArmies;
    public UnitData[] DataMercenaries;

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