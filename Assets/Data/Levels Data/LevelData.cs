using System.Collections;
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
}

[System.Serializable]
public class EnemyConfigurations
{
    public Vector3 EnemyPosition;
    public Vector3 EnemyRotation;
    public EnemyType TypeOfEnemy;
    public LanesFlags AttackLaneFlags;
    public LanesFlags OwnLaneFlags;
}