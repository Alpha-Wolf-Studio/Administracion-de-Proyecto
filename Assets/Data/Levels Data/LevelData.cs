using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string LevelName = "Level 1";
    public int GoldIncome = 1;
    public int GoldOnComplete = 1;
    public int Index = 0;
    public int ProvinceIndex = 0;
    public List<EnemyConfigurations> Enemies = new List<EnemyConfigurations>();
    public List<ControlPointConfigurations> ControlPoints = new List<ControlPointConfigurations>();
}

[System.Serializable]
public class EnemyConfigurations
{
    public int EnemyIndex;
    public Vector3 EnemyPosition;
    public Vector3 EnemyRotation;
    public EnemyType TypeOfEnemy;
    public LanesFlags AttackLaneFlags;
    public LanesFlags OwnLaneFlags;
}

[System.Serializable]
public class ControlPointConfigurations
{
    public Vector3 ControlPosition;
    public Vector3 ControlRotation;
    public ControlPointData ControlData;
    public List<int> ControlEnemiesIndex;

    public int GetControlLanesAmount()
    {
        int lanesAmount = 0;
        if (ControlData.controlLanesFlags.HasFlag(LanesFlags.Bottom)) lanesAmount++;
        if (ControlData.controlLanesFlags.HasFlag(LanesFlags.Mid)) lanesAmount++;
        if (ControlData.controlLanesFlags.HasFlag(LanesFlags.Top)) lanesAmount++;
        return lanesAmount;
    }
}