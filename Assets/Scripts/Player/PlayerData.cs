using System.Collections.Generic;

[System.Serializable]
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
            dataArmies[i].idUnit = units[i].stats.idUnit;
            dataArmies[i].unitType = units[i].stats.unitType;
            dataArmies[i].life = units[i].stats.life;
        }
    }

    public void SaveUnitsMercenaries (List<Unit> units)
    {
        dataMercenaries = new UnitData[units.Count];
        for (int i = 0; i < units.Count; i++)
        {
            dataMercenaries[i].idUnit = units[i].stats.idUnit;
            dataMercenaries[i].unitType = units[i].stats.unitType;
            dataMercenaries[i].life = units[i].stats.life;
        }
    }
}

[System.Serializable]
public class UnitData
{
    public int idUnit;
    public UnitsType unitType;
    public float life;

    public UnitData (int newIdUnit, UnitsType newUnitType, float newLife)
    {
        idUnit = newIdUnit;
        unitType = newUnitType;
        life = newLife;
    }
}