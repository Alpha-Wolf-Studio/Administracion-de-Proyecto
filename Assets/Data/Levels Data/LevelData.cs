using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string LevelName = "Level 1";
    public int GoldIncome = 1;
    public int GoldOnComplete = 1;
    public int DiamondOnComplete = 1;
    public int Index = 0;
    public int ProvinceIndex = 0;
    public List<EnemyConfigurations> Enemies = new List<EnemyConfigurations>();
    public List<ControlPointConfigurations> ControlPoints = new List<ControlPointConfigurations>();
    public EnemySpawnerConfiguration EnemySpawner = new EnemySpawnerConfiguration();
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

[System.Serializable]
public class EnemySpawnerConfiguration
{

    public Action OnSpawnerCopied;
    
    [Header("Spawner General Configuration")]
    public EnemySpawner.SpawnerClass spawnerClass = EnemySpawner.SpawnerClass.None;
    public LanesFlags spawnLanes = LanesFlags.Mid;
    public EnemyType enemyToSpawn = EnemyType.Balloon;
    public float spawnVariance = 2f; 
    
    [Header("Spawn With Time")]
    public float timeForSpawn = 8;
    public bool repeatAfterTime = true;

    [Header("Spawn With Trigger")]
    public float timeForTriggerSpawn = 8;
    public bool repeatAfterTrigger = true;

    public void CopySpawner(EnemySpawnerConfiguration spawnerToCopy)
    {
        spawnerClass = spawnerToCopy.spawnerClass;
        spawnLanes = spawnerToCopy.spawnLanes;
        enemyToSpawn = spawnerToCopy.enemyToSpawn;
        spawnVariance = spawnerToCopy.spawnVariance;
        timeForSpawn = spawnerToCopy.timeForSpawn;
        repeatAfterTime = spawnerToCopy.repeatAfterTime;
        timeForTriggerSpawn = spawnerToCopy.timeForTriggerSpawn;
        repeatAfterTrigger = spawnerToCopy.repeatAfterTrigger;
        
        OnSpawnerCopied?.Invoke();
    }
    
}