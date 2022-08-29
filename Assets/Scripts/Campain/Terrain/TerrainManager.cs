using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

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

    public void ResetCurrentHexagonStates() 
    {
        foreach (var currentHexagon in currentHexagons)
        {
            currentHexagon.CurrentState = TerrainState.Unavailable;
        }
        currentHexagons[0].CurrentState = TerrainState.Locked;
    }

    public void SaveCurrentHexagonStates () 
    {
        GameManager.Get().SetTerrainStates(currentHexagons);
    }

    public void GetCurrentHexagonStates () 
    {
        var campaignState = GameManager.Get().GetTerrainStates();
        for (int i = 0; i < campaignState.Length; i++)
        {
            currentHexagons[i].Init((TerrainState)campaignState[i], i);
        }
    }

    public enum TerrainState 
    {
        Unlocked,
        Locked,
        Unavailable
    }

}
