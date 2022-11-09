using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    public System.Action<HexagonTerrain> OnResetTerrainStates;
    public System.Action OnSaveTerrainStates;

    
    private WorldBuilderHexagon worldBuilder = default;
    private List<HexagonTerrain> currentHexagons = default;

    private void Awake ()
    {
        worldBuilder = GetComponent<WorldBuilderHexagon>();
        currentHexagons = worldBuilder.GetAllHexagons();
    }

    void Start ()
    {
        GetCurrentHexagonData();
        GetCurrentHexagonStates();
    }

    public void UnlockAllHexagons()
    {
        foreach (var currentHexagon in currentHexagons)
        {
            if (currentHexagon.IsValid)
            {
                currentHexagon.CurrentState = TerrainState.Unlocked;
                currentHexagon.Deselect();
            }
        }
    }

    public void ResetCurrentHexagonStates () 
    {
        foreach (var currentHexagon in currentHexagons)
        {
            if (currentHexagon.IsValid)
            {
                currentHexagon.CurrentState = TerrainState.Unavailable;
                currentHexagon.Deselect();
            }
        }

        int startIndex = GameManager.Get().StartingLevelIndex;
        
        currentHexagons[startIndex].CurrentState = TerrainState.Locked;
        OnResetTerrainStates?.Invoke(currentHexagons[startIndex]);
    }

    public void SaveCurrentHexagonStates () 
    {
        GameManager.Get().SetTerrainStates(currentHexagons);
        OnSaveTerrainStates?.Invoke();
    }

    public void SaveCurrentHexagonData()
    {
        List<LevelData> allLevelsData = new List<LevelData>();
        foreach (var currentHexagon in currentHexagons)
        {
            GameManager.Get().SaveLevelData(currentHexagon.GetLevelData());
            allLevelsData.Add(currentHexagon.GetLevelData());
        }

        GameManager.Get().SaveProvinceData(allLevelsData);
    }

    public void ResetCurrentHexagonData()
    {
        List<LevelData> allLevelsData = new List<LevelData>();
        foreach (var currentHexagon in currentHexagons)
        {
            currentHexagon.ResetHexagonData();
            allLevelsData.Add(currentHexagon.GetLevelData());
        }

        GameManager.Get().SaveProvinceData(allLevelsData);
    }


    private void GetCurrentHexagonData()
    {
        for (int i = 0; i < currentHexagons.Count; i++)
        {
            LevelData data = new LevelData();
            data = GameManager.Get().GetLevelDataByFalseIndex(i);
            if (data == null)
            {
                currentHexagons[i].IsValid = false;
            }
            else
            {
                data.Index = i;
                currentHexagons[i].SetData(data);
            } 
            currentHexagons[i].index = i;
        }
    }

    public void GetCurrentHexagonStates () 
    {
        var campaignState = GameManager.Get().GetTerrainStates();

        for (int i = 0; i < campaignState.Length; i++)
        {
            if(currentHexagons[i].IsValid) currentHexagons[i].InitStatus((TerrainState)campaignState[i]);
        }
    }

    public HexagonTerrain GetHexagonByLevel(int level) => currentHexagons.Find(i => i.TerrainIndex == level);
    public HexagonTerrain GetHexagonByFalseIndex(int index) => currentHexagons.Find(i => i.TerrainIndex == index + GameManager.Get().StartingLevelIndex);

    public enum TerrainState 
    {
        Unlocked,
        Locked,
        Unavailable
    }

    public static int[] GetDefaultTerrainEnumIndexes (int rows, int columns) 
    {

        int[] terrainStatus = new int[rows * columns];

        for (int i = 0; i < terrainStatus.Length; i++)
        {
            terrainStatus[i] = (int)TerrainState.Unavailable;
        }

        terrainStatus[0] = (int)TerrainState.Locked;
        
        return terrainStatus;

    }

}
