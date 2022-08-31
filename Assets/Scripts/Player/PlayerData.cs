using System.Collections.Generic;

public class PlayerData
{
    public long lastSavedTime;
    public int currentLevel;
    public int[] campaingStatus;

    public int currentMoney;
    public int[] levelUnits = new int[8];
    public string playerName;

    public UnitData[] dataArmies;
    public UnitData[] dataMercenaries;

    public void SaveUnitsArmy (List<Unit> units)
    {
        dataArmies = new UnitData[units.Count];
        for (int i = 0; i < units.Count; i++)
        {
            dataArmies[i].unitType = units[i].stats.unitType;
            dataArmies[i].life = units[i].stats.life;
        }
    }

    public void SaveUnitsMercenaries (List<Unit> units)
    {
        dataMercenaries = new UnitData[units.Count];
        for (int i = 0; i < units.Count; i++)
        {
            dataMercenaries[i].unitType = units[i].stats.unitType;
            dataMercenaries[i].life = units[i].stats.life;
        }
    }
}

public class UnitData
{
    public UnitsType unitType;
    public float life;
}