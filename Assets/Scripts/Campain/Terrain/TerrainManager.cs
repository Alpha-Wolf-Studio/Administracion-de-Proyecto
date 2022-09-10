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
        GetCurrentHexagonStates();
    }

    public void ResetCurrentHexagonStates () 
    {
        foreach (var currentHexagon in currentHexagons)
        {
            currentHexagon.CurrentState = TerrainState.Unavailable;
            currentHexagon.Deselect();
        }
        currentHexagons[0].CurrentState = TerrainState.Locked;
        OnResetTerrainStates?.Invoke(currentHexagons[0]);
    }

    public void SaveCurrentHexagonStates () 
    {
        GameManager.Get().SetTerrainStates(currentHexagons);
        OnSaveTerrainStates?.Invoke();
    }

    public void GetCurrentHexagonStates () 
    {
        var campaignState = GameManager.Get().GetTerrainStates();
        for (int i = 0; i < campaignState.Length; i++)
        {
            currentHexagons[i].Init((TerrainState)campaignState[i], i);
        }
    }

    public HexagonTerrain GetHexagonByIndex(int index) => currentHexagons.Find(i => i.TerrainIndex == index);

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
