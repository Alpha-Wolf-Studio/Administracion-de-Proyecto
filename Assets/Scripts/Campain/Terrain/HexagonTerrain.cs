using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HexagonTerrain : MonoBehaviour
{

    public System.Action OnSelect = default;
    public System.Action OnDeSelect = default;

    [SerializeField] private GameObject flagObject = default;
    [SerializeField] private MeshRenderer flagMesh = default;
    [Header("Terrains Graphics")] 
    [SerializeField] private List<GameObject> desertCosmetics;
    [SerializeField] private List<GameObject> forestCosmetics;
    [SerializeField] private List<GameObject> tundraCosmetics;
    [SerializeField] private List<GameObject> unreachableCosmetics;

    [Space(20)] 
    [SerializeField] private TerrainGraphicType customTerrainType = TerrainGraphicType.UnreachableWater;
    
    public int index = 0;
    
    private TerrainManager.TerrainState currentState = default;
    private GameObject terrainGraphic;

    private LevelData levelData = new LevelData();
    public LevelData GetLevelData() => levelData;
    
    public bool IsValid { get; set; }

    public void ResetHexagonData() 
    {
        int previousIndex = levelData.Index;
        levelData = new LevelData();
        levelData.Index = previousIndex;
        SetData(levelData);
    }

    public int TerrainIndex 
    {
        get 
        {
            return levelData.Index;
        }
        private set 
        {
            levelData.Index = value;
        }
    }

    public int ProvinceIndex
    {
        get 
        {
            return levelData.ProvinceIndex;
        }
        private set
        {
            levelData.ProvinceIndex = value;
            ChangeProvinceIndex(value);
        }
    }

    public TerrainGraphicType TerrainGraphicType
    {
        get
        {
            return levelData.TerrainType;
        }
        set
        {
            levelData.TerrainType = value;
            ChangeTerrainType(value);
        }
    }

    public TerrainManager.TerrainState CurrentState
    {
        get 
        {
            return currentState;
        }
        set
        {
            currentState = value;
            ChangeTerrainState(value);
        }
    }

    public string TerrainName => levelData.LevelName;
    public int GoldIncome => levelData.GoldIncome;
    public int GoldOnComplete => levelData.GoldOnComplete;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += CustomValidate;
    }
#endif

    private void CustomValidate()
    {
        if (this == null) 
            return;
        
        if (customTerrainType != TerrainGraphicType)
        {
            TerrainGraphicType = customTerrainType;
        }
    }

    public void InitStatus (TerrainManager.TerrainState terrainStatus)
    {
        ChangeTerrainState(terrainStatus);
    }

    public void SetData(LevelData data) 
    {
        TerrainIndex = data.Index;
        TerrainGraphicType = data.TerrainType;
        ProvinceIndex = data.ProvinceIndex;
        levelData.LevelName = data.LevelName;
        levelData.GoldIncome = data.GoldIncome;
        levelData.GoldOnComplete = data.GoldOnComplete;
        levelData.DiamondOnComplete = data.DiamondOnComplete;
        levelData.Enemies = data.Enemies;
        levelData.ControlPoints = data.ControlPoints;
        IsValid = true;
    }

    private void ChangeTerrainState (TerrainManager.TerrainState terrainState) 
    {
        currentState = terrainState;

        switch (currentState)
        {
            case TerrainManager.TerrainState.Unlocked:
                flagObject.SetActive(true);
                flagMesh.materials[1].color = Color.blue;
                break;
            case TerrainManager.TerrainState.Locked:
                flagObject.SetActive(true);
                flagMesh.materials[1].color = Color.red;
                break;
            case TerrainManager.TerrainState.Unavailable:
                flagObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void ChangeProvinceIndex(int index) 
    {
        var color = GameManager.Get().GetProvinceSetting(index).Color;
        terrainGraphic.GetComponentInChildren<TerrainGraphicBase>().SetBaseMaterialColor(color);
    }
    
    private void ChangeTerrainType(TerrainGraphicType terrainType)
    {
        ResetAllTerrains();
        
        customTerrainType = TerrainGraphicType;
        
        int terrainIndex = 0;
        
        switch (terrainType)
        {
            case TerrainGraphicType.Forest:
                terrainIndex = Random.Range(0, forestCosmetics.Count);
                terrainGraphic = forestCosmetics[terrainIndex];
                break;
            
            case TerrainGraphicType.Desert:
                terrainIndex = Random.Range(0, desertCosmetics.Count);
                terrainGraphic = desertCosmetics[terrainIndex];
                break;
            
            case TerrainGraphicType.Snow:
                terrainIndex = Random.Range(0, tundraCosmetics.Count);
                terrainGraphic = tundraCosmetics[terrainIndex];
                break;
            
            case TerrainGraphicType.UnreachableWater:
                terrainGraphic = unreachableCosmetics[0];
                break;
            
            case TerrainGraphicType.UnreachableMountain:
                terrainGraphic = unreachableCosmetics[1];
                break;
            
            default:
                break;
        }
        
        terrainGraphic.SetActive(true);
    }

    private void ResetAllTerrains()
    {
        ResetTerrains(desertCosmetics);
        ResetTerrains(forestCosmetics);
        ResetTerrains(tundraCosmetics);
        ResetTerrains(unreachableCosmetics);
    }
    
    private void ResetTerrains(List<GameObject> terrains)
    {
        foreach (var terrain in terrains)
        {
            terrain.SetActive(false);
        }
    }
    
    public void Select () => OnSelect?.Invoke();
    public void Deselect () => OnDeSelect?.Invoke();

}